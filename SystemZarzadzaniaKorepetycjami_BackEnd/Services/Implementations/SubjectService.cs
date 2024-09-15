using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations
{
    public class SubjectService : ISubjectService
    {

        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPersonRepository _personRepository;

        public SubjectService(ISubjectRepository subjectRepository,ITeacherRepository teacherRepository, IPersonRepository personRepository)
        {
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _personRepository = personRepository;
        }
        public async Task<List<SubjectDTO>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllFullSubjects();
        }
        public async Task<List<SubjectTeacherDTO>> GetAllSubjectsEditAsync(string email)
        {
            if (await _teacherRepository.isTeacherByEmail(email))
            {
                var person = await _personRepository.FindPersonByEmailAsync(email);
                return await _subjectRepository.GetAllFullSubjectsByTeacherId(person.IdPerson);
            }
            return null;
        }
    }
}
