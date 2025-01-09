using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class SubjectLevelTests
{
    [Test]
    public void Constructor_ValidParameters_ShouldCreateSubjectLevel()
    {
        var subjectLevel = new SubjectLevel("Advanced Math", 1);

        Assert.AreEqual("Advanced Math", subjectLevel.Name);
        Assert.AreEqual(1, subjectLevel.IdSubjectCategory);
    }

    [Test]
    public void Constructor_EmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectLevel("", 1));
    }

    [Test]
    public void Constructor_NullName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectLevel(null, 1));
    }

    [Test]
    public void Constructor_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var longName = new string('A', 51);
        Assert.Throws<ArgumentException>(() => new SubjectLevel(longName, 1));
    }

    [Test]
    public void Constructor_InvalidIdSubjectCategory_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectLevel("Math", 0));
    }

    [Test]
    public void SetName_ValidName_ShouldUpdateName()
    {
        var subjectLevel = new SubjectLevel("Initial Name", 1);
        subjectLevel.SetName("Updated Name");

        Assert.AreEqual("Updated Name", subjectLevel.Name);
    }

    [Test]
    public void SetName_EmptyName_ShouldThrowArgumentException()
    {
        var subjectLevel = new SubjectLevel("Initial Name", 1);
        Assert.Throws<ArgumentException>(() => subjectLevel.SetName(""));
    }

    [Test]
    public void SetName_NullName_ShouldThrowArgumentException()
    {
        var subjectLevel = new SubjectLevel("Initial Name", 1);
        Assert.Throws<ArgumentException>(() => subjectLevel.SetName(null));
    }

    [Test]
    public void SetName_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var subjectLevel = new SubjectLevel("Initial Name", 1);
        var longName = new string('A', 51); // 51 characters
        Assert.Throws<ArgumentException>(() => subjectLevel.SetName(longName));
    }

    [Test]
    public void SetIdSubjectCategory_ValidId_ShouldUpdateIdSubjectCategory()
    {
        var subjectLevel = new SubjectLevel("Math", 1);
        subjectLevel.SetIdSubjectCategory(2);

        Assert.AreEqual(2, subjectLevel.IdSubjectCategory);
    }

    [Test]
    public void SetIdSubjectCategory_InvalidId_ShouldThrowArgumentException()
    {
        var subjectLevel = new SubjectLevel("Math", 1);
        Assert.Throws<ArgumentException>(() => subjectLevel.SetIdSubjectCategory(0));
    }
}