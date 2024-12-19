using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SystemZarzadzaniaKorepetycjami_BackEnd.DTOs;
using SystemZarzadzaniaKorepetycjami_BackEnd.Enums;
using SystemZarzadzaniaKorepetycjami_BackEnd.Repositories.Interfaces;
using SystemZarzadzaniaKorepetycjami_BackEnd.Services.Interfaces;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Services.Implementations;

public class LoginService : ILoginService
{
    private readonly IBanRepository _banRepository;
    private readonly IConfiguration _config;
    private readonly ILoginRepository _loginRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IPersonService _personService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginService(ILoginRepository loginRepository, IConfiguration config, IPersonRepository personRepository,
        IRefreshTokenRepository refreshTokenRepository, IPersonService personService, IBanRepository banRepository)
    {
        _loginRepository = loginRepository;
        _config = config;
        _personRepository = personRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _personService = personService;
        _banRepository = banRepository;
    }

    public async Task<(LoginStatus, string, string)> LoginAsync(LoginDTO request)
    {
        var loginStatus = await ValidateUserAsync(request.Email, request.Password);

        if (loginStatus != LoginStatus.USER_EXISTS)
            return (loginStatus, null, null);

        var personRole = await _personService.GetPersonRoleAsync(request.Email);
        var claims = GetClaims(personRole);
        var token = GenerateJwtToken(claims);
        var storedToken = await _refreshTokenRepository.GetRefreshTokenByEmailAsync(request.Email);
        var refreshToken = "";
        if (storedToken == null)
        {
            refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.StoreRefreshTokenAsync(request.Email, refreshToken);
        }
        else if (storedToken.ExpiryDate < DateTime.UtcNow)
        {
            refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.ReplaceRefreshTokenAsync(storedToken.Token, refreshToken);
        }
        else
        {
            refreshToken = storedToken.Token;
        }

        return (LoginStatus.USER_EXISTS, token, refreshToken);
    }

    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);

        if (storedToken == null || storedToken.ExpiryDate < DateTime.UtcNow)
            return null;

        var validToken = await ValidateRefreshToken(storedToken.Token);

        if (validToken == null)
            return null;

        var claims = validToken.Claims;
        var newAccessToken = GenerateJwtToken(claims);
        return newAccessToken;
    }

    public string HashPassword(LoginDTO request)
    {
        var workFactor = 12;
        var password = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor);
        return password;
    }

    public async Task<LoginStatus> ValidateUserAsync(string email, string password)
    {
        try
        {
            var person = await _loginRepository.findPersonByEmailAsync(email);
            if (person == null) return LoginStatus.USER_NOT_EXISTS;

            var isBanned = await _banRepository.IsUserBannedByEmail(email);
            if (isBanned) return LoginStatus.USER_BANED;

            if (BCrypt.Net.BCrypt.Verify(password, person.Password)) return LoginStatus.USER_EXISTS;

            return LoginStatus.USER_NOT_EXISTS;
        }
        catch (Exception e)
        {
            return LoginStatus.DATABASE_ERROR;
        }
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var secret = _config["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            "https://localhost:5001",
            "https://localhost:5001",
            claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        using (var genNum = RandomNumberGenerator.Create())
        {
            var r = new byte[32];
            genNum.GetBytes(r);
            return Convert.ToBase64String(r);
        }
    }

    private async Task<ClaimsPrincipal> ValidateRefreshToken(string refreshToken)
    {
        var personEmail = await _refreshTokenRepository.GetUserEmailByRefreshTokenAsync(refreshToken);
        if (personEmail == null) return null;

        var personRole = await _personService.GetPersonRoleAsync(personEmail);
        if (personRole == null) return null;

        var claims = GetClaims(personRole);
        var identity = new ClaimsIdentity(claims, "custom");
        return new ClaimsPrincipal(identity);
    }

    private IEnumerable<Claim> GetClaims(PersonRoleDTO personRole)
    {
        return new List<Claim>
        {
            new("isAdmin", personRole.isAdmin.ToString()),
            new("isTeacher", personRole.isTeacher.ToString()),
            new("isStudent", personRole.isStudent.ToString()),
            new("idPerson", personRole.IdPerson.ToString())
        };
    }
}