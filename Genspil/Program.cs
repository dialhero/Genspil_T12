namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer.LoadCustomersFromFile(); // Indlæs kunder fra fil ved programstart
            Customer customerManager = new Customer();  // Instans af Customer til at tilføje kunder


            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Tilføj et spil");
                Console.WriteLine("2. Print listen af spil");
                Console.WriteLine("3. Print filen ud");
                Console.WriteLine("4. Tilføj kunde");
                Console.WriteLine("5. Fjern kunde");
                Console.WriteLine("6. Gem kundeliste til fil\n");

                //Console.WriteLine("99. Vis kunder\n");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Boardgame.NewBoardgame();
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
                        Customer.SaveCustomersToFile();
                        Console.WriteLine("Kunder gemt til fil.");
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
