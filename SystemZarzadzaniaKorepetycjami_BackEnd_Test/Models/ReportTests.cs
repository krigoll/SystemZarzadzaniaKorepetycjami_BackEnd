using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class ReportTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateReport()
    {
        var report = new Report("Spam report", "This is spam content.", 1, DateTime.Now);

        Assert.AreEqual("Spam report", report.Title);
        Assert.AreEqual("This is spam content.", report.Content);
        Assert.AreEqual(1, report.Sender);
        Assert.IsFalse(report.Dealt);
    }

    [Test]
    public void Constructor_InvalidTitle_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Report("", "Valid content", 1, DateTime.Now));
        Assert.Throws<ArgumentException>(() => new Report(new string('A', 51), "Valid content", 1, DateTime.Now));
    }

    [Test]
    public void Constructor_InvalidContent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Report("Valid Title", "", 1, DateTime.Now));
        Assert.Throws<ArgumentException>(() => new Report("Valid Title", new string('A', 501), 1, DateTime.Now));
    }

    [Test]
    public void Constructor_InvalidSender_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Report("Valid Title", "Valid Content", 0, DateTime.Now));
        Assert.Throws<ArgumentException>(() => new Report("Valid Title", "Valid Content", -1, DateTime.Now));
    }

    [Test]
    public void SetTitle_ValidInput_ShouldUpdateTitle()
    {
        var report = new Report("Initial Title", "Content", 1, DateTime.Now);
        report.SetTitle("Updated Title");

        Assert.AreEqual("Updated Title", report.Title);
    }

    [Test]
    public void SetTitle_InvalidInput_ShouldThrowArgumentException()
    {
        var report = new Report("Initial Title", "Content", 1, DateTime.Now);

        Assert.Throws<ArgumentException>(() => report.SetTitle(""));
        Assert.Throws<ArgumentException>(() => report.SetTitle(new string('A', 51)));
    }

    [Test]
    public void SetContent_ValidInput_ShouldUpdateContent()
    {
        var report = new Report("Title", "Initial Content", 1, DateTime.Now);
        report.SetContent("Updated Content");

        Assert.AreEqual("Updated Content", report.Content);
    }

    [Test]
    public void SetContent_InvalidInput_ShouldThrowArgumentException()
    {
        var report = new Report("Title", "Initial Content", 1, DateTime.Now);

        Assert.Throws<ArgumentException>(() => report.SetContent(""));
        Assert.Throws<ArgumentException>(() => report.SetContent(new string('A', 501)));
    }

    [Test]
    public void SetSender_ValidInput_ShouldUpdateSender()
    {
        var report = new Report("Title", "Content", 1, DateTime.Now);
        report.SetSender(2);

        Assert.AreEqual(2, report.Sender);
    }

    [Test]
    public void SetSender_InvalidInput_ShouldThrowArgumentException()
    {
        var report = new Report("Title", "Content", 1, DateTime.Now);

        Assert.Throws<ArgumentException>(() => report.SetSender(0));
        Assert.Throws<ArgumentException>(() => report.SetSender(-1));
    }

    [Test]
    public void SetDealt_ValidInput_ShouldUpdateDealt()
    {
        var report = new Report("Title", "Content", 1, DateTime.Now);
        report.SetDealt(true);

        Assert.IsTrue(report.Dealt);
    }

    [Test]
    public void SetDateTime_ShouldUpdateDate()
    {
        var report = new Report("Title", "Content", 1, DateTime.Now);
        var newDate = DateTime.Now.AddDays(1);
        report.SetDateTime(newDate);

        Assert.AreEqual(newDate, report.Date);
    }
}