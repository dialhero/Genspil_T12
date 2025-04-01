using System;
using System.Collections;
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


        public static void PrintListWaress()
        {
            var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();

            foreach (var boardgame in sortedBoardgames)
            {
                Console.WriteLine($"{Program.allBoardgames.IndexOf(boardgame)}. Name: {boardgame.Name}, Edition: {boardgame.Edition}, Genre: {boardgame.Genre}, Players: {boardgame.PlayerAmount}, Price: {boardgame.Price}, Stand: {boardgame.GameCondition}, Antal: {boardgame.Amount}");
            }
        }

        public static void Search()
        {
            Console.WriteLine("Indtast navn (Tryk enter hvis du vil springe over): ");
            string nameSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Indtast edition (Tryk enter hvis du vil springe over): ");
            string editionSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Indtast genre (Tryk enter hvis du vil springe over): ");
            string genreSearch = Console.ReadLine().ToLower();

            Console.WriteLine("Indtast spillere (Tryk enter hvis du vil springe over): ");
            string playerInput = Console.ReadLine();
            int minPlayers = string.IsNullOrWhiteSpace(playerInput) ? 0 : int.Parse(playerInput);

            Console.WriteLine("Indtast stand (Tryk enter hvis du vil springe over): ");
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
                Console.WriteLine("Søgeresultater:");
                int index = 1;
                foreach (var boardgame in searchResults)
                {
                    Console.WriteLine($"{index}. Navn: {boardgame.Name}, Udgave: {boardgame.Edition}, Genre: {boardgame.Genre}, Antal Spillere: {boardgame.PlayerAmount}, Pris: {boardgame.Price}, Stand: {boardgame.GameCondition}, Antal: {boardgame.Amount}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Hov! Der var ikke noget, der passede på dine søgekriterier!");
            }
        }
        public static void PrintListWares22()
        {
            
            var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();


            Console.WriteLine("+---------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("| {0,-5} | {1,-30} | {2,-20} | {3,-8} | {4,-8} | {5,-9} | {6,-5} |", "Index", "Navn", "Edition", "Players", "Pris", "Stand", "Antal");
            Console.WriteLine("+---------------------------------------------------------------------------------------------------------+");
            foreach (var boardgame in sortedBoardgames)
            {
                int x = Program.allBoardgames.IndexOf(boardgame);
                Console.WriteLine("| {0,-5} | {1,-30} | {2,-20} | {3,-8} | {4,-8} | {5,-9} | {6,-5} |", Program.allBoardgames.IndexOf(boardgame), boardgame.Name, boardgame.Edition, boardgame.PlayerAmount, boardgame.Price, boardgame.GameCondition, boardgame.Amount);
                Console.WriteLine("+---------------------------------------------------------------------------------------------------------+");
                
            }
        }
        public static void PrintListWares()
        {
            var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();

            Console.Write($@"
| {"Index",-5}   | {"Navn",-30} | {"Udgave",-20} | {"Spillere",-8} | {"Pris",-8:f2} | {"Stand",-9} | {"Antal",-5} |
|-----------------------------------------------------------------------------------------------------------|");
            foreach (var boardgame in sortedBoardgames)
            {
                int x = Program.allBoardgames.IndexOf(boardgame);
                Console.Write($@"
| {Program.allBoardgames.IndexOf(boardgame),-5}   | {boardgame.Name,-30} | {boardgame.Edition,-20} | {boardgame.PlayerAmount,-8} | {boardgame.Price,-8:f2} | {boardgame.GameCondition,-9} | {boardgame.Amount,-5} |
|-----------------------------------------------------------------------------------------------------------|");

            }
        }

    }
}
