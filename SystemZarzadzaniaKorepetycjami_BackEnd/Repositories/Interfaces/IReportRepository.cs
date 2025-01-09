using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

public interface IReportRepository
{
    public Task<List<ReportDTO>> GetAllReportsAsync();
    public Task<Report> FindReportByIdAsync(int idReport);
    public Task<List<ReportDTO>> GetAllNotDeltedReportsAsync();
    public Task CreateRepostAsync(Report newReport);
    public Task UpdateRepostAsync(Report updatedReport);
}