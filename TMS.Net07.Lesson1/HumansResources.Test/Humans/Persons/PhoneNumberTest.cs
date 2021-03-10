using HumansResources.Humans.Persons;
using NUnit.Framework;

namespace HumansResources.Test.Humans.Persons
{
    class PhoneNumberTest
    {
        [Test]
        [TestCase("375447360162")]
        [TestCase("7037532385")]
        [TestCase("65353265345")]
        [TestCase("3628746325")]
        public void IsValidPhoneNumber(string phoneNumberString)
        {
            PhoneNumber phone = new PhoneNumber(phoneNumberString);
            Assert.AreEqual(((IValidator)phone).Validation(), true);
        }

        [Test]
        [TestCase("+375447360162")]
        [TestCase("7(03)7532385")]
        [TestCase("6535326-53-45")]
        [TestCase("3663454528746325")]
        public void IsNotValidPhoneNumber(string phoneNumberString)
        {
            PhoneNumber phone = new PhoneNumber(phoneNumberString);
            Assert.AreEqual(((IValidator)phone).Validation(), false);
        }
    }
}
