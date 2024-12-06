using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/subjectLevel")]
public class SubjectLevelController : ControllerBase
{
    private readonly ISubjectLevelService _subjectLevelService;

    public SubjectLevelController(ISubjectLevelService subjectLevelService)
    {
        _subjectLevelService = subjectLevelService;
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var subjectLevelStatus = await _subjectLevelService.CreateSubjectLevelAsync(subjectLevelDTO);
        switch (subjectLevelStatus)
        {
            case SubjectLevelStatus.OK:
                return Ok();
                break;
            case SubjectLevelStatus.INVALID_SUBJECT_LEVEL:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Level");
                break;
            case SubjectLevelStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Category Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }

    [HttpPut("{subjectName}/{subjectCategoryName}/{subjectLevelName}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteSubjectLevelAsync(string subjectName, string subjectCategoryName,
        string subjectLevelName)
    {
        var isAdminClaim = HttpContext.User.FindFirst("isAdmin")?.Value;

        if (isAdminClaim == null || !bool.TryParse(isAdminClaim, out var isAdmin) || !isAdmin) return Forbid();

        var subjectLevelStatus =
            await _subjectLevelService.DeleteSubjectLevelAsync(subjectName, subjectCategoryName, subjectLevelName);
        switch (subjectLevelStatus)
        {
            case SubjectLevelStatus.OK:
                return Ok();
                break;
            case SubjectLevelStatus.INVALID_SUBJECT_LEVEL:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Level");
                break;
            case SubjectLevelStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            case SubjectLevelStatus.INVALID_SUBJECT_LEVEL_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Level Id");
                break;
            case SubjectLevelStatus.INVALID_SUBJECT_CATEGORY_ID:
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Subject Category Id");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}