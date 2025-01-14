namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public class TestForStudentStatus
{
    private TestForStudentStatus()
    {
        TestForStudent = new HashSet<TestForStudent>();
    }

    public int IdTestForStudentStatus { get; }
    public string Status { get; }

    public virtual ICollection<TestForStudent> TestForStudent { get; private set; }
}