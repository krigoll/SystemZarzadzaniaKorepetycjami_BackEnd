namespace SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public class Subjects
{
}

public class SubjectLevelDto
{
    public string Name { get; set; }
    public SubjectCategoryDto SubjectCategory { get; set; }
}

public class SubjectCategoryDto
{
    public string Name { get; set; }
    public SubjectDto Subject { get; set; }
}

public class SubjectDto
{
    public string Name { get; set; }
}