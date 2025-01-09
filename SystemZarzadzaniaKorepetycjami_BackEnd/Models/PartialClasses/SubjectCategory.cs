using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class SubjectCategory
{
    public SubjectCategory(string name, int idSubject)
    {
        SetIdSubject(idSubject);
        SetName(name);
    }

    public void SetName(string name)
    {
        if (name.IsNullOrEmpty() || name.Length > 50)
            throw new ArgumentException("Invalid Name");
        Name = name;
    }

    public void SetIdSubject(int idSubject)
    {
        if (idSubject < 1)
            throw new ArgumentException("Invalid Id Subject");
        IdSubject = idSubject;
    }
}