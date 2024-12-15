using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public interface IBanRepository
{
	public Task<List<BanDTO>> GetAllBansAsync();	
	public Task<BanDetailsDTO> GetBanDetalisByIdAsync(int idBan);
	public Task CreateBanAsync(Ban newBan);
	public Task<Ban> FindBanByIdAsync(int idBan);
	public Task UpdateBanAsync(Ban updateBan);
	public Task RemoveBanAsync(Ban ban);
	public Task<BannedInformationDTO> GetNewestBanByUserId(int userId);
}