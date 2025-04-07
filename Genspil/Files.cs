using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Genspil.Boardgame;

namespace Genspil
{
    class Files
    {
        private static readonly string BoardgameFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\mock_boardgames.txt");
        private static readonly string RequestFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\mock_requests.csv");
        private static readonly string CustomerFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\mock_customers.csv");

        public static void SaveOneToFile(string filename, Boardgame boardgame)
        {
            // Åbn StreamWriter i append-tilstand (true betyder "tilføj til filen")
            using (StreamWriter writer = new StreamWriter(filename, true)) // true betyder "append"
            {
                writer.WriteLine($"{boardgame.Name},{boardgame.Edition},{boardgame.Genre},{boardgame.PlayerAmount},{boardgame.Price},{boardgame.GameCondition},{boardgame.Amount}");
            }
        }
        public static void SaveListToFile(string filename)
        {
            // Åbn StreamWriter i append-tilstand (true betyder "tilføj til filen")
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var boardgame in Program.allBoardgames)
                {
                    writer.WriteLine($"{boardgame.Name},{boardgame.Edition},{boardgame.Genre},{boardgame.PlayerAmount},{boardgame.Price},{boardgame.GameCondition},{boardgame.Amount}");
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
                        Program.allBoardgames.Add(boardgame);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved loading: {ex.Message}");
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
        public static void SaveCustomersToFile()
        {
            // Console.WriteLine("Gemmer til fil: " + Path.GetFullPath(CustomerFilePath)); // Debug
            using (StreamWriter writer = new StreamWriter(CustomerFilePath))
            {
                foreach (var customer in Program.customerList)
                {
                    writer.WriteLine($"{customer.Name},{customer.Email},{customer.PhoneNumber}");
                }
            }

            Console.WriteLine($"Kunder gemt til fil: {CustomerFilePath}");

        }

        public static void SaveCustomerToFile(Customer customer)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CustomerFilePath, append: true))
                {
                    writer.WriteLine($"{customer.Name},{customer.Email},{customer.PhoneNumber}");
                }

                Console.WriteLine("Ny kunde gemt til fil.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl under gemning af kunde: {ex.Message}");
            }
        }

        public static void LoadCustomersFromFile()
        {
            try
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
                                Program.customerList.Add(new Customer(name, email, phoneNumber));
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Filen '{CustomerFilePath}' blev ikke fundet.");
            }
        }
        

        public static void SaveRequestsToFile(List<Customer> customers)
        {
            using (StreamWriter writer = new StreamWriter(RequestFilePath))
            {
                foreach (Customer customer in customers)
                {
                    writer.WriteLine($"Customer: {customer.Name},{customer.Email}, {customer.PhoneNumber}");

                    foreach (Request request in customer.requestList)
{
                        writer.WriteLine($"Antal:  {request.Amount}");

                        foreach (Boardgame game in request.Boardgames)
                        {
                            writer.WriteLine($"Boardgame: {game.Name}, {game.Edition}, {game.Genre}, {game.PlayerAmount} players, {game.Price} DKK, Condition: {game.GameCondition}");
                        }
                    }
                    writer.WriteLine(); // tom linje mellem kunder
                }
            }
        }

        public static List<Customer> LoadRequestsFromFile()
        {
            List<Customer> customers = new List<Customer>();
            Customer currentCustomer = null;
            Request currentRequest = null;

            if (File.Exists(RequestFilePath))
            {
                string[] lines = File.ReadAllLines(RequestFilePath);

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // Hvis linjen er tom, afslut den aktuelle kunde og request
                        // Tilføj kun request og kunde én gang her
                        if (currentRequest != null && currentCustomer != null)
                        {
                            currentCustomer.requestList.Add(currentRequest); // Tilføj den afsluttede request
                            currentRequest = null; // Nulstil den aktuelle request
                        }

                        if (currentCustomer != null)
                        {
                            customers.Add(currentCustomer); // Tilføj den afsluttede kunde
                            currentCustomer = null; // Nulstil den aktuelle kunde
                        }
                    }
                    else if (line.StartsWith("Customer:"))
                    {
                        // Hvis der er en eksisterende request, tilføj den til den nuværende kunde
                        if (currentRequest != null)
                        {
                            currentCustomer.requestList.Add(currentRequest);
                            currentRequest = null;
                        }

                        // Håndter kundeinformation
                        string customerData = line.Substring(9).Trim();
                        currentCustomer = Customer.ParseCustomerData(customerData);
                        Console.WriteLine($"Parsing customer: {currentCustomer.Name}");
                    }
                    else if (line.StartsWith("Antal:") && currentCustomer != null)
                    {
                        // Hvis der er en eksisterende request, tilføj den til den nuværende kunde
                        if (currentRequest != null)
                        {
                            currentCustomer.requestList.Add(currentRequest);
                            currentRequest = null;
                        }

                        // Håndter request-antal
                        string amountData = line.Substring(7).Trim();
                        if (int.TryParse(amountData, out int amount))
                        {
                            currentRequest = new Request(amount, currentCustomer);
                            Console.WriteLine($"Adding request for {amount} items");
                        }
                        else
                        {
                            throw new FormatException("Invalid amount format.");
                        }
                    }
                    else if (line.StartsWith("Boardgame:") && currentRequest != null)
                    {
                        // Håndter boardgame-information
                        string gameData = line.Substring(10).Trim();
                        var boardgame = ParseBoardgameFromString(gameData);
                        currentRequest.AddRequestBoardgame(boardgame);
                        Console.WriteLine($"Parsing boardgame: {gameData}");
                    }
                }
                // Tilføj sidste request og kunde, hvis ikke allerede gjort
                if (currentRequest != null && currentCustomer != null)
                {
                    currentCustomer.requestList.Add(currentRequest);
                }
                if (currentCustomer != null)
                {
                    customers.Add(currentCustomer);
                }
            }
            return customers; // Returner listen af kunder
        }

        public static void SaveRequestCustomerToFile(Customer customer)
        {
            using (StreamWriter writer = new StreamWriter(RequestFilePath, append: true)) // append = true tilføjer i stedet for at overskrive
            {
                writer.WriteLine($"Customer: {customer.Name},{customer.Email}, {customer.PhoneNumber}");

                if (customer.requestList != null && customer.requestList.Count > 0)
                {
                    foreach (Request request in customer.requestList)
                    {
                        writer.WriteLine($"Antal:  {request.Amount}");

                        foreach (Boardgame game in request.Boardgames)
                        {
                            writer.WriteLine($"Boardgame: {game.Name}, {game.Edition}, {game.Genre}, {game.PlayerAmount} players, {game.Price} DKK, Condition: {game.GameCondition}");
                        }
                    }
                }
                writer.WriteLine(); // tom linje mellem kunder
            }
        }
    }
}
