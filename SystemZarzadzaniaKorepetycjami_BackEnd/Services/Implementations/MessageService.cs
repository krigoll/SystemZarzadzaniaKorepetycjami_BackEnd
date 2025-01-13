using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations
{
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

            var conversations = await _messageRepository.GetConversationsByUserAsync(person);
            var sortedConversations = conversations.OrderBy(c => c.CorespondentName).ToList();
            return sortedConversations;
        }

        public async Task<List<MessageDTO>> GetConversationByUserIdAndCorespondentIdAsync(int userId,
            int corespondentId)
        {
            var user = await _personRepository.FindPersonByIdAsync(userId);
            var corespondent = await _personRepository.FindPersonByIdAsync(corespondentId);
            if (user == null || corespondent == null) return null;

            var messages = await _messageRepository.GetConversationBySenderAndReciverAsync(user, corespondent);
            return messages;
        }

        public async Task<MessageStatus> CreateMessageAsync(MessageDTO messageDTO)
        {
            try
            {
                var sender = await _personRepository.FindPersonByIdAsync(messageDTO.SenderId);
                var reciver = await _personRepository.FindPersonByIdAsync(messageDTO.ReceiverId);
                if (sender == null || reciver == null) return MessageStatus.INVALID_SENDER_OR_RECEIVER;

                var newMessage = new Message(
                    sender.IdPerson,
                    reciver.IdPerson,
                    DateTime.Parse(messageDTO.Date),
                    messageDTO.Content
                );
                await _messageRepository.AddMessageAsync(newMessage);
                return MessageStatus.OK;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return MessageStatus.INVALID_MESSAGE;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return MessageStatus.SERVER_ERROR;
            }
        }
    }
}