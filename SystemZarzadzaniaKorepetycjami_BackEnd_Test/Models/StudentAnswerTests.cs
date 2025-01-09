using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class StudentAnswerTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateStudentAnswer()
    {
        var studentAnswer = new StudentAnswer("Correct Answer", 1, 1);

        Assert.AreEqual("Correct Answer", studentAnswer.Answer);
        Assert.AreEqual(1, studentAnswer.IdTestForStudent);
        Assert.AreEqual(1, studentAnswer.IdAssignment);
    }

    [Test]
    public void Constructor_InvalidIdTestForStudent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new StudentAnswer("Answer", -1, 1));
    }

    [Test]
    public void Constructor_InvalidIdAssignment_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new StudentAnswer("Answer", 1, -1));
    }

    [Test]
    public void Constructor_NullAnswer_ShouldCreateWithNullAnswer()
    {
        var studentAnswer = new StudentAnswer(null, 1, 1);

        Assert.IsNull(studentAnswer.Answer);
    }

    [Test]
    public void SetAnswer_ValidInput_ShouldUpdateAnswer()
    {
        var studentAnswer = new StudentAnswer("Initial Answer", 1, 1);
        studentAnswer.SetAnswer("Updated Answer");

        Assert.AreEqual("Updated Answer", studentAnswer.Answer);
    }

    [Test]
    public void SetAnswer_Null_ShouldAllowNullAnswer()
    {
        var studentAnswer = new StudentAnswer("Initial Answer", 1, 1);
        studentAnswer.SetAnswer(null);

        Assert.IsNull(studentAnswer.Answer);
    }
}