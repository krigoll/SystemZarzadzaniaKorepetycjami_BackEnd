namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Assignment
{
    public Assignment(string content, string answer, int idTest)
    {
        SetContent(content);
        SetAnswer(answer);
        SetIdTest(idTest);
    }

    public void SetContent(string content)
    {
        if (content.Length == 0 || content.Length > 500)
            throw new ArgumentException("Invalid Content");
        Content = content;
    }

    public void SetAnswer(string answer)
    {
        if (answer != null && (answer.Length == 0 || answer.Length > 50))
            throw new ArgumentException("Invalid Answer");
        Answer = answer;
    }

    public void SetIdTest(int idTest)
    {
        if (idTest < 0)
            throw new AggregateException("Invalid Id Test");
        IdTest = idTest;
    }
}