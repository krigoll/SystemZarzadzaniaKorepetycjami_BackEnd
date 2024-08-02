namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IAdminRepository
{
    public Task<bool> isAdministratorByEmail(string email);
}