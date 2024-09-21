using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models;

public partial class Person
{
    public Person(string name, string surname, string birthDate, string email, string password, string phoneNumber,
        byte[] image)
    {
        SetName(name);
        SetSurname(surname);
        SetBirthDate(birthDate);
        SetEmail(email);
        SetPassword(password);
        SetPhoneNumber(phoneNumber);
        SetImage(image);
        SetJoiningDate(DateOnly.FromDateTime(DateTime.Now));
        SetIsDeleted(false);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.");

        Name = name;
    }

    public void SetSurname(string surname)
    {
        if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be empty.");

        Surname = surname;
    }

    public void SetBirthDate(string birthDate)
    {
        var b = DateOnly.Parse(birthDate);
        if (b > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("BirthDate cannot be in the future.");

        BirthDate = b;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            throw new ArgumentException("Invalid email address.");

        Email = email;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
            throw new ArgumentException("Invalid email address.");

        var workFactor = 12;
        Password = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
    }

    private bool IsValidPassword(string password)
    {
        if (password.Length < 8 || password.Length > 50)
            return false;

        if (!Regex.IsMatch(password, "[a-z]"))
            return false;

        if (!Regex.IsMatch(password, "[A-Z]"))
            return false;

        if (!Regex.IsMatch(password, "[0-9]"))
            return false;

        return true;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) || !IsValidPhoneNumber(phoneNumber))
            throw new ArgumentException("Invalid phone number.");

        PhoneNumber = phoneNumber;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 7 && phoneNumber.Length <= 15;
    }

    public void SetImage(byte[] image)
    {
        Image = image;
    }

    public void SetJoiningDate(DateOnly joiningDate)
    {
        if (joiningDate > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("JoiningDate cannot be from the future.");

        JoiningDate = joiningDate;
    }

    public void SetIsDeleted(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }
}