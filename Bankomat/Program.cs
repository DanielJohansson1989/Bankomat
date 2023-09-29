namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] accountsDaniel = { 500, 2000 };
            decimal[] accountsTobias = { 100055.50m };
            decimal[] accountsMarkus = { 1531.19m, 0, 2525.0m };
            decimal[] accountsSandra = { 0, 50000, 460.99m, 23000 };
            decimal[] accountsEmma = { 777.7m , 0 , 0, 50679.35m, 479549.50m };
            string[] accountNames = { "Lönekonto", "Sparkonto", "Matkonto", "Semesterkonto", "Pensionskonto" };

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
                        
                        Menu(accountsDaniel, accountNames);
                        break;

                    case 2:
                        Menu(accountsTobias, accountNames);
                        break; 

                    case 3:
                        Menu(accountsMarkus, accountNames);
                        break;
                    case 4:
                        Menu(accountsSandra, accountNames);
                        break;
                    case 5:
                        Menu(accountsEmma, accountNames);
                        break;
                }               
            }
        }        
            
        public static void Menu(decimal[] accounts, string[] accountNames)
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
                        PrintAcounts(accounts, accountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 2:
                        // method transfere between acounts
                        TransferringMoney(accounts, accountNames);
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

        // method for printing user accounts and current balance

        public static void PrintAcounts(decimal[] accounts, string[] accountNames) 
        {
            for (int i = 0; i < accounts.Length; i++) 
            {
                Console.WriteLine($"{accountNames[i]}: {accounts[i]} sek"); 
            }
        }

        // method for transfering money between accounts

        public static void TransferringMoney(decimal[] accounts, string[] accountNames) 
        {
            // Print available accounts. Exclude account with no money 
            Console.WriteLine("Välj vilket konto du vill flytta pengar från.");
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] > 0)
                {
                    Console.WriteLine($"{i + 1} - {accountNames[i]}");
                }
            }

            // Acceptable user input is only the numbers of the available acounts
            int accountNumberFrom;
            do 
            {
                while (!int.TryParse(Console.ReadLine(), out accountNumberFrom)) 
                {
                    Console.WriteLine("Ogiltigt val");
                }

                if (accountNumberFrom > accounts.Length || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberFrom > accounts.Length || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0);

            // Print availiable acounts for deposit. 
            Console.WriteLine("Välj vilket konto du vill flytta pengar till.");
            for (int i = 0; i < accounts.Length; i++)
            {
                // Excluding account where money is tranfered from.
                if (i != accountNumberFrom - 1)
                {
                    Console.WriteLine($"{i + 1} - {accountNames[i]}");
                }
            }

            // Acceptable user input is only the number of the accounts printed above
            int accountNumberTo;
            do
            {
                while (!int.TryParse(Console.ReadLine(), out accountNumberTo))
                {
                    Console.WriteLine("Ogiltigt val");
                }

                if (accountNumberTo > accounts.Length || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberTo > accounts.Length || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom);

            Console.WriteLine("Hur mycket vill du överföra?");

            decimal amountToTransfer;

            // Accepted user input to transfer is more than 0 and tops at current balance
            // of the account transferred from.
            do
            {
                while (!decimal.TryParse(Console.ReadLine(), out amountToTransfer))
                {
                    Console.WriteLine("Ogiltigt val");
                }

                if (amountToTransfer <= 0) 
                { 
                    Console.WriteLine("Ange ett belopp större än 0"); 
                }

                else if (amountToTransfer > accounts[accountNumberFrom - 1])
                {
                    Console.WriteLine("Övertrassering ej tillåtet");
                }

            } while (amountToTransfer <= 0 || amountToTransfer > accounts[accountNumberFrom - 1]);

            // Transferring money and printing the current balance
            accounts[accountNumberFrom - 1] -= amountToTransfer; 
            accounts[accountNumberTo - 1] += amountToTransfer;

            Console.WriteLine($"{accountNames[accountNumberFrom - 1]}: {accounts[accountNumberFrom - 1]} sek");
            Console.WriteLine($"{accountNames[accountNumberTo - 1]}: {accounts[accountNumberTo - 1]} sek");
        }

        // method for withdrawing money
    }
}