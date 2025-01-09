namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class StudentAnswer
{
    public StudentAnswer(string answer, int idTestForStudent, int idAssignment)
    {
        SetAnswer(answer);
        if (idTestForStudent < 0) throw new ArgumentException("Wrong Id Test For Student");

        IdTestForStudent = idTestForStudent;
        if (idAssignment < 0) throw new ArgumentException("Wrong Id Assignment");

        IdAssignment = idAssignment;
    }

    public void SetAnswer(string answer)
    {
        Answer = answer;
    }
}