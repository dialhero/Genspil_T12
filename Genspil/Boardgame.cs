using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    
    internal class Boardgame
    {
        private string _name;
        private string _edition;
        private string _genre;
        private int _playerAmount;
        private double _price;
        private bool _inStock;
        private bool _repair;

        private enum _condition
        {
            broken = 0,
            bad = 1,
            worn = 2,
            scratced = 3,
            perfect = 4
        }

    }
}
 