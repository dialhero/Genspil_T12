using static Genspil.Boardgame;

namespace Genspil
{
    internal class Program
    {
        //Lister er placeret i de klasser som har ansvaret for dem
        public static List<Customer> customerList = new List<Customer>();
        public static List<Boardgame> allBoardgames = new List<Boardgame>();

        static void Main(string[] args)
        {
            try
            {
                Files.LoadCustomersFromFile();

                // Instans af Customer til at tilføje kunder
                Customer customerManager = new Customer();

                Files.LoadBoardgames();
                
                List<Customer> loadedCustomers = Files.LoadRequestsFromFile();
                Program.customerList = loadedCustomers;

                while (true)
                {
                    Console.Clear();
                    systemWindow();
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1: //Tilføj et brætspil
                        case ConsoleKey.NumPad1:
                            Console.Clear();                            
                            Console.Write("Indtast spillets navn: ");
                            string boardName = Console.ReadLine();

                            Console.Write("Indtast spillets udgave: ");
                            string edition = Console.ReadLine();

                            Console.Write("Indtast spillets genre: ");
                            string genre = Console.ReadLine();

                            int playerAmount;
                            while (true)
                            {
                                Console.Write("Indtast spillets max antal spillere: ");
                                try
                                {
                                    playerAmount = Convert.ToInt32(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Indtast et tal!");
                                }
                            }
                            double price;
                            while (true)
                            {
                                Console.Write("Indtast spillets pris: ");
                                try
                                {
                                    price = Convert.ToInt32(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Indtast et tal!");
                                }
                            }

                            Console.Write("Indtast spillets stand (0=ødelagt, 1=dårlig, 2=brugt, 3=god, 4=perfekt): ");
                            int gameConditionInt = Convert.ToInt32(Console.ReadLine());

                            // Tjek om værdien er en gyldig enum-værdi
                            if (!Enum.IsDefined(typeof(Condition), gameConditionInt))
                            {
                                Console.WriteLine("Ugyldig stand, sætter til 'ødelagt'.");
                                gameConditionInt = (int)Condition.broken;
                            }
                            Condition gameCondition = (Condition)gameConditionInt;

                            Console.Write("Hvor mange spil: ");
                            int amount;
                            while (true)
                            {
                                try
                                {
                                    amount = Convert.ToInt32(Console.ReadLine());
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Indtast et tal!");
                                }
                            }
                            Boardgame boardgame = new Boardgame(boardName, edition, genre, playerAmount, price, gameCondition, amount);

                            AddBoardgame(boardgame);
                            Files.SaveBoardgameToFile(boardgame);
                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.Clear();
                            Boardgame.PrintListWares();
                            Console.ReadLine();
                            break;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.Clear();
                            Console.Write("\nIndtast kundens navn: ");
                            string name = Console.ReadLine();

                            Console.Write("Indtast kundens email: ");
                            string email = Console.ReadLine();
                            if (!email.Contains("@") || !email.Contains("."))
                            {
                                Console.WriteLine("Fejl! Ugyldig email!");
                                break;
                            }

                            Console.Write("Indtast kundens telefonnummer: ");
                            int phoneNumber;
                            while (!int.TryParse(Console.ReadLine(), out phoneNumber))
                            {
                                Console.WriteLine("Fejl! Indtast et gyldigt telefonnummer:");
                            }
                            AddCustomer(name, email, phoneNumber);
                            break;

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Console.Clear();
                            Console.WriteLine("\nIndtast telefonnummeret på kunden, der skal fjernes:");

                            int deletePhoneNumber;
                            while (!int.TryParse(Console.ReadLine(), out deletePhoneNumber))
                            {
                                Console.WriteLine("Fejl! Indtast et gyldigt telefonnummer:");
                            }

                            DeleteCustomer(deletePhoneNumber);
                            break;

                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:

                            Console.Clear();
                            Search();
                            Console.ReadLine();
                            break;

                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            Console.Clear();
                            Console.WriteLine("Hvilket spil skal der oprettes en forespørgsel på?");
                            Console.WriteLine("Søg efter det spil du vil tilføje og brug indeksnummeret.");
                            Search();
                            int gameIndex = Convert.ToInt32(Console.ReadLine());
                            Boardgame selectedGame = allBoardgames[gameIndex];
                            Console.WriteLine("Indtast kundens telefonnr.");
                            int customerPhoneNumber = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Hvor mange spil forespørger kunden?");
                            int requestAmount = Convert.ToInt32(Console.ReadLine());
                            Request request = new Request(requestAmount, Customer.GetCustomerByPhoneNumber(customerPhoneNumber));
                            Request.AddRequestToList(request, Customer.GetCustomerByPhoneNumber(customerPhoneNumber));
                            request.AddRequestBoardgame(selectedGame);
                            Files.SaveRequestsToFile(customerList);
                            break;

                        case ConsoleKey.D7:
                        case ConsoleKey.NumPad7:
                            Console.Clear();
                            Request.PrintAllRequestList();
                            Console.ReadLine();
                            break;

                        case ConsoleKey.D8: 
                        case ConsoleKey.NumPad8:
                            Console.Clear();
                            Console.WriteLine("---Opdater antal brætspil---");
                            Console.Write("Indtast index nr. på det brætspil, som du vil ændre antal på: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Indtast ændring i antal: ");
                            int updatedAmount = Convert.ToInt32(Console.ReadLine());
                            Boardgame.UpdateAmountByIndex(index, updatedAmount);
                            Files.SaveBoardgamesToFile();
                            break;

                        case ConsoleKey.D9:
                        case ConsoleKey.NumPad9:
                            Console.Clear();
                            Boardgame.WarehouseListSorted();
                            Console.ReadLine();
                            break;

                        case ConsoleKey.D0:
                            Console.Clear();
                            Console.WriteLine("Tak for i dag!");
                            Console.ReadLine();
                            return;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            { 
                    Console.WriteLine($"Uventet fejl: {ex.Message}");
            }
        }

        public static void AddBoardgame(Boardgame newGame)
        {
            var existingBoardgame = allBoardgames.FirstOrDefault(bg => bg.Name == newGame.Name && bg.Edition == newGame.Edition && bg.GameCondition == newGame.GameCondition);

            if (existingBoardgame != null)
            {
                // If found, increase the amount
                existingBoardgame.Amount += newGame.Amount;
                Console.WriteLine($"Antallet af {newGame.Name} er opdateret til {existingBoardgame.Amount}.");
                Files.SaveListToFile("boardgames.txt");
            }
            else
            {
                // If not found, create a new boardgame
                Boardgame newBoardgame = new Boardgame(newGame.Name, newGame.Edition, newGame.Genre, newGame.PlayerAmount, newGame.Price, newGame.GameCondition, newGame.Amount);
                allBoardgames.Add(newBoardgame);
                Files.SaveOneToFile("boardgames.txt", newBoardgame);
                Console.WriteLine($"Ny {newGame.Name} spil er tilføjet.");
            }
        }

        public static void AddCustomer(string name, string email, int phoneNumber) //SaveCustomerToFile lagt ind i Files, og metoden kaldes efter AddNewCustomer
        {
            Customer newCustomer = new Customer(name, email, phoneNumber);
            customerList.Add(newCustomer);
            Files.SaveCustomerToFile(newCustomer);
            Files.SaveRequestCustomerToFile(newCustomer);
            Console.WriteLine($"\nKunde {newCustomer.Name} tilføjet!");
        }

        public static void DeleteCustomer(int phoneNumber) //Vi fjerner kunde via telefonnummer. Vi tjekker også for om nummeret findes. 
        {
            Customer customerToRemove = customerList.Find(c => c.PhoneNumber == phoneNumber);
            if (customerToRemove != null)
            {
                customerList.Remove(customerToRemove);
                Console.WriteLine($"Kunde med telefonnummer {phoneNumber} er blevet fjernet.");
            }
            else
            {
                Console.WriteLine($"Ingen kunde fundet med telefonnummer {phoneNumber}.");
            }
            Files.SaveCustomersToFile();
            Files.SaveRequestsToFile(customerList);
        }

        public static void systemWindow()
        {
            Console.WriteLine(@" 
        | ---------------------------------------------------- |
        |                                                      |
        |       1. Tilføj et brætspil                          |
        |       2. Print liste af spil                         |
        |       3. Tilføj kunde                                |
        |       4. Fjern kunde                                 |
        |       5. Søg efter brætspil                          |
        |       6. Opret forespørgsel på et brætspil           |
        |       7. Print forespørgselsliste                    |
        |       8. Opdater antal brætspil                      |
        |       9. Lageroptælling                              |
        |       0. Afslut program                              |
        | ---------------------------------------------------- |");

        }
    }
}
