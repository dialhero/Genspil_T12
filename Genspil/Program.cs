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

            Files.LoadCustomersFromFile();

            Customer customerManager = new Customer();  // Instans af Customer til at tilføje kunder

            Files.LoadBoardgames();



            while (true)
            {
                Console.WriteLine("\n----Menu----:");
                Console.WriteLine("1. Tilføj et brætspil");
                Console.WriteLine("2. Print listen af brætspil");
                Console.WriteLine("3. Print filen ud");
                Console.WriteLine("4. Tilføj kunde");
                Console.WriteLine("5. Fjern kunde");
                Console.WriteLine("6. Søg efter brætspil ");
                Console.WriteLine("7. Opret forespørgsel på et brætspil");
                Console.WriteLine("8. Print forespørgselsliste");


                //Console.WriteLine("99. Vis kunder\n");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Boardgame.NewBoardgame();
                        Console.WriteLine("Indtast spillets navn: ");
                        string boardName = Console.ReadLine();

                        Console.WriteLine("Indtast spillets udgave: ");
                        string edition = Console.ReadLine();

                        Console.WriteLine("Indtast spillets genre: ");
                        string genre = Console.ReadLine();

                        Console.WriteLine("Indtast spillets player amount: ");
                        int playerAmount = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Indtast spillets pris: ");
                        double price = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Indtast spillets stand (0=broken, 1=bad, 2=worn, 3=scratched, 4=perfect): ");
                        int gameConditionInt = Convert.ToInt32(Console.ReadLine());

                        // Tjek om værdien er en gyldig enum-værdi
                        if (!Enum.IsDefined(typeof(Condition), gameConditionInt))
                        {
                            Console.WriteLine("Ugyldig stand, sætter til 'perfect'.");
                            gameConditionInt = (int)Condition.perfect;
                        }
                        Condition gameCondition = (Condition)gameConditionInt;

                        Console.WriteLine("Hvor mange spil: ");
                        int amount = Convert.ToInt32(Console.ReadLine());


                        Boardgame boardgame = new Boardgame(boardName, edition, genre, playerAmount, price, gameCondition, amount);

                        AddGame(boardgame);
                        Files.SaveBoardgameToFile(boardgame);

                        break;
                    
                    case "2":
                        Boardgame.PrintListWares();
                        break;

                    case "3":
                        Files.PrintBoardgamesFromFile();
                        break;

                    case "4":
                        Console.Write("\nIndtast kundens navn: ");
                        string name = Console.ReadLine();

                        Console.Write("Indtast kundens email: ");
                        string email = Console.ReadLine();

                        Console.Write("Indtast kundens telefonnummer: ");
                        int phoneNumber;
                        while (!int.TryParse(Console.ReadLine(), out phoneNumber))
                        {
                            Console.WriteLine("Fejl! Indtast et gyldigt telefonnummer:");
                        }

                        AddCustomer(name, email, phoneNumber); 
                        Files.SaveCustomersToFile();

                        break;

                    case "5":
                        Console.WriteLine("\nIndtast telefonnummeret på kunden, der skal fjernes:");
                        
                        int deletePhoneNumber;
                        while (!int.TryParse(Console.ReadLine(), out deletePhoneNumber))
                        {
                            Console.WriteLine("Fejl! Indtast et gyldigt telefonnummer:");
                        }

                        DeleteCustomer(deletePhoneNumber);
                        break;

                    case "6":
                        Search();
                        break;

                    case "7":
                        Console.WriteLine("Hvilket spil skal der oprettes en forespørgsel på?");
                        Console.WriteLine("Hvis spillet ikke er på listen nedenfor, skal du først oprette det!");
                        Search();
                        int game = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Indtast kundens telefonnr.?");
                        int customerPhoneNumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Hvor mange spil forespørger kunden?");
                        int requestAmount = Convert.ToInt32(Console.ReadLine());
                        Request request = new Request(requestAmount, Customer.GetCustomerByPhoneNumber(customerPhoneNumber));
                        
                        Request.AddRequestToList(request, Customer.GetCustomerByPhoneNumber(customerPhoneNumber));
                        Files.SaveRequestToFile();
                        break;

                    case "8":
                        //Request list print

                     case "99":
                     Customer.ShowCustomers();

                     break;
                    

                    default:
                        return;

                }
            }
        }

        public static void AddGame(Boardgame newGame)
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


        public static void AddCustomer(string name, string email, int phoneNumber) //SaveCustomersToFile lagt ind i Files, og metoden kaldes efter AddNewCustomer
        {
            Customer newCustomer = new Customer(name, email, phoneNumber);
            customerList.Add(newCustomer);
            Files.SaveCustomersToFile();
            Console.WriteLine($"\nKunde {newCustomer.Name} tilføjet!");

        }


        public static void DeleteCustomer(int phoneNumber) //Vi fjerne kunde via telefonnummer. Vi tjekker også for om nummeret findes. 
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
        }




    }
}
