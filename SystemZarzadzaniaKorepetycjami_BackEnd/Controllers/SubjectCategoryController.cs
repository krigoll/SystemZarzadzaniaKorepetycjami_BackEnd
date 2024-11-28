using Microsoft.AspNetCore.Mvc;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/subjectCategory")]
public class SubjectCategoryController : ControllerBase
{
    private readonly ISubjectCategoryService _subjectCategoryService;

    public SubjectCategoryController(ISubjectCategoryService subjectCategoryService)
    {
        _subjectCategoryService = subjectCategoryService;
    }

    //sprawdzenie czy jest adminem
    [HttpPost]
    public async Task<IActionResult> CreateSubjectCategoryAsync(SubjectCategoryDTO subjectCategoryDTO)
    {
        var subjectCategoryStatus = await _subjectCategoryService.CreateSubjectCategoryAsync(subjectCategoryDTO);
        switch (subjectCategoryStatus)
        {
            case SubjectCategoryStatus.OK:
                return Ok();
                break;
            case SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Category");
                break;
            case SubjectCategoryStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case SubjectCategoryStatus.INVALID_SUBJECT_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{idSubjectCategory}")]
    public async Task<IActionResult> UpdateSubjectCategoryAsync(int idSubjectCategory,
        SubjectCategoryDTO subjectCategoryDTO)
    {
        var subjectCategoryStatus =
            await _subjectCategoryService.UpdateSubjectCategoryAsync(idSubjectCategory, subjectCategoryDTO);
        switch (subjectCategoryStatus)
        {
            case SubjectCategoryStatus.OK:
                return Ok();
                break;
            case SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Category");
                break;
            case SubjectCategoryStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case SubjectCategoryStatus.INVALID_SUBJECT_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Id");
                break;
            case SubjectCategoryStatus.INVALID_SUBJECT_CATEGORY_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Category Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}