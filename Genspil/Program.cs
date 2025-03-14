namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. tilføj et spil");
                Console.WriteLine("2. print listen af spil");
                Console.WriteLine("3. print filen ud");
               

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
                    default:
                        return;

                }
            }
        }
    }
}
