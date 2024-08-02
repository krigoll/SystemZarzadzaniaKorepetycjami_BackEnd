namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Teacher
{
    public Teacher(int IdTeacher)
    {
        SetIdTeacher(IdTeacher);
    }

    public void SetIdTeacher(int idTeacher)
    {
        IdTeacher = idTeacher;
    }
}