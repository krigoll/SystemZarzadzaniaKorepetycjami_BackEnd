using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class SubjectLevel
{
    private SubjectLevel()
    {
        Lesson = new HashSet<Lesson>();
        Task = new HashSet<Task>();
        TeacherSalary = new HashSet<TeacherSalary>();
    }

    public int IdSubjectLevel { get; private set; }
    public string Name { get; private set; }
    public int IdSubjectCategory { get; private set; }

            public virtual SubjectCategory IdSubjectCategoryNavigation { get; private set; }
            public virtual ICollection<Lesson> Lesson { get; private set; }
            public virtual ICollection<Task> Task { get; private set; }
            public virtual ICollection<TeacherSalary> TeacherSalary { get; private set; }
}
}
