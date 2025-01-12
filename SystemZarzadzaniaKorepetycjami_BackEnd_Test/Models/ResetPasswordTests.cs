using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

[TestFixture]
public class ResetPasswordTests
{
    [Test]
    public void Constructor_ValidInput_ShouldCreateResetPassword()
    {
        var expireDate = DateTime.Now.AddHours(1);
        var resetPassword = new ResetPassword(1, "123456", expireDate);

        Assert.AreEqual(1, resetPassword.IdPerson);
        Assert.AreEqual("123456", resetPassword.Code);
        Assert.AreEqual(expireDate, resetPassword.ExpireDate);
    }

    [Test]
    public void Constructor_InvalidIdPerson_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ResetPassword(-1, "123456", DateTime.Now.AddHours(1)));
    }

    [Test]
    public void Constructor_InvalidCode_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ResetPassword(1, "1234567", DateTime.Now.AddHours(1)));
        Assert.DoesNotThrow(() => new ResetPassword(1, "123456", DateTime.Now.AddHours(1))); // Maximum valid length
    }

    [Test]
    public void Constructor_ValidExpireDate_ShouldSetExpireDate()
    {
        var expireDate = DateTime.Now.AddDays(1);
        var resetPassword = new ResetPassword(1, "123456", expireDate);

        Assert.AreEqual(expireDate, resetPassword.ExpireDate);
    }

    [Test]
    public void Constructor_ExpiredDate_ShouldAllowExpiredDates()
    {
        var expiredDate = DateTime.Now.AddDays(-1);
        var resetPassword = new ResetPassword(1, "123456", expiredDate);

        Assert.AreEqual(expiredDate, resetPassword.ExpireDate);
    }
}