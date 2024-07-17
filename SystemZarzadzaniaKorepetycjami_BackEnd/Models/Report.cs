using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Report
{
    public int IdReport { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public int Sender { get; private set; }
    public DateTime Date { get; private set; }
    public bool Dealt { get; private set; }

            public virtual Person SenderNavigation { get; private set; }
}
}
