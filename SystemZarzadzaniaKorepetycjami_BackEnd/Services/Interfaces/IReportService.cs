using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

public interface IReportService
{
    public Task<List<ReportDTO>> GetAllReportsAsync();
    public Task<ReportDetailsDTO> GetReportDetailsByIdAsync(int idReport);
    public Task<List<ReportDTO>> GetAllNotDeltedReportsAsync();
    public Task<ReportStatus> CreateRepostAsync(ReportDetailsDTO report);
    public Task<ReportStatus> UpdateRepostAsync(int idReport, ReportDetailsDTO report);
}