using Microsoft.EntityFrameworkCore;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Implementations;

public class ReportRepository : IReportRepository
{
    private readonly SZKContext _context;

    public ReportRepository(SZKContext context)
    {
        _context = context;
    }

    public async Task<List<ReportDTO>> GetAllReportsAsync()
    {
        var reports = await (
            from report in _context.Report
            select new ReportDTO
            {
                IdReport = report.IdReport,
                Title = report.Title,
                IsDealt = report.Dealt
            }
        ).ToListAsync();
        return reports;
    }

    public async Task<Report> FindReportByIdAsync(int idReport)
    {
        var report = await _context.Report.FirstOrDefaultAsync(r => r.IdReport == idReport);
        return report;
    }

    public async Task<List<ReportDTO>> GetAllNotDeltedReportsAsync()
    {
        var reports = await (
            from report in _context.Report
            where report.Dealt == false
            select new ReportDTO
            {
                IdReport = report.IdReport,
                Title = report.Title,
                IsDealt = report.Dealt
            }
        ).ToListAsync();
        return reports;
    }

    public async Task CreateRepostAsync(Report newReport)
    {
        await _context.Report.AddAsync(newReport);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRepostAsync(Report updatedReport)
    {
        _context.Report.Update(updatedReport);
        await _context.SaveChangesAsync();
    }
}