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



    }
}
