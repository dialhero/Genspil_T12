﻿using System;
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

        public Customer() //Parameterløs constructor for at kunne oprette new Customer uden parametre
        {
            _name = "ukendt";
            _email = "ukendt";
            _phoneNumber = 00000000;
        }


        public Customer(string name, string email, int phoneNumber)
        {
            _name = name ?? "ukendt";
            _email = email ?? "ukendt";
            _phoneNumber = phoneNumber > 0 ? phoneNumber : 00000000;
        }

        public static List<Customer> CustomerList = new List<Customer>();

        public void AddCustomer(string name, string email, int phoneNumber) //SaveCustomersToFile lagt ind i Files, og metoden kaldes efter AddNewCustomer
        {
            Customer newCustomer = new Customer(name, email, phoneNumber);
            CustomerList.Add(newCustomer);
            Files.SaveCustomersToFile();
            Console.WriteLine($"\nKunde {newCustomer.Name} tilføjet!");

        }


        public void DeleteCustomer(int phoneNumber) //Vi fjerne kunde via telefonnummer. Vi tjekker også for om nummeret findes. 
        {
            Customer customerToRemove = CustomerList.Find(c => c.PhoneNumber == phoneNumber);
            if (customerToRemove != null)
            {
                CustomerList.Remove(customerToRemove);
                Console.WriteLine($"Kunde med telefonnummer {phoneNumber} er blevet fjernet.");
            }
            else
            {
                Console.WriteLine($"Ingen kunde fundet med telefonnummer {phoneNumber}.");
            }
            Files.SaveCustomersToFile();
        }


        public static void ShowCustomers() //Til at teste add metoden og CSV filen
        {
            Console.WriteLine("\nListe over kunder:");
            foreach (var customer in CustomerList)
            {
                Console.WriteLine($"Navn: {customer.Name}, Email: {customer.Email}, Tlf: {customer.PhoneNumber}");
            }
        }

        public static Customer GetCustomerByPhoneNumber(int phoneNumber)
        {
            foreach (var customer in CustomerList)
            {
                if (customer._phoneNumber == phoneNumber)
                {
                    return customer;
                }
            }

            return null;
        }

        public static string getContactDetails(List<Customer> customers, int phoneNumber)

        {
            Customer customer = customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

            if (customer != null)
            {
                return $"Navn: {customer.Name}, Email: {customer.Email}, Telefonnummer: {customer.PhoneNumber}";

            }
            else
            {
                return "Brugeren er ikke fundet.";

            }

        }
    }
}





