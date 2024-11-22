using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Ban
{
    public int IdBan { get; private set; }
    public int IdPerson { get; private set; }
    public DateTime StartTime { get; private set; }
    public int LenghtInDays { get; private set; }
    public string Reason { get; private set; }

            public virtual Person IdPersonNavigation { get; private set; }
}
}
