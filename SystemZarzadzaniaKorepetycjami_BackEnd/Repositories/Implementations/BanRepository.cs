using Microsoft.EntityFrameworkCore;
using System.Globalization;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public class BanRepository : IBanRepository
{
	private readonly SZKContext _context;
	
	public BanRepository(SZKContext context)
	{
		_context = context;
	}
	
	public async Task<List<BanDTO>> GetAllBansAsync()
	{
		var bans = await (
			from ban in _context.Ban
			join person in _context.Person
					on ban.IdPerson equals person.IdPerson
			select new BanDTO{
				IdBan = ban.IdBan,
				BanedName = $"{person.Name} {person.Surname}",
				StartTime = ban.StartTime.ToString("yyyy-MM-dd HH:mm", new CultureInfo("pl-Pl"))
			}
		).ToListAsync();
		return bans;	
	}
	
	public async Task<BanDetailsDTO> GetBanDetalisByIdAsync(int idBan)
	{
		var banDetails = await (
			from ban in _context.Ban
			join person in _context.Person
					on ban.IdPerson equals person.IdPerson
			select new BanDetailsDTO{
                IdPerson = ban.IdPerson,
				BanedName = $"{person.Name} {person.Surname}",
				StartTime = ban.StartTime.ToString("yyyy-MM-dd HH:mm", new CultureInfo("pl-Pl")),
				LenghtInDays = ban.LenghtInDays,
				Reason = ban.Reason
			}
		).FirstOrDefaultAsync();
		return banDetails;
	}
	
	public async Task CreateBanAsync(Ban newBan)
	{
		await _context.Ban.AddAsync(newBan);
		await _context.SaveChangesAsync();
	}
	
	public async Task<Ban> FindBanByIdAsync(int idBan)
	{
		var ban = await _context.Ban.FirstOrDefaultAsync(b=> b.IdBan == idBan);
		return ban;
	}
	
	public async Task UpdateBanAsync(Ban updateBan)
	{
		_context.Ban.Update(updateBan);
		await _context.SaveChangesAsync();
	}
	
	public async Task RemoveBanAsync(Ban ban)
	{
		_context.Ban.Remove(ban);
		await _context.SaveChangesAsync(); 
	}
}