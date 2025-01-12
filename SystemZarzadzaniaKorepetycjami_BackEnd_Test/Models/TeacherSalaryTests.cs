using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class TeacherSalaryTests
{
    [Test]
    public void Constructor_ValidParameters_ShouldCreateTeacherSalary()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);

        Assert.AreEqual(50.0m, teacherSalary.HourlyRate);
        Assert.AreEqual(1, teacherSalary.IdTeacher);
        Assert.AreEqual(1, teacherSalary.IdSubject);
    }

    [Test]
    public void Constructor_NegativeHourlyRate_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TeacherSalary(-10.0m, 1, 1));
    }

    [Test]
    public void Constructor_NegativeIdTeacher_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TeacherSalary(50.0m, -1, 1));
    }

    [Test]
    public void Constructor_NegativeIdSubject_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new TeacherSalary(50.0m, 1, -1));
    }

    [Test]
    public void SetHourlyRate_ValidValue_ShouldUpdateHourlyRate()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        teacherSalary.SetHourlyRate(75.0m);

        Assert.AreEqual(75.0m, teacherSalary.HourlyRate);
    }

    [Test]
    public void SetHourlyRate_NegativeValue_ShouldThrowArgumentException()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        Assert.Throws<ArgumentException>(() => teacherSalary.SetHourlyRate(-10.0m));
    }

    [Test]
    public void SetIdTeacher_ValidValue_ShouldUpdateIdTeacher()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        teacherSalary.SetIdTeacher(2);

        Assert.AreEqual(2, teacherSalary.IdTeacher);
    }

    [Test]
    public void SetIdTeacher_NegativeValue_ShouldThrowArgumentException()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        Assert.Throws<ArgumentException>(() => teacherSalary.SetIdTeacher(-1));
    }

    [Test]
    public void SetIdSubject_ValidValue_ShouldUpdateIdSubject()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        teacherSalary.SetIdSubject(3);

        Assert.AreEqual(3, teacherSalary.IdSubject);
    }

    [Test]
    public void SetIdSubject_NegativeValue_ShouldThrowArgumentException()
    {
        var teacherSalary = new TeacherSalary(50.0m, 1, 1);
        Assert.Throws<ArgumentException>(() => teacherSalary.SetIdSubject(-1));
    }
}