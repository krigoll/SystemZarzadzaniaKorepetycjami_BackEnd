using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class BanTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateBan()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");

        Assert.AreEqual(1, ban.IdPerson);
        Assert.AreEqual(7, ban.LengthInDays);
        Assert.AreEqual("Breaking the rules", ban.Reason);
        Assert.IsTrue(ban.StartTime <= DateTime.Now);
    }

    [Test]
    public void Constructor_InvalidIdPerson_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Ban(0, DateTime.Now, 7, "Breaking the rules"));

        Assert.Throws<ArgumentException>(() =>
            new Ban(-1, DateTime.Now, 7, "Breaking the rules"));
    }

    [Test]
    public void Constructor_InvalidLengthInDays_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Ban(1, DateTime.Now, 0, "Breaking the rules"));

        Assert.Throws<ArgumentException>(() =>
            new Ban(1, DateTime.Now, -5, "Breaking the rules"));
    }

    [Test]
    public void Constructor_InvalidReason_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Ban(1, DateTime.Now, 7, ""));

        Assert.Throws<ArgumentException>(() =>
            new Ban(1, DateTime.Now, 7, new string('A', 501))); // Exceeds 500 characters
    }

    [Test]
    public void SetIdBaned_ValidId_ShouldUpdateIdPerson()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");
        ban.SetIdBaned(10);

        Assert.AreEqual(10, ban.IdPerson);
    }

    [Test]
    public void SetIdBaned_InvalidId_ShouldThrowArgumentException()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");

        Assert.Throws<ArgumentException>(() => ban.SetIdBaned(0));
        Assert.Throws<ArgumentException>(() => ban.SetIdBaned(-1));
    }

    [Test]
    public void SetStartTime_ValidDate_ShouldUpdateStartTime()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");
        var newStartTime = DateTime.Now.AddDays(-1);

        ban.SetStartTime(newStartTime);

        Assert.AreEqual(newStartTime, ban.StartTime);
    }

    [Test]
    public void SetLenghtInDays_ValidLength_ShouldUpdateLength()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");
        ban.SetLenghtInDays(14);

        Assert.AreEqual(14, ban.LengthInDays);
    }

    [Test]
    public void SetLenghtInDays_InvalidLength_ShouldThrowArgumentException()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");

        Assert.Throws<ArgumentException>(() => ban.SetLenghtInDays(0));
        Assert.Throws<ArgumentException>(() => ban.SetLenghtInDays(-5));
    }

    [Test]
    public void SetReason_ValidReason_ShouldUpdateReason()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");
        var newReason = "Spamming in chat";

        ban.SetReason(newReason);

        Assert.AreEqual(newReason, ban.Reason);
    }

    [Test]
    public void SetReason_InvalidReason_ShouldThrowArgumentException()
    {
        var ban = new Ban(1, DateTime.Now, 7, "Breaking the rules");

        Assert.Throws<ArgumentException>(() => ban.SetReason(""));
        Assert.Throws<ArgumentException>(() => ban.SetReason(new string('A', 501))); // Exceeds 500 characters
    }
}