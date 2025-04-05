using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();

            Console.WriteLine(

          "Name".PadRight(28) +
          "Edition".PadRight(23) +
          "Genre".PadRight(15) +
          "Players".PadRight(10) +
          "Price".PadRight(10) +
          "Stand".PadRight(10) +
          "Antal".PadRight(10));

            foreach (var boardgame in sortedBoardgames)
            {
                Console.WriteLine(
                    $"{Program.allBoardgames.IndexOf(boardgame)}.".PadRight(5) +
                    boardgame.Name.PadRight(23) +
                    boardgame.Edition.PadRight(23) +
                    boardgame.Genre.PadRight(15) +
                    boardgame.PlayerAmount.ToString().PadRight(10) +
                    boardgame.Price.ToString().PadRight(10) +
                    boardgame.GameCondition.ToString().PadRight(10) +
                    boardgame.Amount.ToString().PadRight(10));


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
                foreach (var boardgame in searchResults)
                {
                    Console.WriteLine($"{Program.allBoardgames.IndexOf(boardgame)}. Navn: {boardgame.Name}, Udgave: {boardgame.Edition}, Genre: {boardgame.Genre}, Antal Spillere: {boardgame.PlayerAmount}, Pris: {boardgame.Price}, Stand: {boardgame.GameCondition}, Antal: {boardgame.Amount}");
                }
            }
            else
            {
                Console.WriteLine("Hov! Der var ikke noget, der passede på dine søgekriterier!");
            }
        }

        public static void UpdateAmountByIndex(int index, int change)
        {
            if (index >= 0 && index < Program.allBoardgames.Count)
            {
                Boardgame selectedGame = Program.allBoardgames[index];
                int newAmount = selectedGame.Amount + change;

                if (newAmount < 0)
                {
                    Console.WriteLine($"Fejl - du kan ikke have færre end 0 af '{selectedGame.Name}' på lager.");
                }
                else
                {
                    selectedGame.SetAmount(newAmount);
                    Console.WriteLine($"Antallet af '{selectedGame.Name}' er nu opdateret til {selectedGame.Amount} stk.");
                    Console.ReadLine();
                }
            }
            else
            {

                Console.WriteLine("Ugyldigt index");
            }
            Files.SaveBoardgamesToFile();

        }



        public void SetAmount(int newAmount)
        {
            _amount = newAmount;
        }


        public static void WarehouseListSorted()
        {
            Console.WriteLine("skal den sortere efter. 1) navn eller 2) Genre?");
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($@"
| {"Index",-5}   | {"Navn",-30} | {"Udgave",-20} |{"Genre",-10}| {"Spillere",-8} | {"Pris",-8} | {"Stand",-9} | {"Antal",-5} |");
            Console.Write($@"
|-----------------------------------------------------------------------------------------------------------------------|");
            if (choice == 1)
            {
                var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Name).ToList();
                foreach (var boardgame in sortedBoardgames)
                {
                    if (boardgame.Amount > 0)
                        Console.Write($@"
| {Program.allBoardgames.IndexOf(boardgame),-5}   | {boardgame.Name,-30} | {boardgame.Edition,-20} |{boardgame.Genre,-10} | {boardgame.PlayerAmount,-8} | {boardgame.Price,-8} | {boardgame.GameCondition,-9} | {boardgame.Amount,-5} |
|-----------------------------------------------------------------------------------------------------------------------|");

                }
            }
            if (choice == 2)
            {
                var sortedBoardgames = Program.allBoardgames.OrderBy(bg => bg.Genre).ToList();
                foreach (var boardgame in sortedBoardgames)
                {
                    if (boardgame.Amount > 0)
                        Console.Write($@"
| {Program.allBoardgames.IndexOf(boardgame),-5}   | {boardgame.Name,-30} | {boardgame.Edition,-20} |{boardgame.Genre,-10} | {boardgame.PlayerAmount,-8} | {boardgame.Price,-8} | {boardgame.GameCondition,-9} | {boardgame.Amount,-5} |
|-----------------------------------------------------------------------------------------------------------------------|");

                }
            }

        }

        public static Boardgame ParseBoardgameFromString(string gameData)
        {
            string[] parts = gameData.Split(',');

            if (parts.Length < 6)
            {
                throw new FormatException("Invalid boardgame data format.");
            }

            string name = parts[0].Trim();
            string edition = parts[1].Trim();
            string genre = parts[2].Trim();
            string playerInfo = parts[3].Trim();
            string priceInfo = parts[4].Trim();
            string conditionInfo = parts[5].Trim();

            // Parse playerAmount (spillere)
            int playerAmount = int.Parse(playerInfo.Split(' ')[0]);

            // Parse price (pris)
            double price = double.Parse(priceInfo.Split(' ')[0]);

            // Parse game condition (tilstand)
            Condition gameCondition = (Condition)Enum.Parse(typeof(Condition), conditionInfo.Split(':')[1].Trim());

            // Opret et Boardgame objekt
            Boardgame boardgame = new Boardgame(name, edition, genre, playerAmount, price, gameCondition, 0); // Sætter Amount til 0 (kan justeres senere)

            return boardgame;
        }
    }
}
