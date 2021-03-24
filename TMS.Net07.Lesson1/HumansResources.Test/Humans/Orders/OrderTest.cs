using System;
using NUnit.Framework;
using HumansResources.Humans.Orders;
using HumansResources.Humans.Clients;

namespace HumansResources.Test.Humans.Orders
{
    public class OrderTest
    {
        [Test]
        [TestCase("2020-08-01", "2020-08-01", 1000, false)]
        [TestCase("2020-08-01", "2020-09-01", -100, false)]
        [TestCase("2020-09-01", "2020-08-01", 1000, false)]
        [TestCase("2020-09-01", "2020-00-01", 1000, false)]
        [TestCase("2020-08-01", "2020-09-01", 1000, true)]
        public void IsValidOrder(string dateStringStart, string dateStringEnd, decimal orderCost, bool expectedResult)
        {
            Client client = new Client();
            DateTime.TryParse(dateStringStart, out DateTime dateStart);
            DateTime.TryParse(dateStringEnd, out DateTime dateEnd);
            Order order = new Order(client, dateStart, dateEnd, orderCost);
            Assert.AreEqual(order.IsValidOrder(), expectedResult);
        }
    }
}