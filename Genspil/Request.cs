using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    internal class Request
    {
        private int _amount;
        private Boardgame _boardGame;
        private Customer _customer;
        public static List<Boardgame> boardgames = new List<Boardgame>();

        
        public Request(int amount, Customer customer)
        {
            _amount = amount;
            boardgames = new List<Boardgame>();
            _customer = customer;

            
        }

        public int GetAmonut()
        {
            return _amount;
        }
        
        public Boardgame GetBoardGame()
        {
            return _boardGame;
        }
        
        public Customer GetCustomer()
        {
            return _customer;
        }
        
        
        public void AddRequestToList(Request request, Customer customer)
        {
            customer.requestList.Add(request);
        }

        public static void PrintRequestList(List<Request> requestList)
        {
            foreach (Request request in requestList)
            {
                int index = 1;
                Console.WriteLine($"{index}. Name: {request._customer.Name}, Phone number: {request._customer.PhoneNumber}");
                Console.WriteLine($"  Game: {request._boardGame.Name}, Edition: {request._boardGame.Edition}, Genre: {request._boardGame.Genre}, Players: {request._boardGame.PlayerAmount}, Price: {request._boardGame.Price}, Stand: {request._boardGame.GameCondition}, Antal: {request._amount}");
                index++;
            }
        }

        public static void printList<T>(List<T> list) //print requestlist
        {
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}

