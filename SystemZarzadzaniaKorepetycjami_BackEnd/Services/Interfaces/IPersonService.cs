using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IPersonService
{
    public Task<RegisterStatus> RegistrationPerson(RegistrationDTO registrationDto);
    public Task<PersonRoleDTO> GetPersonRoleAsync(string email);
    public Task<PersonProfileDTO> GetPersonProfileByEmailAsync(string email);
    public Task<UpdateUserStatus> UpdateUserAsync(int idPerson, PersonEditProfileDTO personProfileDto);
}