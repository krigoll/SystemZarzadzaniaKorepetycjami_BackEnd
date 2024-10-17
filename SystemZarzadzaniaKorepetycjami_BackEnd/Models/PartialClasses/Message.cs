namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Message
{
    public Message(int sender, int receiver, DateTime date, string content)
	{
		SetSender(sender);
        SetReceiver(receiver);
		SetDate(date);
		SetContent(content);
	}
	
	public void SetSender(int sender)
	{
		if (sender <= 0)
			throw new ArgumentException("Invalid sender");
		Sender = sender;
	}
	
	public void SetReceiver(int receiver)
	{
		if (receiver <= 0)
			throw new ArgumentException("Invalid recevier");
		Receiver = receiver;
	}
	
	public void SetDate(DateTime date)
	{
		Date = date;
	}
	
	public void SetContent(string content)
	{
		if (content == null || (content.Length  == 0 || content.Length >500))
			throw new ArgumentException("Invalid Content");
		Content = content;
	}
}