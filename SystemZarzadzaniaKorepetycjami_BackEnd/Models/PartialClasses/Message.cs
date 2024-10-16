namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Message
{
    public Message(int sender, int reciver, DateTime date, string content)
	{
		SetSender(sender);
        SetReceiver(reciver);
		SetDate(date);
		SetContent(content);
	}
	
	public void SetSender(int sender)
	{
		if (sender <= 0)
			throw new ArgumentException("Invalid sender");
		Sender = sender;
	}
	
	public void SetReceiver(int recevier)
	{
		if (recevier <= 0)
			throw new ArgumentException("Invalid recevier");
		Receiver = recevier;
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