using SystemZarzadzaniaKorepetycjami_BackEnd.Models;

namespace SystemZarzadzaniaKorepetycjami_BackEnd_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var person = new Person("Ja", "Ty", "01-01-2000", "ja@wp.pl", "Haslo1234", "123456789", null);
            Assert.Pass();
        }
    }
}