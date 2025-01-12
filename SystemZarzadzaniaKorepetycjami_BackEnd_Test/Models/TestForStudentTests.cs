using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test.Models;

public class TestForStudentTests
{
    [Test]
    public void TestConstructor_ValidInput_ShouldSetPropertiesCorrectly()
    {
        var idTest = 1;
        var idStudent = 1;

        var testForStudent = new TestForStudent(idTest, idStudent);

        Assert.AreEqual(idTest, testForStudent.IdTest);
        Assert.AreEqual(idStudent, testForStudent.IdStudent);
        Assert.AreEqual(DateTime.Now.Date, testForStudent.DateOfCreation.Date);
    }

    [Test]
    public void TestConstructor_InvalidIdTest_ThrowsArgumentException()
    {
        var invalidIdTest = -1;
        var idStudent = 1;

        var ex = Assert.Throws<ArgumentException>(() => new TestForStudent(invalidIdTest, idStudent));
        Assert.That(ex.Message, Does.Contain("Id is wrong"));
    }

    [Test]
    public void TestConstructor_InvalidIdStudent_ThrowsArgumentException()
    {
        var idTest = 1;
        var invalidIdStudent = -1;

        var ex = Assert.Throws<ArgumentException>(() => new TestForStudent(idTest, invalidIdStudent));
        Assert.That(ex.Message, Does.Contain("Id is wrong"));
    }

    [Test]
    public void TestConstructor_InvalidIdTestAndIdStudent_ThrowsArgumentException()
    {
        var invalidIdTest = -1;
        var invalidIdStudent = -1;

        var ex = Assert.Throws<ArgumentException>(() => new TestForStudent(invalidIdTest, invalidIdStudent));
        Assert.That(ex.Message, Does.Contain("Id is wrong"));
    }

    [Test]
    public void SetProperties_ValidInput_ShouldSetPropertiesCorrectly()
    {
        var testForStudent = new TestForStudent(1, 1);
        var initialDate = testForStudent.DateOfCreation;
        testForStudent = new TestForStudent(2, 2);
        Assert.AreNotEqual(initialDate, testForStudent.DateOfCreation);
    }
}