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

    //sprawdzenie czy jest adminem
    [HttpPost]
    public async Task<IActionResult> CreateSubjectLevelAsync(SubjectLevelDTO subjectLevelDTO)
    {
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

    [HttpPut("{idSubjectLevel}")]
    public async Task<IActionResult> UpdateSubjectLevelAsync(int idSubjectLevel, SubjectLevelDTO subjectLevelDTO)
    {
        var subjectLevelStatus = await _subjectLevelService.UpdateSubjectLevelAsync(idSubjectLevel, subjectLevelDTO);
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