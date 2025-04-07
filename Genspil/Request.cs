using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Genspil
{
    internal class Request
    {
        private int _amount;
        private Customer _customer;
        private List<Boardgame> _boardgames;

        public Request(int amount, Customer customer)
        {
            _amount = amount;
            _customer = customer;
            _boardgames = new List<Boardgame>();
        }
        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
        public List<Boardgame> Boardgames => _boardgames;
        public void AddRequestBoardgame(Boardgame game)
        {
            _boardgames.Add(game);
        }

        public int GetAmonut()
        {
            return _amount;
        }
        
        public Customer GetCustomer()
        {
            return _customer;
        }

        public static void AddRequestToList(Request request, Customer customer)
        {
            customer.requestList.Add(request);
        }

        public static void PrintAllRequestList()
        {
            foreach (Customer customer in Program.customerList) // Gå igennem hver kunde i kunde-listen
            {
                Console.WriteLine($"Navn: {customer.Name}, Email: {customer.Email}, Telefonnummer: {customer.PhoneNumber}");

                foreach (Request request in customer.requestList) // Gå igennem hver request for den aktuelle kunde
                {
                    Console.WriteLine($"  Antal: {request.Amount}");

                    foreach (Boardgame boardgame in request.Boardgames)
                    {
                        Console.WriteLine($"    Navn: {boardgame.Name}, Udgave: {boardgame.Edition}, Genre: {boardgame.Genre}, Antal spillere: {boardgame.PlayerAmount}, Pris: {boardgame.Price} DKK, Stand: {boardgame.GameCondition}");
                    }
                    Console.WriteLine(); // Ekstra linje mellem requests
                }
                Console.WriteLine(); // Ekstra linje mellem kunder
            }
        }
    }
}

