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
        public void IsNotValidOrder()
        {
            DateTime.TryParse("2020-08-01", out DateTime dateStart);
            DateTime.TryParse("2021-09-01", out DateTime dateEnd);
            decimal amount = -1000;
            Client client = null;
            Order order = new Order(client, dateStart, dateEnd, amount);
            Assert.AreEqual(order.IsValidOrder(), false);
        }
    }
}