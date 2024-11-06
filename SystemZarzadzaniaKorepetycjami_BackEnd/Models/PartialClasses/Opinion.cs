namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Opinion
{
    public Opinion(int idStudent, int idTeacher, int rating, string content)
	{
		SetIdStudent(idStudent);
		SetIdTeacher(idTeacher);
		SetRating(rating);
		SetContent(content);
	}
	
	public void SetIdStudent(int idStudent)
	{
		if (idStudent <= 0)
			throw new ArgumentException("Invalid idStudent");
		IdStudent = idStudent;
	}
	
	public void SetIdTeacher(int idTeacher)
	{
		if (idTeacher <= 0)
			throw new ArgumentException("Invalid idTeacher");
		IdTeacher = idTeacher;
	}
	
	public void SetRating(int rating)
	{
		if (rating <= 0 || rating > 5)
			throw new ArgumentException("Invalid Rating");
		Rating = rating;
	}
	
	public void SetContent(string content)
	{
		if (content.Length>500)
			throw new ArgumentException("Invalid Content");
		Content = content;
	}
}