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
        private int _amount;
        private int _playerAmount;
        private double _price;
        private Condition _gameCondition;

        public static List<Boardgame> Boardgames = new List<Boardgame>();

        public enum Condition
        {
            broken = 0,
            bad = 1,
            worn = 2,
            scratched = 3,
            perfect = 4
        }

        public Boardgame(string name, string edition, string genre, int playerAmount, double price, Condition gameCondition, int amount)
        {
            this._name = name;
            this._edition = edition;
            this._genre = genre;
            this._playerAmount = playerAmount;
            this._price = price;
            this._gameCondition = gameCondition;
            this._amount = amount;

        }
        public string name => _name;
        public string edition => _edition;
        public string genre => _genre;
        public int amount => _amount;
        public int playerAmount => _playerAmount;
        public double price => _price;
        public Condition gameCondition => _gameCondition;


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

            Console.WriteLine("Indtast spillets stand (0=broken, 1=bad, 2=worn, 3=scratched, 4=perfect): ");
            int gameConditionInt = Convert.ToInt32(Console.ReadLine());

            // Check if the entered value is a valid enum value
            if (!Enum.IsDefined(typeof(Condition), gameConditionInt))
            {
                Console.WriteLine("Ugyldig stand, sætter til 'perfect'.");
                gameConditionInt = (int)Condition.perfect;
            }
            Condition gameCondition = (Condition)gameConditionInt;

            Console.WriteLine("hvor mange spil: ");
            int amount = Convert.ToInt32(Console.ReadLine());

            var existingBoardgame = Boardgames.FirstOrDefault(bg => bg.name == name && bg.edition == edition && bg.gameCondition == gameCondition);

            if (existingBoardgame != null)
            {
                // If found, increase the amount
                existingBoardgame._amount += amount;
                Console.WriteLine($"Antallet af {name} er opdateret til {existingBoardgame.amount}.");
                Files.SaveListToFile("boardgames.txt");
            }
            else
            {
                // If not found, create a new boardgame
                Boardgame newBoardgame = new Boardgame(name, edition, genre, playerAmount, price, gameCondition, amount);
                Boardgames.Add(newBoardgame);
                Files.SaveOneToFile("boardgames.txt", newBoardgame);
                Console.WriteLine($"Ny {name} spil er tilføjet.");
            }

            return existingBoardgame ?? new Boardgame(name, edition, genre, playerAmount, price, gameCondition, amount);

        }

        public static void PrintListWares()
        {
            foreach (var boardgame in Boardgames)
            {
                Console.WriteLine($"Name: {boardgame.name}, Edition: {boardgame.edition}, Genre: {boardgame.genre}, Players: {boardgame.playerAmount}, Price: {boardgame.price}, Stand: {boardgame.gameCondition}, Antal: {boardgame.amount}");
            }
        }



    }
}
