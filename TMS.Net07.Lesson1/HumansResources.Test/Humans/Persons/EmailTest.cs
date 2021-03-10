using HumansResources.Humans.Persons;
using NUnit.Framework;

namespace HumansResources.Test.Humans.Persons
{
    class EmailTest
    {
        [Test]
        [TestCase("fisdy@mail.ru")]
        [TestCase("fdu23@ufi.gdf")]
        [TestCase("78873@gmail.com")]
        [TestCase("rw70@yandex.ru")]
        public void IsValidEmail(string emailString)
        {
            Email email = new Email(emailString);
            Assert.AreEqual(((IValidator)email).Validation(), true); 
        }
        
        [Test]
        [TestCase("fisdymail.ru")]
        [TestCase("fdu23@ufigdf")]
        [TestCase("78873@fafr.")]
        [TestCase("rw70@org,fr")]
        public void IsNotValidEmail(string emailString)
        {
            Email email = new Email(emailString);
            Assert.AreEqual(((IValidator)email).Validation(), false);
        }
    }
}
