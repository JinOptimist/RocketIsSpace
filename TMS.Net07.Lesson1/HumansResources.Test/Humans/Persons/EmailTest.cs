using HumansResources.Humans.Persons;
using NUnit.Framework;

namespace HumansResources.Test.Humans.Persons
{
    class EmailTest
    {
        [Test]
        [TestCase("fisdy@mail.ru", true)]
        [TestCase("fdu23@ufi.gdf", true)]
        [TestCase("78873@gmail.com", true)]
        [TestCase("rw70@yandex.ru", true)]
        [TestCase("fisdymail.ru", false)]
        [TestCase("fdu23@ufigdf", false)]
        [TestCase("78873@fafr.", false)]
        [TestCase("rw70@org,fr", false)]
        public void IsValidEmail(string emailString, bool acttualResult)
        {
            Email email = new Email(emailString);
            Assert.AreEqual(((IValidator)email).Validation(), acttualResult); 
        }
    }
}