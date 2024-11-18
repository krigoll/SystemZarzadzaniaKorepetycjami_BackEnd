using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public class OpinionService : IOpinionService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IOpinionRepository _opinionRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public OpinionService(IOpinionRepository opinionRepository, IStudentRepository studentRepository,
        ITeacherRepository teacherRepository, IPersonRepository personRepository, ILessonRepository lessonRepository)
    {
        _opinionRepository = opinionRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _personRepository = personRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<List<OpinionDTO>> GetOpinionsByStudentEmailAsync(string email)
    {
        var student = await _studentRepository.GetStudentByEmailAsync(email);
        if (student == null) return null;

        var opinions = await _opinionRepository.GetOpinionsByStudentAsync(student.IdStudent);
        return opinions;
    }

    public async Task<List<OpinionDTO>> GetOpinionsByTeacherIdAsync(int teacherId)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(teacherId);
        if (teacher == null) return null;

        var opinions = await _opinionRepository.GetOpinionsByTeacherAsync(teacher.IdTeacher);
        return opinions;
    }

    public async Task<List<OpinionDTO>> GetOpinionsByTeacherEmailAsync(string email)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(email);
        if (teacher == null) return null;

        var opinions = await _opinionRepository.GetOpinionsByTeacherAsync(teacher.IdTeacher);
        return opinions;
    }

    public async Task<OpinionDetailsDTO> GetOpinionDetailsByIdAsync(int opinionId)
    {
        var opinion = await _opinionRepository.GetOpinionByIdAsync(opinionId);
        var studentPerson = await _personRepository.FindPersonByIdAsync(opinion.IdStudent);
        var techerPerson = await _personRepository.FindPersonByIdAsync(opinion.IdTeacher);

        var opinionDetais = new OpinionDetailsDTO
        {
            OpinionId = opinion.IdOpinion,
            StudentName = "${studentPerson.Name} {studentPerson.Surname}",
            TeacherName = "${techerPerson.Name} {techerPerson.Surname}",
            TeacherId = techerPerson.IdPerson,
            Rating = opinion.Rating,
            Content = opinion.Content
        };

        return opinionDetais;
    }

    public async Task<OpinionStatus> CreateOpinionAsync(OpinionCreateDTO opinionCreateDTO)
    {
        try
        {
            Console.WriteLine(opinionCreateDTO);
            var student = await _studentRepository.GetStudentByEmailAsync(opinionCreateDTO.StudentEmail);
            var teacher = await _teacherRepository.GetTeacherByIdAsync(opinionCreateDTO.IdTeacher);
            if (student == null || teacher == null) return OpinionStatus.INVALID_STUDENT_OR_TEACHER;

            if (!await _lessonRepository.AreThisTeacherTeachThisStudentAsync(teacher.IdTeacher, student.IdStudent))
                return OpinionStatus.CAN_NOT_OPINION_TEACHER_WITCH_NOT_TEACHED_YOU;

            var newOpinion = new Opinion(
                student.IdStudent,
                teacher.IdTeacher,
                opinionCreateDTO.Rating,
                opinionCreateDTO.Content
            );
            await _opinionRepository.AddOpinionAsync(newOpinion);
            return OpinionStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return OpinionStatus.INVALID_OPINION;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OpinionStatus.SERVER_ERROR;
        }
    }

    public async Task<OpinionStatus> UpdateOpinionByIdAsync(int opinionId, OpinionCreateDTO opinionCreateDTO)
    {
        try
        {
            var student = await _studentRepository.GetStudentByEmailAsync(opinionCreateDTO.StudentEmail);
            var teacher = await _teacherRepository.GetTeacherByIdAsync(opinionCreateDTO.IdTeacher);
            if (student == null || teacher == null) return OpinionStatus.INVALID_STUDENT_OR_TEACHER;

            if (!await _lessonRepository.AreThisTeacherTeachThisStudentAsync(teacher.IdTeacher, student.IdStudent))
                return OpinionStatus.CAN_NOT_OPINION_TEACHER_WITCH_NOT_TEACHED_YOU;

            var opinion = await _opinionRepository.GetOpinionByIdAsync(opinionId);
            opinion.SetIdStudent(student.IdStudent);
            opinion.SetIdTeacher(teacher.IdTeacher);
            opinion.SetRating(opinionCreateDTO.Rating);
            opinion.SetContent(opinionCreateDTO.Content);
            await _opinionRepository.UpdateOpinionAsync(opinion);
            return OpinionStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return OpinionStatus.INVALID_OPINION;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OpinionStatus.SERVER_ERROR;
        }
    }

    public async Task<bool> DeleteOpinionByIdAsync(int opinionId)
    {
        var opinion = await _opinionRepository.GetOpinionByIdAsync(opinionId);
        if (opinion == null)
            return false;
        await _opinionRepository.DeleteOpinionByIdAsync(opinion);
        return true;
    }
}