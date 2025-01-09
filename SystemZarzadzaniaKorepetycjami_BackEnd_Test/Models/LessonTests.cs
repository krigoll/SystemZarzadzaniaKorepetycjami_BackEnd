using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class LessonTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateLesson()
    {
        var startDate = DateTime.Now;
        var lesson = new Lesson(1, 2, 3, 1, startDate, 60);

        Assert.AreEqual(1, lesson.IdStudent);
        Assert.AreEqual(2, lesson.IdTeacher);
        Assert.AreEqual(3, lesson.IdSubjectLevel);
        Assert.AreEqual(1, lesson.IdLessonStatus);
        Assert.AreEqual(startDate, lesson.StartDate);
        Assert.AreEqual(60, lesson.DurationInMinutes);
    }

    [Test]
    public void Constructor_InvalidIdStudent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Lesson(0, 2, 3, 1, DateTime.Now, 60));
        Assert.Throws<ArgumentException>(() => new Lesson(-1, 2, 3, 1, DateTime.Now, 60));
    }

    [Test]
    public void Constructor_InvalidIdTeacher_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Lesson(1, 0, 3, 1, DateTime.Now, 60));
        Assert.Throws<ArgumentException>(() => new Lesson(1, -2, 3, 1, DateTime.Now, 60));
    }

    [Test]
    public void Constructor_InvalidIdSubjectLevel_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, 0, 1, DateTime.Now, 60));
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, -3, 1, DateTime.Now, 60));
    }

    [Test]
    public void Constructor_InvalidIdLessonStatus_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, 3, 0, DateTime.Now, 60));
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, 3, -1, DateTime.Now, 60));
    }

    [Test]
    public void Constructor_InvalidDuration_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, 3, 1, DateTime.Now, 0));
        Assert.Throws<ArgumentException>(() => new Lesson(1, 2, 3, 1, DateTime.Now, -15));
    }

    [Test]
    public void SetIdStudent_ValidInput_ShouldUpdateIdStudent()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        lesson.SetIdStudent(5);

        Assert.AreEqual(5, lesson.IdStudent);
    }

    [Test]
    public void SetIdStudent_InvalidInput_ShouldThrowArgumentException()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);

        Assert.Throws<ArgumentException>(() => lesson.SetIdStudent(0));
        Assert.Throws<ArgumentException>(() => lesson.SetIdStudent(-1));
    }

    [Test]
    public void SetIdTeacher_ValidInput_ShouldUpdateIdTeacher()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        lesson.SetIdTeacher(10);

        Assert.AreEqual(10, lesson.IdTeacher);
    }

    [Test]
    public void SetIdTeacher_InvalidInput_ShouldThrowArgumentException()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);

        Assert.Throws<ArgumentException>(() => lesson.SetIdTeacher(0));
        Assert.Throws<ArgumentException>(() => lesson.SetIdTeacher(-1));
    }

    [Test]
    public void SetIdSubjectLevel_ValidInput_ShouldUpdateIdSubjectLevel()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        lesson.SetIdSubjectLevel(7);

        Assert.AreEqual(7, lesson.IdSubjectLevel);
    }

    [Test]
    public void SetIdSubjectLevel_InvalidInput_ShouldThrowArgumentException()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);

        Assert.Throws<ArgumentException>(() => lesson.SetIdSubjectLevel(0));
        Assert.Throws<ArgumentException>(() => lesson.SetIdSubjectLevel(-3));
    }

    [Test]
    public void SetIdLessonStatus_ValidInput_ShouldUpdateIdLessonStatus()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        lesson.SetIdLessonStatus(2);

        Assert.AreEqual(2, lesson.IdLessonStatus);
    }

    [Test]
    public void SetIdLessonStatus_InvalidInput_ShouldThrowArgumentException()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);

        Assert.Throws<ArgumentException>(() => lesson.SetIdLessonStatus(0));
        Assert.Throws<ArgumentException>(() => lesson.SetIdLessonStatus(-2));
    }

    [Test]
    public void SetStartDate_ValidInput_ShouldUpdateStartDate()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        var newDate = DateTime.Now.AddDays(1);

        lesson.SetStartDate(newDate);

        Assert.AreEqual(newDate, lesson.StartDate);
    }

    [Test]
    public void SetDurationInMinutes_ValidInput_ShouldUpdateDuration()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);
        lesson.SetDurationInMinutes(90);

        Assert.AreEqual(90, lesson.DurationInMinutes);
    }

    [Test]
    public void SetDurationInMinutes_InvalidInput_ShouldThrowArgumentException()
    {
        var lesson = new Lesson(1, 2, 3, 1, DateTime.Now, 60);

        Assert.Throws<ArgumentException>(() => lesson.SetDurationInMinutes(0));
        Assert.Throws<ArgumentException>(() => lesson.SetDurationInMinutes(-10));
    }
}