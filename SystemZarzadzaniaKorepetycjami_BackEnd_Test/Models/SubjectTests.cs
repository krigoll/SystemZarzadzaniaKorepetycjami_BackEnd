using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class SubjectTests
{
    [Test]
    public void Constructor_ValidName_ShouldCreateSubject()
    {
        var subject = new Subject("Mathematics");

        Assert.AreEqual("Mathematics", subject.Name);
    }

    [Test]
    public void Constructor_EmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Subject(""));
    }

    [Test]
    public void Constructor_NullName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Subject(null));
    }

    [Test]
    public void Constructor_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var longName = new string('A', 51); // 51 characters
        Assert.Throws<ArgumentException>(() => new Subject(longName));
    }

    [Test]
    public void SetName_ValidName_ShouldUpdateName()
    {
        var subject = new Subject("Initial Name");
        subject.SetName("Updated Name");

        Assert.AreEqual("Updated Name", subject.Name);
    }

    [Test]
    public void SetName_EmptyName_ShouldThrowArgumentException()
    {
        var subject = new Subject("Initial Name");
        Assert.Throws<ArgumentException>(() => subject.SetName(""));
    }

    [Test]
    public void SetName_NullName_ShouldThrowArgumentException()
    {
        var subject = new Subject("Initial Name");
        Assert.Throws<ArgumentException>(() => subject.SetName(null));
    }

    [Test]
    public void SetName_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var subject = new Subject("Initial Name");
        var longName = new string('A', 51);
        Assert.Throws<ArgumentException>(() => subject.SetName(longName));
    }
}