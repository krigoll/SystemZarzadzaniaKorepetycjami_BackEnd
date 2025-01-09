namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class TestForStudent
{
    public TestForStudent(int idTest, int idStudent)
    {
        if (idTest < 0 || idStudent < 0) throw new ArgumentException("Id is wrong");

        IdTest = idTest;
        IdStudent = idStudent;
        DateOfCreation = DateTime.Now;
    }
}