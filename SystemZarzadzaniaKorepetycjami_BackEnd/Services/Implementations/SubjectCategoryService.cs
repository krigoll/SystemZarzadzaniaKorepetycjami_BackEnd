using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class SubjectCategoryService : ISubjectCategoryService
{
    private readonly ISubjectCategoryRepository _subjectCategoryRepository;
    private readonly ISubjectRepository _subjectRepository;

    public SubjectCategoryService(ISubjectRepository subjectRepository,
        ISubjectCategoryRepository subjectCategoryRepository)
    {
        _subjectRepository = subjectRepository;
        _subjectCategoryRepository = subjectCategoryRepository;
    }

    public async Task<SubjectCategoryStatus> CreateSubjectCategoryAsync(SubjectCategoryDTO subjectCategoryDTO)
    {
        try
        {
            var subject = await _subjectRepository.FindSubjectByNameAsync(subjectCategoryDTO.SubjectName);
            if (subject == null) return SubjectCategoryStatus.INVALID_SUBJECT_ID;

            var newSubjectCategory = new SubjectCategory(subjectCategoryDTO.SubjectCategoryName, subject.IdSubject);

            await _subjectCategoryRepository.CreateSubjectCategoryAsync(newSubjectCategory);

            return SubjectCategoryStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectCategoryStatus.SERVER_ERROR;
        }
    }

    public async Task<SubjectCategoryStatus> DeleteSubjectCategoryAsync(string subjectName,
        string subjectCategoryName)
    {
        try
        {
            var subject = await _subjectRepository.FindSubjectByNameAsync(subjectName);
            if (subject == null) return SubjectCategoryStatus.INVALID_SUBJECT_ID;

            await _subjectCategoryRepository.DeleteSubjectCategoryAsync(subject.IdSubject, subjectCategoryName);

            return SubjectCategoryStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SubjectCategoryStatus.SERVER_ERROR;
        }
    }
}