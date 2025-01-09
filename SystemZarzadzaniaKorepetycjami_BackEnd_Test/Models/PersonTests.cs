using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class PersonTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreatePerson()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.AreEqual("Jan", person.Name);
        Assert.AreEqual("Kowalski", person.Surname);
        Assert.AreEqual(DateOnly.Parse("2000-01-01"), person.BirthDate);
        Assert.AreEqual("jan.kowalski@example.com", person.Email);
        Assert.IsNotNull(person.Password);
        Assert.AreEqual("123456789", person.PhoneNumber);
        Assert.IsNull(person.Image);
    }

    [Test]
    public void SetName_EmptyName_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() => person.SetName(""));
    }

    [Test]
    public void SetSurname_EmptySurname_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() => person.SetSurname("  "));
    }

    [Test]
    public void SetBirthDate_FutureDate_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() =>
            person.SetBirthDate(DateOnly.FromDateTime(DateTime.Now.AddDays(1)).ToString()));
    }

    [Test]
    public void SetEmail_InvalidEmail_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() => person.SetEmail("invalid-email"));
    }

    [Test]
    public void SetPassword_InvalidPassword_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() => person.SetPassword("short"));
    }

    [Test]
    public void SetPhoneNumber_InvalidPhoneNumber_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(() => person.SetPhoneNumber("abc123"));
    }

    [Test]
    public void SetJoiningDate_FutureDate_ShouldThrowArgumentException()
    {
        var person = new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234",
            "123456789", null);
        Assert.Throws<ArgumentException>(
            () => person.SetJoiningDate(DateOnly.FromDateTime(DateTime.Now.AddDays(1))));
    }

    [Test]
    public void Constructor_InvalidEmail_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Person("Jan", "Kowalski", "2000-01-01", "invalid-email", "Haslo1234", "123456789", null));
    }

    [Test]
    public void Constructor_InvalidPassword_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "short", "123456789", null));
    }

    [Test]
    public void Constructor_InvalidPhoneNumber_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Person("Jan", "Kowalski", "2000-01-01", "jan.kowalski@example.com", "Haslo1234", "123abc", null));
    }
}