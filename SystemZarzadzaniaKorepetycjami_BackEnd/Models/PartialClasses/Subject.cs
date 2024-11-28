using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Subject
{
    public Subject(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (name.IsNullOrEmpty() || name.Length > 50)
            throw new ArgumentException("Invalid Name");
        Name = name;
    }
}