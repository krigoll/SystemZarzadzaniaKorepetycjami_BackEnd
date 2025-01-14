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
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ILessonRepository _lessonRepository;

        private readonly IPersonRepository _personRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectLevelRepository _subjectLevelRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherSalaryRepository _teacherSalaryRepository;

        public SingUpToLessonService(ILessonRepository lessonRepository, IPersonRepository personRepository,
            IStudentRepository studentRepository, ISubjectLevelRepository subjectLevelRepository,
            ITeacherRepository teacherRepository, IAvailabilityRepository availabilityRepository,
            ITeacherSalaryRepository teacherSalaryRepository)
        {
            _lessonRepository = lessonRepository;
            _personRepository = personRepository;
            _studentRepository = studentRepository;
            _subjectLevelRepository = subjectLevelRepository;
            _teacherRepository = teacherRepository;
            _availabilityRepository = availabilityRepository;
            _teacherSalaryRepository = teacherSalaryRepository;
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

                if (startDate < DateTime.Now)
                    return SingUpToLessonStatus.INVALID_LESSON;

                var lessonStatusId = 1;

                var teacherSalary = await _teacherSalaryRepository.FindByTeacherAndSubjectAsync(teacher.IdPerson,
                    singUpToLessonDTO.SubjectLevelId);

                var cost = decimal.ToInt32(teacherSalary.HourlyRate * singUpToLessonDTO.DurationInMinutes / 60);

                var lesson = new Lesson(student.IdPerson, teacher.IdPerson, singUpToLessonDTO.SubjectLevelId,
                    lessonStatusId,
                    startDate, singUpToLessonDTO.DurationInMinutes, cost);

                if (!await _availabilityRepository.IsThisLessonInAvailabilityTime(lesson))
                    return SingUpToLessonStatus.NOT_AVAILABILITY;

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