using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        public Task<List<ConversationDTO>> GetConversationsByUserAsync(Person user);
	    public Task<List<MessageDTO>> GetConversationBySenderAndReciverAsync(Person user, Person corespondent);
	    public Task AddMessageAsync(Message newMessage);   
    }
}
