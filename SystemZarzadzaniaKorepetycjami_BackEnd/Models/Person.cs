namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class Person
    {
        private Person()
        {
            Ban = new HashSet<Ban>();
            MessageReceiverNavigation = new HashSet<Message>();
            MessageSenderNavigation = new HashSet<Message>();
            OpinionIdStudentNavigation = new HashSet<Opinion>();
            OpinionIdTeacherNavigation = new HashSet<Opinion>();
            RefreshTokens = new HashSet<RefreshTokens>();
            Report = new HashSet<Report>();
            RessetPassword = new HashSet<RessetPassword>();
        }

        public int IdPerson { get; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public byte[] Image { get; private set; }
        public DateOnly JoiningDate { get; private set; }
        public bool IsDeleted { get; private set; }

        public virtual Administrator Administrator { get; }
        public virtual Student Student { get; }
        public virtual Teacher Teacher { get; }
        public virtual ICollection<Ban> Ban { get; private set; }
        public virtual ICollection<Message> MessageReceiverNavigation { get; private set; }
        public virtual ICollection<Message> MessageSenderNavigation { get; private set; }
        public virtual ICollection<Opinion> OpinionIdStudentNavigation { get; private set; }
        public virtual ICollection<Opinion> OpinionIdTeacherNavigation { get; private set; }
        public virtual ICollection<RefreshTokens> RefreshTokens { get; private set; }
        public virtual ICollection<Report> Report { get; private set; }
        public virtual ICollection<RessetPassword> RessetPassword { get; private set; }
    }
}