using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using Task = System.Threading.Tasks.Task;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SZKContext _context;
	
	    public MessageRepository(SZKContext context)
	    {
		    _context = context;
	    }
	
	    public async Task<List<ConversationDTO>> GetConversationsByUserAsync(Person user)
	    {
            var conversations = await _context.Message
            .Where(m => m.Sender == user.IdPerson || m.Receiver == user.IdPerson)
            .Select(m => new
            {
                CorrespondentId = m.Sender == user.IdPerson ? m.Receiver : m.Sender
            })
            .Distinct()
            .Join(_context.Person,
                msg => msg.CorrespondentId,
                person => person.IdPerson,
                (msg, person) => new ConversationDTO
                {
                    UserId = user.IdPerson,
                    CorespondentId = person.IdPerson,
                    CorespondentName = person.Name + " " + person.Surname
                })
            .ToListAsync();
            return conversations;
	    }
	
	    public async Task<List<MessageDTO>> GetConversationBySenderAndReciverAsync(Person user, Person corespondent)
	    {
		    var messages = await _context.Message
			    .Where(m => (m.Sender == user.IdPerson || m.Receiver == corespondent.IdPerson) || (m.Sender == corespondent.IdPerson || m.Receiver == user.IdPerson))
			    .OrderByDescending(m => m.Date)
			    .Select(m => new MessageDTO {
				    SenderId = m.Sender,
				    ReceiverId = m.Receiver,
				    Date = m.Date.ToString("yyyy-MM-dd HH:mm"),
				    Content = m.Content
			    })
			    .ToListAsync();
		    return messages;
	    }
	
	    public async Task AddMessageAsync(Message newMessage)
	    {
		    await _context.Message.AddAsync(newMessage);
		    await _context.SaveChangesAsync();
	    }
    }
}
