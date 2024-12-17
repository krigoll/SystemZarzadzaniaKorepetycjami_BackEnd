using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Ban
{
    public Ban(int idPerson, DateTime startTime, int lengthInDays, string reason)
    {
        SetIdBaned(idPerson);
        SetStartTime(startTime);
        SetLenghtInDays(lengthInDays);
        SetReason(reason);
    }

    public void SetIdBaned(int idPerson)
    {
        if (idPerson < 1)
            throw new ArgumentException("Invalid IdBaned");
        IdPerson = idPerson;
    }

    public void SetStartTime(DateTime startTime)
    {
        StartTime = startTime;
    }

    public void SetLenghtInDays(int lengthInDays)
    {
        if (lengthInDays < 1)
            throw new ArgumentException("Invalid Length In Days");
        LengthInDays = lengthInDays;
    }

    public void SetReason(string reason)
    {
        if (reason.IsNullOrEmpty() || reason.Length > 500)
            throw new ArgumentException("Invalid Reason");
        Reason = reason;
    }
}