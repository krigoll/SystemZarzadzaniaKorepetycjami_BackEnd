using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Report
{
    public Report(string title, string content, int sender, DateTime date)
    {
        SetTitle(title);
        SetContent(content);
        SetSender(sender);
        SetDateTime(date);
        SetDealt(false);
    }

    public void SetTitle(string title)
    {
        if (title.IsNullOrEmpty() || title.Length >= 50)
            throw new ArgumentException("Invalid Title");
        Title = title;
    }

    public void SetContent(string content)
    {
        if (content.IsNullOrEmpty() || content.Length >= 500)
            throw new ArgumentException("Invalid Content");
        Content = content;
    }

    public void SetSender(int sender)
    {
        if (sender < 1)
            throw new ArgumentException("Invalid IdSender");
        Sender = sender;
    }

    public void SetDateTime(DateTime date)
    {
        Date = date;
    }

    public void SetDealt(bool dealt)
    {
        Dealt = dealt;
    }
}