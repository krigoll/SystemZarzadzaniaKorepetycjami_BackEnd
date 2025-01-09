using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class AssignmentTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateAssignment()
    {
        var assignment = new Assignment("Content of the assignment", "Answer", 1);

        Assert.AreEqual("Content of the assignment", assignment.Content);
        Assert.AreEqual("Answer", assignment.Answer);
        Assert.AreEqual(1, assignment.IdTest);
    }

    [Test]
    public void Constructor_InvalidContent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Assignment("", "Answer", 1));

        Assert.Throws<ArgumentException>(() =>
            new Assignment(new string('a', 501), "Answer", 1));
    }

    [Test]
    public void Constructor_InvalidAnswer_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Assignment("Valid content", "", 1));

        Assert.Throws<ArgumentException>(() =>
            new Assignment("Valid content", new string('a', 51), 1));
    }

    [Test]
    public void Constructor_InvalidIdTest_ShouldThrowArgumentException()
    {
        Assert.Throws<AggregateException>(() =>
            new Assignment("Valid content", "Answer", -1));
    }

    [Test]
    public void SetContent_ValidContent_ShouldUpdateContent()
    {
        var assignment = new Assignment("Initial content", "Answer", 1);
        assignment.SetContent("Updated content");

        Assert.AreEqual("Updated content", assignment.Content);
    }

    [Test]
    public void SetContent_InvalidContent_ShouldThrowArgumentException()
    {
        var assignment = new Assignment("Initial content", "Answer", 1);

        Assert.Throws<ArgumentException>(() => assignment.SetContent(""));
        Assert.Throws<ArgumentException>(() => assignment.SetContent(new string('a', 501)));
    }

    [Test]
    public void SetAnswer_ValidAnswer_ShouldUpdateAnswer()
    {
        var assignment = new Assignment("Content", "Initial answer", 1);
        assignment.SetAnswer("Updated answer");

        Assert.AreEqual("Updated answer", assignment.Answer);
    }

    [Test]
    public void SetAnswer_InvalidAnswer_ShouldThrowArgumentException()
    {
        var assignment = new Assignment("Content", "Answer", 1);

        Assert.Throws<ArgumentException>(() => assignment.SetAnswer(""));
        Assert.Throws<ArgumentException>(() => assignment.SetAnswer(new string('a', 51)));
    }

    [Test]
    public void SetAnswer_NullAnswer_ShouldSetAnswerToNull()
    {
        var assignment = new Assignment("Content", "Answer", 1);
        assignment.SetAnswer(null);

        Assert.IsNull(assignment.Answer);
    }

    [Test]
    public void SetIdTest_ValidIdTest_ShouldUpdateIdTest()
    {
        var assignment = new Assignment("Content", "Answer", 1);
        assignment.SetIdTest(5);

        Assert.AreEqual(5, assignment.IdTest);
    }

    [Test]
    public void SetIdTest_InvalidIdTest_ShouldThrowAggregateException()
    {
        var assignment = new Assignment("Content", "Answer", 1);

        Assert.Throws<AggregateException>(() => assignment.SetIdTest(-1));
    }
}