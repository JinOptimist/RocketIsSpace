using System;
using NUnit.Framework;
using HumansResources.Humans.Orders;
using HumansResources.Humans.Clients;

namespace HumansResources.Test.Humans.Orders
{
    public class OrderTest
    {
        [Test]
        public void IsValidOrder()
        {
            DateTime.TryParse("2020-08-01", out DateTime dateStart);
            DateTime.TryParse("2020-09-01", out DateTime dateEnd);
            decimal amount = 1000;
            Client client = new Client();
            Order order = new Order(client, dateStart, dateEnd, amount);
            Assert.AreEqual(order.IsValidOrder(), true);
        }

        [Test]
        [TestCase("2020-08-01", "2020-08-01", 1000)]
        [TestCase("2020-08-01", "2020-09-01", -100)]
        [TestCase("2020-09-01", "2020-08-01", 1000)]
        [TestCase("2020-09-01", "2020-00-01", 1000)]
        public void IsNotValidOrder(string dateStringStart, string dateStringEnd, decimal amount)
        {
            Client client = new Client();
            DateTime.TryParse(dateStringStart, out DateTime dateStart);
            DateTime.TryParse(dateStringEnd, out DateTime dateEnd);
            Order order = new Order(client, dateStart, dateEnd, amount);
            Assert.AreEqual(order.IsValidOrder(), false);
        }
    }
}