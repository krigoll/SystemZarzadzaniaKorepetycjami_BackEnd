using System.Net;
using System.Net.Mail;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Models;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class ResetPasswordService : IResetPasswordService
{
    private readonly IPersonRepository _personRepository;
    private readonly IResetPasswordRepository _resetPasswordRepository;

    public ResetPasswordService(IResetPasswordRepository resetPasswordRepository, IPersonRepository personRepository)
    {
        _resetPasswordRepository = resetPasswordRepository;
        _personRepository = personRepository;
    }

    public async Task<CodeStatus> CreateCodeAsync(string email)
    {
        try
        {
            var person = await _personRepository.FindPersonByEmailAsync(email);

            if (person == null) return CodeStatus.INVALID_EMAIL;

            var rnd = new Random();
            var code = rnd.Next(100000, 999999);
            var resetPassword = new RessetPassword(person.IdPerson, code.ToString(), DateTime.Now.AddHours(1));

            await _resetPasswordRepository.AddAsync(resetPassword);
            await SendEmailAsync(email, code.ToString());
            return CodeStatus.OK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CodeStatus.SERVER_ERROR;
        }
    }

    public async Task<ResetStatus> ResetPasswordAsync(string code, string password)
    {
        try
        {
            var idPerson = await _resetPasswordRepository.GetIdPersonByCode(code);
            if (idPerson == -1) return ResetStatus.INVALID_CODE;

            var person = await _personRepository.FindPersonByIdAsync(idPerson);
            person.SetPassword(password);
            await _personRepository.UpdateUserAsync(person);
            await _resetPasswordRepository.RemoveCode(code);
            return ResetStatus.OK;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResetStatus.SERVER_ERROR;
        }
    }

    private async Task SendEmailAsync(string email, string code)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("testpjatk@gmail.com", "nmjr uwxf pqfh ytrf"),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("testpjatk@gmail.com"),
            Subject = "Kod resetu hasła",
            Body = $"Twój kod resetu hasła to: {code}",
            IsBodyHtml = false
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}