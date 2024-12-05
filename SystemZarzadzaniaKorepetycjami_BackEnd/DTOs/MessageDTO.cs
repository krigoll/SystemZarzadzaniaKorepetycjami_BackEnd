namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs
{
    public class MessageDTO
    {
        public int SenderId {get; set;}
	    public int ReceiverId {get; set;}
	    public string Date {get; set;}
	    public string Content {get; set;}
    }
}
