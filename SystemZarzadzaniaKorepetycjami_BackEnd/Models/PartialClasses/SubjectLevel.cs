using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class SubjectLevel
{
    public SubjectLevel(string name, int idSubjectCategory)
    {
        SetIdSubjectCategory(idSubjectCategory);
        SetName(name);
    }

    public void SetName(string name)
    {
        if (name.IsNullOrEmpty() || name.Length > 50)
            throw new ArgumentException("Invalid Name");
        Name = name;
    }

    public void SetIdSubjectCategory(int idSubjectCategory)
    {
        if (idSubjectCategory < 1)
            throw new ArgumentException("Invalid Id Subject Category");
        IdSubjectCategory = idSubjectCategory;
    }
}