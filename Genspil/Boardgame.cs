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
        private Guid _id;

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
            this._id = Guid.NewGuid();
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Edition
        {
            get => _edition;
            set => _edition = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public int PlayerAmount
        {
            get => _playerAmount;
            set => _playerAmount = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }

        public Condition GameCondition
        {
            get => _gameCondition;
            set => _gameCondition = value;
        }


        public static void PrintListWares()
        {
            int index = 1;

            var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();

            foreach (var boardgame in sortedBoardgames)
            {
                Console.WriteLine($"{index}. Name: {boardgame.Name}, Edition: {boardgame.Edition}, Genre: {boardgame.Genre}, Players: {boardgame.PlayerAmount}, Price: {boardgame.Price}, Stand: {boardgame.GameCondition}, Antal: {boardgame.Amount}");
                index++;
            }
        }

        public static void Search()
        {
            Console.WriteLine("Enter navn (Hold den blank hvis du vil skip): ");
            string nameSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Enter edition (Hold den blank hvis du vil skip): ");
            string editionSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Enter genre (Hold den blank hvis du vil skip): ");
            string genreSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Enter spillere (Hold den blank hvis du vil skip): ");
            string playerInput = Console.ReadLine();
            int minPlayers = string.IsNullOrWhiteSpace(playerInput) ? 0 : int.Parse(playerInput);

            Console.WriteLine("Enter stand (Hold den blank hvis du vil skip): ");
            string conditionSearch = Console.ReadLine().ToLower();

            var searchResults = Program.allBoardgames.Where(bg =>
                (string.IsNullOrWhiteSpace(nameSearch) || bg.Name.ToLower().Contains(nameSearch)) &&
                (string.IsNullOrWhiteSpace(editionSearch) || bg.Edition.ToLower().Contains(editionSearch)) &&
                (string.IsNullOrWhiteSpace(genreSearch) || bg.Genre.ToLower().Contains(genreSearch)) &&
                (minPlayers == 0 || bg.PlayerAmount >= minPlayers) &&
                (string.IsNullOrWhiteSpace(conditionSearch) || bg.GameCondition.ToString().ToLower().Contains(conditionSearch))
            ).ToList();

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Search Results:");
                int index = 1;
                foreach (var boardgame in searchResults)
                {
                    Console.WriteLine($"{index}. Name: {boardgame.Name}, Edition: {boardgame.Edition}, Genre: {boardgame.Genre}, Players: {boardgame.PlayerAmount}, Price: {boardgame.Price}, Stand: {boardgame.GameCondition}, Antal: {boardgame.Amount}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine("No board games found matching your search criteria.");
            }
        }


    }
}
