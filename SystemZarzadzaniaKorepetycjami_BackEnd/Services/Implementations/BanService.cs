using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public class BanService : IBanService
{
	private readonly IBanRepository _banRepository;
	private readonly IPersonRepository _personRepository;
	
	public BanService(IBanRepository banRepository, IPersonRepository personRepository)
	{
		_banRepository = banRepository;
		_personRepository = personRepository;
	}
	
	public async Task<List<BanDTO>> GetAllBansAsync()
	{
		return await _banRepository.GetAllBansAsync();	
	}
	
	public async Task<BanDetailsDTO> GetBanDetailsByIdAsync(int idBan)
	{
		var ban = await _banRepository.GetBanDetalisByIdAsync(idBan);
		if (ban == null)
		{
			return null;
		}
		
		return ban;
	}
	
	public async Task<BanStatus> CreateBanAsync(BanDetailsDTO ban)
	{
		try
		{
			var baned = _personRepository.FindPersonByIdAsync(ban.IdPerson);
			if(baned == null)
			{
				return BanStatus.INVALID_BANED_ID;
			}

			var newBan = new Ban(ban.IdPerson, DateTime.Parse(ban.StartTime), ban.LenghtInDays, ban.Reason);
			
			await _banRepository.CreateBanAsync(newBan);
			
			return BanStatus.OK;
		}
		catch (ArgumentException e)
		{
			Console.WriteLine(e);
			return BanStatus.INVALID_BAN;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return BanStatus.SERVER_ERROR;
		}
	}
	
	public async Task<BanStatus> UpdateBanAsync(int idBan, BanDetailsDTO ban)
	{
		try
		{
			var baned = await _personRepository.FindPersonByIdAsync(ban.IdPerson);
			if(baned == null)
			{
				return BanStatus.INVALID_BANED_ID;
			}

			var updateBan = await _banRepository.FindBanByIdAsync(idBan);
			updateBan.SetLenghtInDays(ban.LenghtInDays);
			updateBan.SetStartTime(DateTime.Parse(ban.StartTime));
			updateBan.SetReason(ban.Reason);
			
			await _banRepository.UpdateBanAsync(updateBan);
			return BanStatus.OK;
		}
		catch (ArgumentException e)
		{
			Console.WriteLine(e);
			return BanStatus.INVALID_BAN;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return BanStatus.SERVER_ERROR;
		}
	}
	
	public async Task<bool> DeleteBanByIdAsync(int idBan)
	{
		var ban = await _banRepository.FindBanByIdAsync(idBan);
		if (ban == null)
		{
			return false;
		}	
		await _banRepository.RemoveBanAsync(ban);
		return true;
	}
	
}