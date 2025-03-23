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

        public static void addGame(Boardgame newGame)
        {
            var existingBoardgame = Boardgames.FirstOrDefault(bg => bg.name == newGame.name && bg.edition == newGame.edition && bg.gameCondition == newGame.gameCondition);

            if (existingBoardgame != null)
            {
                // If found, increase the amount
                existingBoardgame._amount += newGame.amount;
                Console.WriteLine($"Antallet af {newGame.name} er opdateret til {existingBoardgame.amount}.");
                Files.SaveListToFile("boardgames.txt");
            }
            else
            {
                // If not found, create a new boardgame
                Boardgame newBoardgame = new Boardgame(newGame.name, newGame.edition, newGame.genre, newGame.playerAmount, newGame.price, newGame.gameCondition, newGame.amount);
                Boardgames.Add(newBoardgame);
                Files.SaveOneToFile("boardgames.txt", newBoardgame);
                Console.WriteLine($"Ny {newGame.name} spil er tilføjet.");
            }
           
        }

        public static void PrintListWares()
        {
            int index = 1;
            foreach (var boardgame in Boardgames)
            {
                Console.WriteLine($"{index}. Name: {boardgame.name}, Edition: {boardgame.edition}, Genre: {boardgame.genre}, Players: {boardgame.playerAmount}, Price: {boardgame.price}, Stand: {boardgame.gameCondition}, Antal: {boardgame.amount}");
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

            var searchResults = Boardgames.Where(bg =>
                (string.IsNullOrWhiteSpace(nameSearch) || bg.name.ToLower().Contains(nameSearch)) &&
                (string.IsNullOrWhiteSpace(editionSearch) || bg.edition.ToLower().Contains(editionSearch)) &&
                (string.IsNullOrWhiteSpace(genreSearch) || bg.genre.ToLower().Contains(genreSearch)) &&
                (minPlayers == 0 || bg.playerAmount >= minPlayers) &&
                (string.IsNullOrWhiteSpace(conditionSearch) || bg.gameCondition.ToString().ToLower().Contains(conditionSearch))
            ).ToList();

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Search Results:");
                int index = 1;
                foreach (var boardgame in searchResults)
                {
                    Console.WriteLine($"{index}. Name: {boardgame.name}, Edition: {boardgame.edition}, Genre: {boardgame.genre}, Players: {boardgame.playerAmount}, Price: {boardgame.price}, Stand: {boardgame.gameCondition}, Antal: {boardgame.amount}");
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
