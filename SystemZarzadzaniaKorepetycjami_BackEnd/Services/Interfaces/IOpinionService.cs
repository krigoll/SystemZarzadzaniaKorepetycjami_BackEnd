public interface IOpinionService
{
	public Task<List<OpinionDTO>> GetOpinionsByStudentEmailAsync(string email);
	public Task<List<OpinionDTO>> GetOpinionsByTeacherIdAsync(int teacherId);
	public Task<List<OpinionDTO>> GetOpinionsByTeacherEmailAsync(string email);
	public Task<OpinionDetailsDTO> GetOpinionDetailsByIdAsync(int opinionId);
	public Task<OpinionStatus> CreateOpinionAsync(OpinionCreateDTO opinionCreateDTO);
	public Task<OpinionStatus> UpdateOpinionByIdAsync(int opinionId, OpinionCreateDTO opinionCreateDTO);
	public Task<bool> DeleteOpinionByIdAsync(int opinionId);
}