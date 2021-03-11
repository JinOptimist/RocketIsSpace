using HumansResources.Humans.Persons;
using NUnit.Framework;

namespace HumansResources.Test.Humans.Persons
{
    class PhoneNumberTest
    {
        [Test]
        [TestCase("375447360162", true)]
        [TestCase("7037532385", true)]
        [TestCase("65353265345", true)]
        [TestCase("3628746325", true)]
        [TestCase("+375447360162", false)]
        [TestCase("7(03)7532385", false)]
        [TestCase("6535326-53-45", false)]
        [TestCase("3663454528746325", false)]
        public void IsValidPhoneNumber(string phoneNumberString, bool actualResult)
        {
            PhoneNumber phone = new PhoneNumber(phoneNumberString);
            Assert.AreEqual(((IValidator)phone).Validation(), actualResult);
        }
    }
}