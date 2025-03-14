using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Genspil
{


    internal class Customer
    {
        private string _name;
        private string _email;
        private int _phoneNumber;
    

    public string Name
        {
            get => _name;
            set => _name = value;
        }

    public string Email
        {
            get => _email;
            set => _email = value;
        }

    public int PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public Customer()
        {
            _name = "ukendt";
            _email = "ukendt";
            _phoneNumber = 00000000;
        }


        public Customer (string name, string email, int phoneNumber)
        {
            _name = name ?? "ukendt";
            _email = email ?? "ukendt";
            _phoneNumber = phoneNumber > 0 ? phoneNumber : 00000000;
        }
    
        public static List<Customer> CustomerList = new List<Customer>();

        public void AddCustomer(string name, string email, int phoneNumber)
        {
            Customer newCustomer = new Customer(name, email, phoneNumber);
            CustomerList.Add(newCustomer);
            Console.WriteLine($"Kunde {newCustomer.Name} tilføjet!");

        }


        public void DeleteCustomer(int phoneNumber)
        { 
        
        }
    }
}