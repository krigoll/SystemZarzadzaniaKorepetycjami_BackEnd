using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class OpinionTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateOpinion()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");

        Assert.AreEqual(1, opinion.IdStudent);
        Assert.AreEqual(2, opinion.IdTeacher);
        Assert.AreEqual(5, opinion.Rating);
        Assert.AreEqual("Great teacher!", opinion.Content);
    }

    [Test]
    public void Constructor_InvalidIdStudent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Opinion(0, 2, 5, "Great teacher!"));
        Assert.Throws<ArgumentException>(() => new Opinion(-1, 2, 5, "Great teacher!"));
    }

    [Test]
    public void Constructor_InvalidIdTeacher_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Opinion(1, 0, 5, "Great teacher!"));
        Assert.Throws<ArgumentException>(() => new Opinion(1, -1, 5, "Great teacher!"));
    }

    [Test]
    public void Constructor_InvalidRating_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Opinion(1, 2, 0, "Great teacher!"));
        Assert.Throws<ArgumentException>(() => new Opinion(1, 2, 6, "Great teacher!"));
        Assert.Throws<ArgumentException>(() => new Opinion(1, 2, -1, "Great teacher!"));
    }

    [Test]
    public void Constructor_InvalidContent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Opinion(1, 2, 5, new string('A', 501)));
    }

    [Test]
    public void SetIdStudent_ValidInput_ShouldUpdateIdStudent()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");
        opinion.SetIdStudent(3);

        Assert.AreEqual(3, opinion.IdStudent);
    }

    [Test]
    public void SetIdStudent_InvalidInput_ShouldThrowArgumentException()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");

        Assert.Throws<ArgumentException>(() => opinion.SetIdStudent(0));
        Assert.Throws<ArgumentException>(() => opinion.SetIdStudent(-1));
    }

    [Test]
    public void SetIdTeacher_ValidInput_ShouldUpdateIdTeacher()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");
        opinion.SetIdTeacher(4);

        Assert.AreEqual(4, opinion.IdTeacher);
    }

    [Test]
    public void SetIdTeacher_InvalidInput_ShouldThrowArgumentException()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");

        Assert.Throws<ArgumentException>(() => opinion.SetIdTeacher(0));
        Assert.Throws<ArgumentException>(() => opinion.SetIdTeacher(-1));
    }

    [Test]
    public void SetRating_ValidInput_ShouldUpdateRating()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");
        opinion.SetRating(4);

        Assert.AreEqual(4, opinion.Rating);
    }

    [Test]
    public void SetRating_InvalidInput_ShouldThrowArgumentException()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");

        Assert.Throws<ArgumentException>(() => opinion.SetRating(0));
        Assert.Throws<ArgumentException>(() => opinion.SetRating(6));
        Assert.Throws<ArgumentException>(() => opinion.SetRating(-1));
    }

    [Test]
    public void SetContent_ValidInput_ShouldUpdateContent()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");
        opinion.SetContent("Updated opinion");

        Assert.AreEqual("Updated opinion", opinion.Content);
    }

    [Test]
    public void SetContent_InvalidInput_ShouldThrowArgumentException()
    {
        var opinion = new Opinion(1, 2, 5, "Great teacher!");

        Assert.Throws<ArgumentException>(() => opinion.SetContent(new string('A', 501)));
    }
}