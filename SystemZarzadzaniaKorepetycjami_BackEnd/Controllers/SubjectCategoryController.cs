using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("addSubjectCategory")]
    [Authorize]
    public async Task<IActionResult> CreateSubjectCategoryAsync(SubjectCategoryDTO subjectCategoryDTO)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

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

    [HttpPut("{subjectName}/{subjectCategoryName}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteSubjectCategoryAsync(string subjectName,
        string subjectCategoryName)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var subjectCategoryStatus =
            await _subjectCategoryService.DeleteSubjectCategoryAsync(subjectName, subjectCategoryName);
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