using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IPersonRepository _personRepository;

    public MessageService(IMessageRepository messageRepository, IPersonRepository personRepository)
    {
        _messageRepository = messageRepository;
        _personRepository = personRepository;
    }

    public async Task<List<ConversationDTO>> GetConversationsByEmailAsync(string email)
    {
        var person = await _personRepository.FindPersonByEmailAsync(email);
        if (person == null) return null;

        return await _messageRepository.GetConversationsByPersonAsync(person);
    }

    public async Task<List<MessageDTO>> GetConversationByUserIdAndCorespondentIdAsync(int userId, int corespondentId)
    {
        var user = await _personRepository.FindPersonByIdAsync(userId);
        var corespondent = await _personRepository.FindPersonByIdAsync(corespondentId);
        if (user == null || corespondent == null) return null;

        return await _messageRepository.GetConversationBySenderAndReciverAsync(user, corespondent);
    }

    public async Task<CreateMessageStatus> CreateMessageAsync(MessageDTO messageDTO)
    {
        try
        {
            var sender = await _personRepository.FindPersonByIdAsync(messageDTO.SenderId);
            var receiver = await _personRepository.FindPersonByIdAsync(messageDTO.ReceiverId);
            if (sender == null || receiver == null) return CreateMessageStatus.INVALID_SENDER_OR_RECIVER;

            var newMessage = new Message(
                sender.IdPerson,
                receiver.IdPerson,
                DateTime.Parse(messageDTO.Date),
                messageDTO.Content
            );
            await _messageRepository.AddMessageAsync(newMessage);
            return CreateMessageStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return CreateMessageStatus.INVALID_MESSAGE;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateMessageStatus.SERVER_ERROR;
        }
    }
}