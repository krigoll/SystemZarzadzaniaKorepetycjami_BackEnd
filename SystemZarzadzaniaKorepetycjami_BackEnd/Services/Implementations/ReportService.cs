using System.Globalization;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class ReportService : IReportService
{
    private readonly IPersonRepository _personRepository;
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository, IPersonRepository personRepository)
    {
        _reportRepository = reportRepository;
        _personRepository = personRepository;
    }

    public async Task<List<ReportDTO>> GetAllReportsAsync()
    {
        return await _reportRepository.GetAllReportsAsync();
    }

    public async Task<ReportDetailsDTO> GetReportDetailsByIdAsync(int idReport)
    {
        var report = await _reportRepository.FindReportByIdAsync(idReport);
        if (report == null) return null;
        var person = await _personRepository.FindPersonByIdAsync(report.Sender);

        var reportDetails = new ReportDetailsDTO
        {
            IdSender = report.Sender,
            Title = report.Title,
            Content = report.Content,
            DateTime = report.Date.ToString("yyyy-MM-dd HH:mm", new CultureInfo("pl-Pl")),
            IsDealt = report.Dealt,
            FullName = person == null ? "Konto usunięte" : $"{person.Name} {person.Surname}"
        };

        return reportDetails;
    }

    public async Task<List<ReportDTO>> GetAllNotDeltedReportsAsync()
    {
        return await _reportRepository.GetAllNotDeltedReportsAsync();
    }

    public async Task<ReportStatus> CreateRepostAsync(ReportDetailsDTO report)
    {
        try
        {
            var sender = await _personRepository.FindPersonByIdAsync(report.IdSender);
            if (sender == null) return ReportStatus.INVALID_SENDER_ID;

            var newReport = new Report(report.Title, report.Content, report.IdSender, DateTime.Parse(report.DateTime));

            await _reportRepository.CreateRepostAsync(newReport);

            return ReportStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return ReportStatus.INVALID_REPORT;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ReportStatus.SERVER_ERROR;
        }
    }

    public async Task<ReportStatus> UpdateRepostAsync(int idReport, ReportDetailsDTO report)
    {
        try
        {
            var sender = await _personRepository.FindPersonByIdAsync(report.IdSender);
            if (sender == null) return ReportStatus.INVALID_SENDER_ID;

            var updateReport = await _reportRepository.FindReportByIdAsync(idReport);
            updateReport.SetTitle(report.Title);
            updateReport.SetContent(report.Content);
            updateReport.SetDealt(report.IsDealt);

            await _reportRepository.UpdateRepostAsync(updateReport);

            return ReportStatus.OK;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return ReportStatus.INVALID_REPORT;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ReportStatus.SERVER_ERROR;
        }
    }
}