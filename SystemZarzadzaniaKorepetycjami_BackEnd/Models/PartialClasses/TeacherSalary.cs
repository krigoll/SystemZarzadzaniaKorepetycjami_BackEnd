namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class TeacherSalary
{
    public TeacherSalary(decimal hourlyRate, int idTeacher, int idSubject)
    {
        SetHourlyRate(hourlyRate);
        SetIdSubject(idSubject);
        SetIdTeacher(idTeacher);
    }

    public void SetHourlyRate(decimal hourlyRate)
    {
        if  (hourlyRate<0)
            throw new ArgumentException("HourlyRate can not by on minus.");
        HourlyRate = hourlyRate;    
    }

    public void SetIdSubject(int idSubject)
    {
        if  (idSubject<0)
            throw new ArgumentException("IdSubject can not by on minus.");
        IdSubject = idSubject;   
    }

    public void SetIdTeacher(int idTeacher)
    {
        if  (idTeacher<0)
            throw new ArgumentException("IdTeacher can not by on minus.");
        IdTeacher = idTeacher;   
    }
    
}