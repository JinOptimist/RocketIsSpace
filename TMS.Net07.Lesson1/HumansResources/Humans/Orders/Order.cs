﻿using System;
using System.Collections.Generic;
using HumansResources.Humans.Clients;
using HumansResources.Humans.Employes;

namespace HumansResources.Humans.Orders
{
    public class Order
    {
        public Client Client { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal OrderCost { get; set; }
        public string OrderDescription { get; set; }
        public bool IsClosed { get; set; }

        private readonly List<Employe> _listEmployes = new List<Employe>();

        public Order()
        {
        }

        public Order(Client client, DateTime dateStart, DateTime dateEnd, decimal orderCost)
        {
            Client = client;
            DateStart = dateStart;
            DateEnd = dateEnd;
            OrderCost = orderCost;
        }

        public bool IsValidOrder()
        {
            return (
                Client != null &&
                DateStart != null &&
                DateEnd != null &&
                DateTime.Compare(DateStart, DateEnd) < 0 &&
                OrderCost > 0
                );
        }

        public List<Employe> GetEmployes()
        {
            return _listEmployes;
        }

        public void SetEmploye(Employe employe)
        {
            _listEmployes.Add(employe);
        }
    }
}