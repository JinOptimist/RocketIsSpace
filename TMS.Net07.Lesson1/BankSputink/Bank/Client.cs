using System;
using System.Collections.Generic;
using System.Text;

namespace BankSputink.Bank
{
    class Client                                //основной класс, используется при создании профиля
    {
        public string Name { get; set; }        //не имя, но логин
        public string Email { get; set; }
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _birthday;
        private string _phoneNumber;
        private bool _isBankAccountCreated;

        public Client (string name, string email)
        {
            Name = name;
            Email = email;
        }
        public string GetInfo ()                //метод, возвращает информацию об аккаунте
        {
            if (_isBankAccountCreated)
            {
                return $"Name: {Name}, Email {Email}, Phone Number: {_phoneNumber}\n"+
                    $"First Name: {_firstName}, Middle Name: {_middleName}, Last Name: {_lastName}\n " +
                    $"Birthday {_birthday}";
            }
            return $"Name: {Name}, Email: {Email}";
        }
        public void CreateBankAccount                                   //метод, создает счет/банковский аккаунт
            (string firstName, string middleName, string lastName,
            DateTime birthday, string phoneNumber /*enum currency*/)    //нужно создать enum с валютами
        {           
            if(!_isBankAccountCreated)
            {
                _firstName = firstName;     //идея заключается в том, чтобы ФИО + данные устанавливались только при создании счета
                _middleName = middleName;
                _lastName = lastName;       
                _birthday = birthday;
                _phoneNumber = phoneNumber;
                _isBankAccountCreated = true;
            }

            BankAccount newAccount = new BankAccount(this);
        }
    }
}
