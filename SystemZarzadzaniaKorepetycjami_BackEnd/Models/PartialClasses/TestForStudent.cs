namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class TestForStudent
{
    public TestForStudent(int idTest, int idStudent)
    {
        if (idTest < 0 || idStudent < 0) throw new ArgumentException("Id is wrong");

        IdTest = idTest;
        IdStudent = idStudent;
        DateOfCreation = DateTime.Now;
        SetIdTestForStudentStatus(1);
    }

    public void SetIdTestForStudentStatus(int idTestForStudentStatus)
    {
        if (idTestForStudentStatus < 1 || idTestForStudentStatus > 3)
            throw new ArgumentException("Invalid idTestForStudentStatus");

        IdTestForStudentStatus = idTestForStudentStatus;
    }
}