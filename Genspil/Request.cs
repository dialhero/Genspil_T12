using System;
using System.Collections.Generic;
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
        private List<Request> requestList {  get; set; }

        public Request(int amount, Boardgame boardGame, Customer customer)
        {
            _amount = amount;
            _boardGame = boardGame;
            _customer = customer;
        }

        public void AddRequestToList()
        {
            requestList.Add(this);
        }

        public static void PrintRequestList(List<Request> requestList)
        {
            foreach (Request request in requestList)
            {
                int index = 1;
                Console.WriteLine($"{index}. Name: {request._customer.Name}, Phone number: {request._customer.PhoneNumber}");
                Console.WriteLine($"  Game: {request._boardGame.name}, Edition: {request._boardGame.edition}, Genre: {request._boardGame.genre}, Players: {request._boardGame.playerAmount}, Price: {request._boardGame.price}, Stand: {request._boardGame.gameCondition}, Antal: {request._amount}");
                index++;
            }
        }
    }
}
