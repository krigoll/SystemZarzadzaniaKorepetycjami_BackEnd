using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

[ApiController]
[Route("api/opinion")]
public class OpinionController : ControllerBase
{
	private readonly IOpinionService _opinionService;
	
	public OpinionController(IOpinionService opinionService)
	{
		_opinionService = opinionService;
	}
	
	[HttpGet("getOpinionsByStudentEmail")]
	[Authorize]
	public async Task<IActionResult> GetOpinionsByStudentEmailAsync(string email)
	{
		var OpinionDTOList = await _opinionService.GetOpinionsByStudentEmailAsync(email);
			
		return OpinionDTOList == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Student Id") : Ok(OpinionDTOList);	
	}
	
	[HttpGet("getOpinionsByTeacherId")]
	[Authorize]
	public async Task<IActionResult> GetOpinionsByTeacherIdAsync(int teacherId)
	{
		var OpinionDTOList = await _opinionService.GetOpinionsByTeacherIdAsync(teacherId);
			
		return OpinionDTOList == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id") : Ok(OpinionDTOList);	
	}
	
	[HttpGet("getOpinionsByTeacherEmail")]
	[Authorize]
	public async Task<IActionResult> GetOpinionsByTeacherEmailAsync(string email)
	{
		var OpinionDTOList = await _opinionService.GetOpinionsByTeacherEmailAsync(email);
			
		return OpinionDTOList == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Teacher Id") : Ok(OpinionDTOList);	
	}
	
	[HttpGet("{opinionId}")]
	[Authorize]
	public async Task<IActionResult> GetOpinionByIdAsync(int opinionId)
	{
		var OpinionDTO = await _opinionService.GetOpinionDetailsByIdAsync(opinionId);
			
		return OpinionDTO == null ? StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id") : Ok(OpinionDTO);	
	}
	
	[HttpPut("{opinionId}/update")]
	[Authorize]
	public async Task<IActionResult> UpdateOpinionByIdAsync(int opinionId, [FromBody] OpinionCreateDTO opinionCreateDTO)
	{
		var createOpinionStatus = await _opinionService.UpdateOpinionByIdAsync(opinionId, opinionCreateDTO);
		switch(createOpinionStatus) 
		{
			case OpinionStatus.INVALID_OPINION : return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion");
				break;
			case OpinionStatus.OK : return Ok();
				break;
			case OpinionStatus.INVALID_STUDENT_OR_TEACHER : return StatusCode(StatusCodes.Status400BadRequest, "Invalid student or teacher");
				break;
			default:
				return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id");
		}
	}
	
	[HttpDelete("{opinionId}/delete")]
	[Authorize]
	public async Task<IActionResult> DeleteOpinionByIdAsync(int opinionId)
	{
		if (await _opinionService.DeleteOpinionByIdAsync(opinionId))
			return Ok();
		return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion Id");	
	}
	
	// [HttpDelete("deleteAll")]
	// [Authorize]
	// public async Task<IActionResult> DeleteAllOpinionsByEmailAsync(string eamil)
	// {
	// 	//usuwane wszystkich opinji o podanym email 
	// }
	
	[HttpPost("createOpinion")]
	[Authorize]
	public async Task<IActionResult> CreateOpinionAsync(OpinionCreateDTO opinionCreateDTO)
	{
		var createOpinionStatus = await _opinionService.CreateOpinionAsync(opinionCreateDTO);
		switch(createOpinionStatus) 
		{
			case OpinionStatus.INVALID_OPINION : return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion");
				break;
			case OpinionStatus.OK : return Ok();
				break;
			case OpinionStatus.INVALID_STUDENT_OR_TEACHER : return StatusCode(StatusCodes.Status400BadRequest, "Invalid student or teacher");
				break;
			case OpinionStatus.SERVER_ERROR : return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
				break;
			case OpinionStatus.CAN_NOT_OPINION_TEACHER_WITCH_NOT_TEACHED_YOU : return StatusCode(StatusCodes.Status400BadRequest, "Cant no make opinion about teacher witch never teach you");
				break;
			default:
				return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
		}
	}

}