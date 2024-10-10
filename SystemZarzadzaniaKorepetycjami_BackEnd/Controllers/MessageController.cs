using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("getConversations")]
    [Authorize]
    public async Task<IActionResult> GetConversationsByEmailAsync(string email)
    {
        var ConversationDTOList = await _messageService.GetConversationsByEmailAsync(email);

        return ConversationDTOList == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid email")
            : Ok(ConversationDTOList);
    }

    [HttpGet("getConversation")]
    [Authorize]
    public async Task<IActionResult> GetConversationByUserIdAndCorespondentIdAsync(int userId, int corespondentId)
    {
        var MessageDTOList =
            await _messageService.GetConversationByUserIdAndCorespondentIdAsync(userId, corespondentId);

        return MessageDTOList == null
            ? StatusCode(StatusCodes.Status400BadRequest, "Invalid sender or reciver")
            : Ok(MessageDTOList);
    }


    [HttpPost("createMessage")]
    [Authorize]
    public async Task<IActionResult> CreateMessageAsync(MessageDTO messageDTO)
    {
        var createMessageStatus = await _messageService.CreateMessageAsync(messageDTO);
        switch (createMessageStatus)
        {
            case CreateMessageStatus.INVALID_MESSAGE:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Opinion");
                break;
            case CreateMessageStatus.OK:
                return Ok();
                break;
            case CreateMessageStatus.INVALID_SENDER_OR_RECIVER:
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid sender or reciver");
                break;
            case CreateMessageStatus.SERVER_ERROR:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
                break;
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}