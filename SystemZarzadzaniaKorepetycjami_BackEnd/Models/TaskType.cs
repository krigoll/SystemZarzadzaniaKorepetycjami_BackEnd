namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class TaskType
    {
        private TaskType()
        {
            Task = new HashSet<Task>();
        }

        public int IdTaskType { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<Task> Task { get; private set; }
    }
}