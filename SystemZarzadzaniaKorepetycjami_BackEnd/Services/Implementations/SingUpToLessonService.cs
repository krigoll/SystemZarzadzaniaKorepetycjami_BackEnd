using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations
{
    public class SingUpToLessonService : ISingUpToLessonService
    {

        private readonly IPersonRepository _personRepository;
        private readonly ISubjectLevelRepository _subjectLevelRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IMessageRepository _messageRepository;

        public SingUpToLessonService(IPersonRepository personRepository, ISubjectLevelRepository subjectLevelRepository, ILessonRepository lessonRepository, IMessageRepository messageRepository)
        {
            _personRepository = personRepository;
            _subjectLevelRepository = subjectLevelRepository;
            _lessonRepository = lessonRepository;
            _messageRepository = messageRepository
        }

        public async Task<SingUpToLessonStaus> SingUpToLessonAndSendMessageAsync(SingUpToLessonDTO singUpToLessonDTO);
        {
            try 
            {
                var student = await _personRepository.FindPersonByEmailAsync(singUpToLessonDTO.StudentEmail);
                var teacher = await _teacherRepository.FindUserByIdAsync(singUpToLessonDTO.TeacherId);
                if (student == null || teacher == null)
                {
                    return SingUpToLessonStaus.INVALID_EMAIL;
                }
                if (!(await _teacherRepository.isTeacherByEmail(teacher.email)))
                {
                    return SingUpToLessonStaus.INVALID_EMAIL;
                }//jeszcze student czy student
                var subjectNotExists = await _subjectLevelRepository.IsSubjectLevelExistsBySubjectLevelIdAsync(singUpToLessonDTO.SubjectLevelId);
                if (subjectNotExists) 
                {
                    return SingUpToLessonStaus.INVALID_SUBJECT;
                }
                var lesson = new lesson(student.IdPerson, teacher.IdPerson, singUpToLessonDTO.SubjectLevelId, 1, singUpToLessonDTO.StartDate, singUpToLessonDTO.DurationInMinutes);
                var isConflictWithAnotherLesson = await _lessonRepository.IsLessonConflictlessAsync(lesson);
                if (isConflictWithAnotherLesson)
                {
                    return SingUpToLessonStaus.CONFLICT_LESSON;
                }
                await _lessonRepository.AddLessonAsync(lesson);
                var systemMessage = new Message(student.IdPerson, teacher.IdPerson, DataTime.Now(), "Nowa rejestracja!");
                await _messageRepository.SendMessageAsync(systemMessage);
                if (singUpToLessonDTO.MessageContent != null)
                {
                    var studentMessage = new Message(student.IdPerson, teacher.IdPerson, DataTime.Now(), singUpToLessonDTO.MessageContent);
                    await _messageRepository.SendMessageAsync(systemMessage);
                }
                return SingUpToLessonStaus.OK;  
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return SingUpToLessonStaus.INVALID_LESSON;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return SingUpToLessonStaus.DATABASE_ERROR;
            }
        }
    }
}
