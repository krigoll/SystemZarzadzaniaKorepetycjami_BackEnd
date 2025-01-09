using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;

public interface IBanService
{
	public Task<List<BanDTO>> GetAllBansAsync();
	public Task<BanDetailsDTO> GetBanDetailsByIdAsync(int idBan);
	public Task<BanStatus> CreateBanAsync(BanDetailsDTO ban);
	public Task<BanStatus> UpdateBanAsync(int idBan, BanDetailsDTO ban);
	public Task<bool> DeleteBanByIdAsync(int idBan);
}