using System;
using System.Collections.Generic;
using System.Text;
using HumansResources.Humans.Persons;
using HumansResources.Humans.Orders;

namespace HumansResources.Humans.Clients
{
    class ClientVariant : IContact

    {
       
        //здесь мы реализовываем интерфейс IContact
        
        public string Name { get; set; }
        public string Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public PostAddress PostAddress { get; set; }

        //здесь мы будем хранить заказы этого клиента
        private readonly List<Order> listOrders = new List<Order>();

       //Оставь пустой конструктор для других
        public ClientVariant() { 
        }


        /*Через конструктор получаем нужные поля, я умышленно пропускаю  PostAddress postAddress.
        Мы можем не знать его или получить через setter потом. Вообще через конструктор, как правило,
        передают минимальные параметры для работоспособности класса или его целесообразности.*/
        public ClientVariant(string name, string email, PhoneNumber phoneNumber) {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        //Можем сделать такой конструктор, когда сразу передаем order
        public ClientVariant(string name,  Order order)
        {
            Name = name;           
            listOrders.Add(order);
        }

        
        //Далее придумываем какие-нибудь методы     
        
        public void SetOrder(Order order) {
            listOrders.Add(order);
        }

        public List<Order> GetOrders()
        {
            return listOrders;
        }

        public int GetCountOrders() {
            return listOrders.Count;
        }

        /*Осталось придумать какой-нибудь метод, лучше два, которые целесообразно
        "покрыть" unit-тестами. Тут нужно проявить фантазию. У меня пока закончилось 
        на сегодня, может завтра что-нибудь еще подскажу. */
    }
}
