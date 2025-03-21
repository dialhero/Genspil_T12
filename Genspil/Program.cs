using static Genspil.Boardgame;

namespace Genspil
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Files.LoadCustomersFromFile(); // Indlæs kunder fra fil ved programstart
            Customer customerManager = new Customer();  // Instans af Customer til at tilføje kunder
            Files.LoadBoardgamesFromFile("boardgames.txt");


            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Tilføj et spil");
                Console.WriteLine("2. Print listen af spil");
                Console.WriteLine("3. Print filen ud");
                Console.WriteLine("4. Tilføj kunde");
                Console.WriteLine("5. Fjern kunde");
                Console.WriteLine("6. TOM PLADS ");
                Console.WriteLine("7. Opret forespørgsel på et spil");


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

                        Boardgame.addGame(boardgame);
                        break;
                    
                    case "2":
                        Boardgame.PrintListWares();
                        break;

                    case "3":
                        Files.PrintFromFile("boardgames.txt");
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

                        customerManager.AddCustomer(name, email, phoneNumber);

                        break;

                    case "5":
                        Console.WriteLine("\nIndtast telefonnummeret på kunden, der skal fjernes:");
                        
                        int deletePhoneNumber;
                        while (!int.TryParse(Console.ReadLine(), out deletePhoneNumber))
                        {
                            Console.WriteLine("Fejl! Indtast et gyldigt telefonnummer:");
                        }

                        customerManager.DeleteCustomer(deletePhoneNumber);
                        break;

                    case "6":
                        Console.WriteLine("Her kunne der være noget");
                        
                        break;

                    case "7":
                        Console.WriteLine("Hvilket spil skal der oprettes en forespørgsel på?");
                        Console.WriteLine("Hvis spillet ikke er på listen nedenfor, skal du først oprette det!");
                        Boardgame.PrintListWares();
                        int game = Convert.ToInt32(Console.ReadLine()) - 1;
                        Console.WriteLine("Indtast kundens telefonnr.?");
                        int telefonNr = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Hvor mange spil forespørger kunden?");
                        int requestAmount = Convert.ToInt32(Console.ReadLine());
                        Request request = new Request(requestAmount, Boardgame.Boardgames[game], Customer.GetCustomerByPhoneNumber(telefonNr));
                        request.AddRequestToList();
                        break;

                    case "8":
                        //Request.PrintRequestList();
                        break;

                     case "99":
                     Customer.ShowCustomers();

                     break;
                    

                    default:
                        return;

                }
            }
        }
    }
}


/* Ideer til søgefunktionen - søgetermer adskilt af kommaer. 
 * 
 * Skal vi opdatere playerAmount? (min max)
 *  
 * PrintListWares, der skal printes til medarbejderne skal inkludere broken spil, men ikke requested spil. OBS på listen! 
 * 
 * Kan vi inkluderer filerne i vores Git?
 * 
 */