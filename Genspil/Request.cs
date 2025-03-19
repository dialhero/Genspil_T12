using System;
using System.Collections.Generic;
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
    }
}
