using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class MessageTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateMessage()
    {
        var date = DateTime.Now;
        var message = new Message(1, 2, date, "Hello!");

        Assert.AreEqual(1, message.Sender);
        Assert.AreEqual(2, message.Receiver);
        Assert.AreEqual(date, message.Date);
        Assert.AreEqual("Hello!", message.Content);
    }

    [Test]
    public void Constructor_InvalidSender_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Message(0, 2, DateTime.Now, "Hello!"));
        Assert.Throws<ArgumentException>(() => new Message(-1, 2, DateTime.Now, "Hello!"));
    }

    [Test]
    public void Constructor_InvalidReceiver_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Message(1, 0, DateTime.Now, "Hello!"));
        Assert.Throws<ArgumentException>(() => new Message(1, -1, DateTime.Now, "Hello!"));
    }

    [Test]
    public void Constructor_InvalidContent_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Message(1, 2, DateTime.Now, ""));
        Assert.Throws<ArgumentException>(() => new Message(1, 2, DateTime.Now, null));
        Assert.Throws<ArgumentException>(() => new Message(1, 2, DateTime.Now, new string('A', 501)));
    }

    [Test]
    public void SetSender_ValidInput_ShouldUpdateSender()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");
        message.SetSender(3);

        Assert.AreEqual(3, message.Sender);
    }

    [Test]
    public void SetSender_InvalidInput_ShouldThrowArgumentException()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");

        Assert.Throws<ArgumentException>(() => message.SetSender(0));
        Assert.Throws<ArgumentException>(() => message.SetSender(-1));
    }

    [Test]
    public void SetReceiver_ValidInput_ShouldUpdateReceiver()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");
        message.SetReceiver(4);

        Assert.AreEqual(4, message.Receiver);
    }

    [Test]
    public void SetReceiver_InvalidInput_ShouldThrowArgumentException()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");

        Assert.Throws<ArgumentException>(() => message.SetReceiver(0));
        Assert.Throws<ArgumentException>(() => message.SetReceiver(-1));
    }

    [Test]
    public void SetContent_ValidInput_ShouldUpdateContent()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");
        message.SetContent("Updated content");

        Assert.AreEqual("Updated content", message.Content);
    }

    [Test]
    public void SetContent_InvalidInput_ShouldThrowArgumentException()
    {
        var message = new Message(1, 2, DateTime.Now, "Hello!");

        Assert.Throws<ArgumentException>(() => message.SetContent(""));
        Assert.Throws<ArgumentException>(() => message.SetContent(null));
        Assert.Throws<ArgumentException>(() => message.SetContent(new string('B', 501)));
    }

    [Test]
    public void SetDate_ShouldUpdateDate()
    {
        var initialDate = DateTime.Now;
        var newDate = initialDate.AddDays(1);
        var message = new Message(1, 2, initialDate, "Hello!");

        message.SetDate(newDate);

        Assert.AreEqual(newDate, message.Date);
    }
}