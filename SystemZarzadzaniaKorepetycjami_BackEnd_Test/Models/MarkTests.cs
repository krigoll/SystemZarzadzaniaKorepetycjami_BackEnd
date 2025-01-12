using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class MarkTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateMark()
    {
        var mark = new Mark("Good job", true, 1);

        Assert.AreEqual("Good job", mark.Description);
        Assert.IsTrue(mark.Value);
        Assert.AreEqual(1, mark.IdStudentAnswer);
    }

    [Test]
    public void Constructor_InvalidDescription_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Mark(new string('A', 101), true, 1));
    }

    [Test]
    public void Constructor_InvalidIdStudentAnswer_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Mark("Good job", true, 0));
        Assert.Throws<ArgumentException>(() => new Mark("Good job", true, -1));
    }

    [Test]
    public void SetDescription_ValidInput_ShouldUpdateDescription()
    {
        var mark = new Mark("Good job", true, 1);
        mark.SetDescription("Excellent work");

        Assert.AreEqual("Excellent work", mark.Description);
    }

    [Test]
    public void SetDescription_InvalidInput_ShouldThrowArgumentException()
    {
        var mark = new Mark("Good job", true, 1);
        Assert.Throws<ArgumentException>(() => mark.SetDescription(new string('B', 101)));
    }

    [Test]
    public void SetValue_ShouldUpdateValue()
    {
        var mark = new Mark("Good job", true, 1);

        mark.SetValue(false);
        Assert.IsFalse(mark.Value);

        mark.SetValue(true);
        Assert.IsTrue(mark.Value);
    }
}