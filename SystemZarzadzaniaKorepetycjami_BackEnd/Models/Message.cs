using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Message
{
    public int IdMessage { get; private set; }
    public int Sender { get; private set; }
    public int Receiver { get; private set; }
    public DateTime Date { get; private set; }
    public string Content { get; private set; }

            public virtual Person ReceiverNavigation { get; private set; }
            public virtual Person SenderNavigation { get; private set; }
}
}
