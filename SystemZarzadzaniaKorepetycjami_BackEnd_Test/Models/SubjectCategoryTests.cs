using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class SubjectCategoryTests
{
    [Test]
    public void Constructor_ValidParameters_ShouldCreateSubjectCategory()
    {
        var subjectCategory = new SubjectCategory("Science", 1);

        Assert.AreEqual("Science", subjectCategory.Name);
        Assert.AreEqual(1, subjectCategory.IdSubject);
    }

    [Test]
    public void Constructor_EmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectCategory("", 1));
    }

    [Test]
    public void Constructor_NullName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectCategory(null, 1));
    }

    [Test]
    public void Constructor_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var longName = new string('A', 51); // 51 characters
        Assert.Throws<ArgumentException>(() => new SubjectCategory(longName, 1));
    }

    [Test]
    public void Constructor_InvalidIdSubject_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SubjectCategory("Science", 0));
    }

    [Test]
    public void SetName_ValidName_ShouldUpdateName()
    {
        var subjectCategory = new SubjectCategory("Initial Name", 1);
        subjectCategory.SetName("Updated Name");

        Assert.AreEqual("Updated Name", subjectCategory.Name);
    }

    [Test]
    public void SetName_EmptyName_ShouldThrowArgumentException()
    {
        var subjectCategory = new SubjectCategory("Initial Name", 1);
        Assert.Throws<ArgumentException>(() => subjectCategory.SetName(""));
    }

    [Test]
    public void SetName_NullName_ShouldThrowArgumentException()
    {
        var subjectCategory = new SubjectCategory("Initial Name", 1);
        Assert.Throws<ArgumentException>(() => subjectCategory.SetName(null));
    }

    [Test]
    public void SetName_NameExceedingMaxLength_ShouldThrowArgumentException()
    {
        var subjectCategory = new SubjectCategory("Initial Name", 1);
        var longName = new string('A', 51);
        Assert.Throws<ArgumentException>(() => subjectCategory.SetName(longName));
    }

    [Test]
    public void SetIdSubject_ValidId_ShouldUpdateIdSubject()
    {
        var subjectCategory = new SubjectCategory("Science", 1);
        subjectCategory.SetIdSubject(2);

        Assert.AreEqual(2, subjectCategory.IdSubject);
    }

    [Test]
    public void SetIdSubject_InvalidId_ShouldThrowArgumentException()
    {
        var subjectCategory = new SubjectCategory("Science", 1);
        Assert.Throws<ArgumentException>(() => subjectCategory.SetIdSubject(0));
    }
}