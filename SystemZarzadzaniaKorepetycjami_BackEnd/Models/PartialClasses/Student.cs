namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Student
{
    public Student(int IdStudent) 
    { 
        SetIdStudent(IdStudent);
    }

    public void SetIdStudent(int idStudent)
    {
        IdStudent = idStudent;
    }

}