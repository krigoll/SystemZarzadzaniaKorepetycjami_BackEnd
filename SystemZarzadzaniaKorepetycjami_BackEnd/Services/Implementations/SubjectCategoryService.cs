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
            var subject = await _subjectRepository.FindSubjectByIdAsync(subjectCategoryDTO.IdSubject);
            if (subject == null) return SubjectCategoryStatus.INVALID_SUBJECT_ID;

            var newSubjectCategory = new SubjectCategory(subjectCategoryDTO.IdSubject, subjectCategoryDTO.Name);

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

    public async Task<SubjectCategoryStatus> UpdateSubjectCategoryAsync(int idSubjectCategory,
        SubjectCategoryDTO subjectCategoryDTO)
    {
        try
        {
            var updateSubjectCategory =
                await _subjectCategoryRepository.FindSubjectCategoryByIdAsync(idSubjectCategory);
            if (updateSubjectCategory == null) return SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY_ID;

            var subject = await _subjectRepository.FindSubjectByIdAsync(subjectCategoryDTO.IdSubject);
            if (subject == null) return SubjectCategoryStatus.INVALID_SUBJECT_ID;

            updateSubjectCategory.SetName(subjectCategoryDTO.Name);
            updateSubjectCategory.SetIdSubject(subjectCategoryDTO.IdSubject);

            await _subjectCategoryRepository.UpdateSubjectCategoryAsync(updateSubjectCategory);

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