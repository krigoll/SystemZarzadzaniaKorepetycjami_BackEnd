using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class AvailabilityTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateAvailability()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));

        Assert.AreEqual(1, availability.IdTeacher);
        Assert.AreEqual(2, availability.IdDayOfTheWeek);
        Assert.AreEqual(new TimeOnly(9, 0), availability.StartTime);
        Assert.AreEqual(new TimeOnly(12, 0), availability.EndTime);
    }

    [Test]
    public void Constructor_InvalidIdTeacher_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Availability(0, 2, new TimeOnly(9, 0), new TimeOnly(12, 0)));

        Assert.Throws<ArgumentException>(() =>
            new Availability(-1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0)));
    }

    [Test]
    public void Constructor_InvalidIdDayOfTheWeek_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Availability(1, 0, new TimeOnly(9, 0), new TimeOnly(12, 0)));

        Assert.Throws<ArgumentException>(() =>
            new Availability(1, 8, new TimeOnly(9, 0), new TimeOnly(12, 0)));
    }

    [Test]
    public void Constructor_InvalidTimeRange_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Availability(1, 2, new TimeOnly(12, 0), new TimeOnly(9, 0)));
    }

    [Test]
    public void SetIdTeacher_ValidIdTeacher_ShouldUpdateIdTeacher()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));
        availability.SetIdTeacher(10);

        Assert.AreEqual(10, availability.IdTeacher);
    }

    [Test]
    public void SetIdTeacher_InvalidIdTeacher_ShouldThrowArgumentException()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));

        Assert.Throws<ArgumentException>(() => availability.SetIdTeacher(0));
        Assert.Throws<ArgumentException>(() => availability.SetIdTeacher(-1));
    }

    [Test]
    public void SetIdDayOfTheWeek_ValidIdDayOfTheWeek_ShouldUpdateIdDayOfTheWeek()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));
        availability.SetIdDayOfTheWeek(5);

        Assert.AreEqual(5, availability.IdDayOfTheWeek);
    }

    [Test]
    public void SetIdDayOfTheWeek_InvalidIdDayOfTheWeek_ShouldThrowArgumentException()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));

        Assert.Throws<ArgumentException>(() => availability.SetIdDayOfTheWeek(0));
        Assert.Throws<ArgumentException>(() => availability.SetIdDayOfTheWeek(8));
    }

    [Test]
    public void SetTime_ValidTimeRange_ShouldUpdateStartTimeAndEndTime()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));
        availability.SetTime(new TimeOnly(10, 0), new TimeOnly(13, 0));

        Assert.AreEqual(new TimeOnly(10, 0), availability.StartTime);
        Assert.AreEqual(new TimeOnly(13, 0), availability.EndTime);
    }

    [Test]
    public void SetTime_InvalidTimeRange_ShouldThrowArgumentException()
    {
        var availability = new Availability(1, 2, new TimeOnly(9, 0), new TimeOnly(12, 0));

        Assert.Throws<ArgumentException>(() => availability.SetTime(new TimeOnly(14, 0), new TimeOnly(10, 0)));
    }
}