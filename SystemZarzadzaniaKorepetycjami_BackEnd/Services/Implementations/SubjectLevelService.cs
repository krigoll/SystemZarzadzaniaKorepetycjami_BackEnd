using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public class SubjectLevelService : ISubjectLevelService
{
    private readonly ISubjectCategoryRepository _subjectCategoryRepository;
    private readonly ISubjectLevelRepository _subjectLevelRepository;

    public SubjectLevelService(ISubjectLevelRepository subjectLevelRepository,
        ISubjectCategoryRepository subjectCategoryRepository)
    {
        _subjectLevelRepository = subjectLevelRepository;
        _subjectCategoryRepository = subjectCategoryRepository;
    }

    public async Task<SubjectLevelStatus> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO)
    {
        try
        {
            var subjectCategory =
                await _subjectCategoryRepository.FindSubjectCategoryByIdAsync(subjectLevelDTO.IdSubjectCategory);
            if (subjectCategory == null) return SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID;

            var newSubjectLevel = new SubjectLevel(subjectLevelDTO.IdSubjectCategory, subjectLevelDTO.Name);

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

    public async Task<SubjectLevelStatus> UpdateSubjectLevelAsync(int idSubjectLevel, SubjectLevelDTO subjectLevelDTO)
    {
        try
        {
            var updateSubjectLevel = await _subjectLevelRepository.FindSubjectLevelByIdAsync(idSubjectLevel);
            if (updateSubjectLevel == null) return SubjectLevelStatus.INVALID_SUBJECT_LEVEL_ID;

            var subjectCategory =
                await _subjectCategoryRepository.FindSubjectCategoryByIdAsync(subjectLevelDTO.IdSubjectCategory);
            if (subjectCategory == null) return SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID;

            updateSubjectLevel.SetName(subjectLevelDTO.Name);
            updateSubjectLevel.SetIdSubjectCategory(subjectLevelDTO.IdSubjectCategory);

            await _subjectLevelRepository.UpdateSubjectLevelAsync(updateSubjectLevel);

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