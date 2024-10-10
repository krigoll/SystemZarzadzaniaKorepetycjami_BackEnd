using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IOpinionService
{
    public Task<List<OpinionDTO>> GetOpinionsByStudentEmailAsync(string email);
    public Task<List<OpinionDTO>> GetOpinionsByTeacherIdAsync(int teacherId);
    public Task<List<OpinionDTO>> GetOpinionsByTeacherEmailAsync(string email);
    public Task<OpinionDetailsDTO> GetOpinionByIdAsync(int opinionId);
    public Task<CreateOpinionStatus> CreateOpinionAsync(OpinionCreateDTO opinionCreateDTO);
    public Task<CreateOpinionStatus> UpdateOpinionByIdAsync(int opinionId, OpinionCreateDTO opinionCreateDTO);
    public Task<bool> DeleteOpinionByIdAsync(int opinionId);
}