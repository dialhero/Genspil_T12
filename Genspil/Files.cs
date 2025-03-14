using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    internal class Files
    {
        public static void SaveToFile(string filename)
        {
            // Åbn StreamWriter i append-tilstand (true betyder "tilføj til filen")
            using (StreamWriter writer = new StreamWriter(filename, true)) // true betyder "append"
            {
                foreach (var boardgame in Boardgame.Boardgames)
                {
                    writer.WriteLine($"Name: {boardgame.name}, Edition: {boardgame.edition}, Genre: {boardgame.genre}, Players: {boardgame.playerAmount}, Price: {boardgame.price}");
                }
            }
        }
        public static void PrintFromFile(string filename)
        {
            try
            {
                // Åbn filen og læs den linje for linje
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            }
        }
        public static void LoadBoardgamesFromFile(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        string name = data[0];
                        string edition = data[1];
                        string genre = data[2];
                        int playerAmount = int.Parse(data[3]);
                        double price = double.Parse(data[4]);
                       

                        Boardgame boardgame = new Boardgame(name, edition, genre, playerAmount, price);
                        Boardgame.Boardgames.Add(boardgame);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
        }
    }
}
