
using Microsoft.IdentityModel.Tokens;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Ban
{
	public Ban(int idPerson, DateTime startTime, int lenghtInDays, string reason)
	{
		SetIdBaned(idPerson);
		SetStartTime(startTime);
		SetLenghtInDays(lenghtInDays);
		SetReason(reason);	
	}
	
	public void SetIdBaned(int idPerson)
	{
		if(idPerson<1)
			throw new ArgumentException("Invalid IdBaned");
		IdPerson = idPerson;
	}
	
	public void SetStartTime(DateTime startTime)
	{
		if(startTime == null)
			throw new ArgumentException("Invalid StartTime");
		StartTime = startTime;
	}
	
	public void SetLenghtInDays(int lenghtInDays)
	{
		if(lenghtInDays<1)
			throw new ArgumentException("Invalid Lenght In Days");
		LenghtInDays = lenghtInDays;
	}
	
	public void SetReason(string reason)
	{
		if(reason.IsNullOrEmpty() || reason.Length > 500)
			throw new ArgumentException("Invalid Reason");
		Reason = reason;
	}
	
}