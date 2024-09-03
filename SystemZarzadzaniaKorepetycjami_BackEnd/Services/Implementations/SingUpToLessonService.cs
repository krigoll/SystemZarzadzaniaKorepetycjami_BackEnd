using System.Globalization;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations
{
    public class SingUpToLessonService : ISingUpToLessonService
    {
        private readonly ILessonRepository _lessonRepository;

        private readonly IPersonRepository _personRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectLevelRepository _subjectLevelRepository;
        private readonly ITeacherRepository _teacherRepository;

        public SingUpToLessonService(IPersonRepository personRepository, ISubjectLevelRepository subjectLevelRepository,
            ILessonRepository lessonRepository, IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _personRepository = personRepository;
            _subjectLevelRepository = subjectLevelRepository;
            _lessonRepository = lessonRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<SingUpToLessonStatus> SingUpToLessonAsync(SingUpToLessonDTO singUpToLessonDTO)
        {
            try
            {
                var student = await _personRepository.FindPersonByEmailAsync(singUpToLessonDTO.StudentEmail);
                var teacher = await _personRepository.FindPersonByIdAsync(singUpToLessonDTO.TeacherId);
                if (student == null || teacher == null)
                {
                    return SingUpToLessonStatus.INVALID_EMAIL;
                }

                if (!(await _teacherRepository.isTeacherByEmail(teacher.Email)))
                {
                    return SingUpToLessonStatus.INVALID_EMAIL;
                }

                if (!(await _studentRepository.isStudentByEmail(student.Email)))
                {
                    return SingUpToLessonStatus.INVALID_EMAIL;
                }

                var subjectNotExists =
                    await _subjectLevelRepository.IsSubjectLevelExistsBySubjectLevelIdAsync(singUpToLessonDTO
                        .SubjectLevelId);
                if (subjectNotExists)
                {
                    return SingUpToLessonStatus.INVALID_SUBJECT;
                }

                var startDate = DateTime.ParseExact($"{singUpToLessonDTO.StartDate} {singUpToLessonDTO.StartTime}",
                    "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                var lessonStatusId = 1;    
                var lesson = new Lesson(student.IdPerson, teacher.IdPerson, singUpToLessonDTO.SubjectLevelId, lessonStatusId,
                    startDate, singUpToLessonDTO.DurationInMinutes);
                var isLessonConflictlessAsync = await _lessonRepository.IsLessonConflictlessAsync(lesson);
                if (!isLessonConflictlessAsync)
                {
                    return SingUpToLessonStatus.CONFLICT_LESSON;
                }

                await _lessonRepository.AddLessonAsync(lesson);
                return SingUpToLessonStatus.OK;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return SingUpToLessonStatus.INVALID_LESSON;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return SingUpToLessonStatus.DATABASE_ERROR;
            }
        }
    }
}