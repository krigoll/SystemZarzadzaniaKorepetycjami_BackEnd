using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IPersonService
{
    public Task<RegisterStatus> RegistrationPerson(RegistrationDTO registrationDto);
    public Task<PersonRoleDTO> GetPersonRoleAsync(string email);
    public Task<PersonProfileDTO> GetPersonProfileByEmailAsync(string email);
    public Task<PersonProfileForAdminDTO> GetPersonProfileByIdAsync(int userId);
    public Task<UpdateUserStatus> UpdateUserAsync(int idPerson, PersonEditProfileDTO personProfileDto);
    public Task DeleteUserByEmailAsync(string email);
    public Task<List<PersonDTO>> FindPersonsByNameOrSurname(string search);
    public Task<List<PersonInfoDTO>> GetAllPersonsAsync();
}