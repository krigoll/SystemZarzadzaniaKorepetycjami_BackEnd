namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Test
{
    public Test(int idTeacher, string title)
    {
        SetTitle(title);
        SetIdTeacher(idTeacher);
    }

    public void SetTitle(string title)
    {
        if (title.Length == 0 || title.Length > 50)
            throw new ArgumentException("Invalid Title");
        Title = title;
    }

    public void SetIdTeacher(int idTeacher)
    {
        if (idTeacher <= 0)
            throw new ArgumentException("Invalid Id Teacher");
        IdTeacher = idTeacher;
    }
}