using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    internal class Request
    {
        private int _amount;
        private Boardgame _boardGame;
        private Customer _customer;

        public Request(int amount, Boardgame boardGame, Customer customer)
        {
            _amount = amount;
            _boardGame = boardGame;
            _customer = customer;

            
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
