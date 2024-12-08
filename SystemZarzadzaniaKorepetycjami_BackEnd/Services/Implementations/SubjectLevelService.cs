using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public class SubjectLevelService : ISubjectLevelService
{
    private readonly ISubjectCategoryRepository _subjectCategoryRepository;
    private readonly ISubjectLevelRepository _subjectLevelRepository;
    private readonly ISubjectRepository _subjectRepository;

    public SubjectLevelService(ISubjectLevelRepository subjectLevelRepository,
        ISubjectCategoryRepository subjectCategoryRepository, ISubjectRepository subjectRepository)
    {
        _subjectLevelRepository = subjectLevelRepository;
        _subjectCategoryRepository = subjectCategoryRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<SubjectLevelStatus> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO)
    {
        try
        {
            var subject = await _subjectRepository.FindSubjectByNameAsync(subjectLevelDTO.SubjectName);
            var subjectCategory =
                await _subjectCategoryRepository.FindSubjectCategoryBySubjectIdAndSubjectCategoryNameAsync(
                    subject.IdSubject,
                    subjectLevelDTO.SubjectCategoryName);
            if (subjectCategory == null) return SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID;

            var newSubjectLevel = new SubjectLevel(subjectLevelDTO.SubjectLevelName, subjectCategory.IdSubjectCategory);

            await _subjectLevelRepository.CreateSubjectLevelAsync(newSubjectLevel);

            return SubjectLevelStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectLevelStatus.INVALID_SUBJECT_LEVEL;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectLevelStatus.SERVER_ERROR;
        }
    }

    public async Task<SubjectLevelStatus> DeleteSubjectLevelAsync(string subjectName, string subjectCategoryName,
        string subjectLevelName)
    {
        try
        {
            var subject = await _subjectRepository.FindSubjectByNameAsync(subjectName);

            var subjectCategory =
                await _subjectCategoryRepository.FindSubjectCategoryBySubjectIdAndSubjectCategoryNameAsync(
                    subject.IdSubject,
                    subjectCategoryName);
            if (subjectCategory == null) return SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID;


            await _subjectLevelRepository.DeleteSubjectLevelAsync(subjectCategory.IdSubjectCategory, subjectLevelName);

            return SubjectLevelStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectLevelStatus.INVALID_SUBJECT_LEVEL;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectLevelStatus.SERVER_ERROR;
        }
    }
}