using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Mark
{
    public Mark(string description, bool value, int idStudentAnswer)
    {
        SetDescription(description);
        Value = value;
        IdStudentAnswer = idStudentAnswer;
    }

    public void SetDescription(string description)
    {
        if (description.IsNullOrEmpty() || description.Length > 100) throw new ArgumentException("Invalid Description");

        Description = description;
    }

    public void SetValue(bool value)
    {
        Value = value;
    }
}