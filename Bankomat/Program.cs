namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] acountsDaniel = { 500, 2000 };
            decimal[] acountsTobias = { 100055.50m };
            decimal[] acountsMarkus = { 1531.19m, 0, 2525.0m };
            decimal[] acountsSandra = { 0, 50000, 460.99m, 23000 };
            decimal[] acountsEmma = { 777.7m , 0 , 0, 50679.35m, 479549.50m };
            string[] acountNames = { "Lönekonto", "Sparkonto", "Matkonto", "Semesterkonto", "Pensionskonto" };

            bool isRunning = true;

            // Greet user at startup

            Console.WriteLine("Välkomen till Sparbanken!");

            while (isRunning)
            {
                // Logging in to account
                int userProfile = 0;
                int loggInAtempts = 0;
                do
                {
                    Console.Write("Ange användarnamn:");
                    string username = Console.ReadLine().ToUpper();

                    Console.Write("Ange lösenord:");
                    int pinCode;
                    while (!int.TryParse(Console.ReadLine(), out pinCode)) 
                    { 
                        Console.WriteLine("Du kan enbart ange siffror");
                        Console.Write("Ange lösenord:");
                    }

                    if (username == "DANIEL" && pinCode == 12345) { userProfile = 1; }

                    else if (username == "TOBIAS" && pinCode == 54321) { userProfile = 2; }

                    else if (username == "MARKUS" && pinCode == 67890) { userProfile = 3; }

                    else if (username == "SANDRA" && pinCode == 09876) { userProfile = 4; } 
                    
                    else if (username == "EMMA" && pinCode == 01234) { userProfile = 5; }

                        loggInAtempts++;

                }while (userProfile == 0 && loggInAtempts < 3);

                switch (userProfile)
                {
                    case 0:
                        isRunning = false; // Prgoram shuts down
                        break;

                    case 1:
                        
                        Menu(acountsDaniel, acountNames);
                        break;

                    case 2:
                        Menu(acountsTobias, acountNames);
                        break; 

                    case 3:
                        Menu(acountsMarkus, acountNames);
                        break;
                    case 4:
                        Menu(acountsSandra, acountNames);
                        break;
                    case 5:
                        Menu(acountsEmma, acountNames);
                        break;
                }               
            }
        }        
            
        public static void Menu(decimal[] acounts, string[] acountNames)
        {
            // Menu with functions

            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Vad vill du göra?\n");
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");

                int menuOption;

                while (!int.TryParse(Console.ReadLine(), out menuOption))
                {
                    Console.WriteLine("Ogiltigt val");
                }

                switch (menuOption)
                {
                    case 1:
                        // method see acounts and balance
                        PrintAcounts(acounts, acountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 2:
                        // method transfere between acounts
                        TransferringMoney(acounts, acountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 3:
                        // method withdraw money
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 4:
                        runMenu = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        Console.ReadKey();
                        break;
                }
            }
        }   

        // method for printing user acounts and current balance

        public static void PrintAcounts(decimal[] acounts, string[] acountNames) 
        {
            for (int i = 0; i < acounts.Length; i++) 
            {
                Console.WriteLine($"{acountNames[i]}: {acounts[i]} sek"); 
            }
        }

        // method for transfering money between acounts

        public static void TransferringMoney(decimal[] acounts, string[] acountNames)

            // Method incomplete. Able to select same acount for both withdrawing and deposit money.
            // Able to withdraw more money than avaliable on the acount resulting in a negativ balance.
            // Able to to select a number higher than the number of acounts resulting in an exception. 
        {
            Console.WriteLine("Välj vilket konto du vill flytta pengar från.");
            for (int i = 0; i < acounts.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {acountNames[i]}");
            }

            int acountNumberFrom;

            while (!int.TryParse(Console.ReadLine(), out acountNumberFrom)) // Need to secure from out of range exception
            {
                Console.WriteLine("Ogiltigt val"); 
            }

            Console.WriteLine("Välj vilket konto du vill flytta pengar till.");
            for (int i = 0; i < acounts.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {acountNames[i]}");
            }

            int acountNumberTo;

            while (!int.TryParse(Console.ReadLine(),out acountNumberTo)) // Need to secure from out of range exception
            {
                Console.WriteLine("Ogiltigt val");
            }

            Console.WriteLine("Hur mycket vill du överföra?");

            decimal amountToTransfer;

            while (!decimal.TryParse(Console.ReadLine(), out amountToTransfer)) { Console.WriteLine("Ogiltigt val"); }

            acounts[acountNumberFrom - 1] -= amountToTransfer; // Out keyword skips this part

            acounts[acountNumberTo - 1] += amountToTransfer; // Out keyword goes directly here, don't know why

            Console.WriteLine($"{acountNames[acountNumberFrom - 1]}: {acounts[acountNumberFrom - 1]} sek");
            Console.WriteLine($"{acountNames[acountNumberTo - 1]}: {acounts[acountNumberTo - 1]} sek");
        }

        // method for withdrawing money
    }
}