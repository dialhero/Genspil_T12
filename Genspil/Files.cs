﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Genspil.Boardgame;

namespace Genspil
{
    class Files
    {
        private static readonly string BoardgameFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\mock_boardgames.txt");

        public static void SaveOneToFile(string filename, Boardgame boardgame)
        {
            // Åbn StreamWriter i append-tilstand (true betyder "tilføj til filen")
            using (StreamWriter writer = new StreamWriter(filename, true)) // true betyder "append"
            {

                writer.WriteLine($"{boardgame.name},{boardgame.edition},{boardgame.genre},{boardgame.playerAmount},{boardgame.price},{boardgame.gameCondition},{boardgame.amount}");

            }
        }
        public static void SaveListToFile(string filename)
        {
            // Åbn StreamWriter i append-tilstand (true betyder "tilføj til filen")
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var boardgame in Boardgame.Boardgames)
                {
                    writer.WriteLine($"{boardgame.name},{boardgame.edition},{boardgame.genre},{boardgame.playerAmount},{boardgame.price},{boardgame.gameCondition},{boardgame.amount}");
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
                        Condition gameCondition = (Condition)Enum.Parse(typeof(Condition), data[5]);
                        int amount = int.Parse(data[6]);

                        Boardgame boardgame = new Boardgame(name, edition, genre, playerAmount, price, gameCondition, amount);
                        Boardgame.Boardgames.Add(boardgame);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
        }


        // Wrapper-metoder til boardgames med konstant sti
        public static void SaveBoardgamesToFile()
        {
            SaveListToFile(BoardgameFilePath);
        }

        public static void SaveBoardgameToFile(Boardgame boardgame)
        {
            SaveOneToFile(BoardgameFilePath, boardgame);
            Console.WriteLine("Gemmer boardgame til fil: " + Path.GetFullPath(BoardgameFilePath));

        }

        public static void PrintBoardgamesFromFile()
        {
            PrintFromFile(BoardgameFilePath);
        }

        public static void LoadBoardgames()
        {
            LoadBoardgamesFromFile(BoardgameFilePath);
        }




        //Her starter metoderne til at gemme og hente kunder fra mockdata

        private static readonly string CustomerFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\mock_customers.csv");


        public static void SaveCustomersToFile()
        {
            Console.WriteLine("Gemmer til fil: " + Path.GetFullPath(CustomerFilePath)); // Debug
            using (StreamWriter writer = new StreamWriter(CustomerFilePath))
            {
                foreach (var customer in Customer.CustomerList)
                {
                    writer.WriteLine($"{customer.Name},{customer.Email},{customer.PhoneNumber}");
                }
            }
            
            Console.WriteLine($"Kunder gemt til fil: {CustomerFilePath}");

        }

        public static void LoadCustomersFromFile()
        {
            if (File.Exists(CustomerFilePath))
            {
                string[] lines = File.ReadAllLines(CustomerFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        string name = parts[0];
                        string email = parts[1];
                        if (int.TryParse(parts[2], out int phoneNumber))
                        {
                            Customer.CustomerList.Add(new Customer(name, email, phoneNumber));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Filen '{CustomerFilePath}' blev ikke fundet.");
            }
        }

    }
}
