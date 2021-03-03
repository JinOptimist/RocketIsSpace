using System.Collections.Generic;
using System.Text;
using BankSputink.Rocket;
using NUnit.Framework;
using System;
using System.Linq;
using Moq;


namespace BankSputink.Test
{
    public class RocketLauncherTest
    {
        [Test]
        public void IsAnySputnikInProcess()
        {
            var sputniks = new List<ISputnik>();

            var sputnikReadyMock = new Mock<ISputnik>();
            sputnikReadyMock
                .Setup(x => x.IsReadyToLaunch)
                .Returns(true);

            var sputnikNotReadyMock = new Mock<ISputnik>();
            sputnikNotReadyMock
                .Setup(x => x.IsReadyToLaunch)
                .Returns(false);

            sputniks.Add(sputnikReadyMock.Object);
            sputniks.Add(sputnikNotReadyMock.Object);

            var rocketLauncher = new RocketLauncher(sputniks);

            var result = rocketLauncher.IsAnySputnikInProcess();

            Assert.AreEqual(true, result);
        }
    }
}
