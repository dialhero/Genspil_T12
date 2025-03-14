using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static List<Boardgame> Boardgames = new List<Boardgame>();

        private enum condition
        {
            broken = 0,
            bad = 1,
            worn = 2,
            scratced = 3,
            perfect = 4
        }

        public Boardgame(string name, string edition, string genre, int playerAmount, double price)
        {
            this._name = name;
            this._edition = edition;
            this._genre = genre;
            this._playerAmount = playerAmount;
            this._price = price;

        }
        public string name => _name;
        public string edition => _edition;
        public string genre => _genre;
        public int playerAmount => _playerAmount;
        public double price => _price;


        public static Boardgame NewBoardgame()
        {
            Console.WriteLine("intast spillets navn: ");
            string name = Console.ReadLine();

            Console.WriteLine("indtast spillets udgave: ");
            string edition = Console.ReadLine();

            Console.WriteLine("indtast spillets genre: ");
            string genre = Console.ReadLine();

            Console.WriteLine("indtast spillets player amount: ");
            int playerAmount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("indtast spillets pris: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Boardgame newboardgame = new Boardgame(name, edition, genre, playerAmount, price);

            Boardgames.Add(newboardgame);

            Files.SaveToFile("boardgames.txt");

            return newboardgame;
        }

        public static void PrintListWares()
        {
            foreach (var boardgame in Boardgames)
            {
                Console.WriteLine($"Name: {boardgame.name}, Edition: {boardgame.edition}, Genre: {boardgame.genre}, Players: {boardgame.playerAmount}, Price: {boardgame.price}");
            }
        }

      


    }
}
 