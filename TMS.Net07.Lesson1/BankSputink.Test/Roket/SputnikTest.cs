using BankSputink.Rocket;
using NUnit.Framework;
using System;

namespace BankSputink.Test
{
    public class SputnikTest
    {
        [Test]
        public void Launch_ToHeavy()
        {
            var sputnik = new Sputnik(50);

            Assert.Throws<Exception>(() => sputnik.Launch());
        }

        [Test]
        [TestCase(3, "Light")]
        [TestCase(7, "Medium")]
        public void Launch_Normal(int mass, string roketName)
        {
            var sputnik = new Sputnik(mass);

            var result = sputnik.Launch();

            Assert.AreEqual(roketName, result);
        }
    }
}
