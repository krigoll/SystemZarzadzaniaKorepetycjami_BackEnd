using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class Person
{
    private Person()
    {
        MessageReceiverNavigation = new HashSet<Message>();
        MessageSenderNavigation = new HashSet<Message>();
        RefreshTokens = new HashSet<RefreshTokens>();
        Report = new HashSet<Report>();
    }

    public int IdPerson { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; }
    public byte[] Image { get; private set; }
    public DateOnly JoiningDate { get; private set; }

            public virtual Administrator Administrator { get; private set; }
            public virtual Student Student { get; private set; }
            public virtual Teacher Teacher { get; private set; }
            public virtual ICollection<Message> MessageReceiverNavigation { get; private set; }
            public virtual ICollection<Message> MessageSenderNavigation { get; private set; }
            public virtual ICollection<RefreshTokens> RefreshTokens { get; private set; }
            public virtual ICollection<Report> Report { get; private set; }
}
}
