using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<List<ConversationDTO>> GetConversationsByEmailAsync(string email);
	    public Task<List<MessageDTO>> GetConversationByUserIdAndCorespondentIdAsync(int userId, int corespondentId);
	    public Task<MessageStatus> CreateMessageAsync(MessageDTO messageDTO); 
    }
}
