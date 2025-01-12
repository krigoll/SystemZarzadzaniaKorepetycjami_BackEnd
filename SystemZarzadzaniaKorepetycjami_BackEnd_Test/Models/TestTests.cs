using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

public class TestTests
{
    [Test]
    public void TestConstructor_ValidInput_ShouldSetPropertiesCorrectly()
    {
        var idTeacher = 1;
        var title = "Test Title";

        var test = new Test(idTeacher, title);

        Assert.AreEqual(idTeacher, test.IdTeacher);
        Assert.AreEqual(title, test.Title);
    }

    [Test]
    public void TestConstructor_InvalidTitle_ThrowsArgumentException()
    {
        var idTeacher = 1;
        var invalidTitle = "";

        var ex = Assert.Throws<ArgumentException>(() => new Test(idTeacher, invalidTitle));
        Assert.That(ex.Message, Does.Contain("Invalid Title"));
    }

    [Test]
    public void TestConstructor_InvalidIdTeacher_ThrowsArgumentException()
    {
        var invalidIdTeacher = 0;
        var title = "Valid Title";

        var ex = Assert.Throws<ArgumentException>(() => new Test(invalidIdTeacher, title));
        Assert.That(ex.Message, Does.Contain("Invalid Id Teacher"));
    }

    [Test]
    public void SetTitle_ValidInput_ShouldSetTitle()
    {
        var test = new Test(1, "Initial Title");

        test.SetTitle("Updated Title");

        Assert.AreEqual("Updated Title", test.Title);
    }

    [Test]
    public void SetTitle_InvalidTitle_ThrowsArgumentException()
    {
        var test = new Test(1, "Initial Title");
        var ex = Assert.Throws<ArgumentException>(() => test.SetTitle(""));
        Assert.That(ex.Message, Does.Contain("Invalid Title"));
    }

    [Test]
    public void SetIdTeacher_ValidInput_ShouldSetIdTeacher()
    {
        var test = new Test(1, "Test Title");
        test.SetIdTeacher(2);
        Assert.AreEqual(2, test.IdTeacher);
    }

    [Test]
    public void SetIdTeacher_InvalidIdTeacher_ThrowsArgumentException()
    {
        var test = new Test(1, "Test Title");
        var ex = Assert.Throws<ArgumentException>(() => test.SetIdTeacher(0));
        Assert.That(ex.Message, Does.Contain("Invalid Id Teacher"));
    }
}