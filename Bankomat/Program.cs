namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<decimal> accountsDaniel = new List<decimal>() { 500, 2000 };
            List<decimal> accountsTobias = new List<decimal>() { 100055.50m };
            List<decimal> accountsMarkus = new List<decimal>() { 1531.19m, 0, 2525.0m };
            List<decimal> accountsSandra = new List<decimal>() { 0, 50000, 460.99m, 23000 };
            List<decimal> accountsEmma = new List<decimal>() { 777.7m, 0, 0, 50679.35m, 479549.50m };
            List<string> accountNamesDaniel = new List<string>() { "Lönekonto", "Sparkonto" };
            List<string> accountNamesTobias = new List<string>() { "Lönekonto" };
            List<string> accountNamesMarkus = new List<string>() { "Lönekonto", "Sparkonto", "Matkonto" };
            List<string> accountNamesSandra = new List<string>() { "Lönekonto", "Sparkonto", "Matkonto", "Semesterkonto" };
            List<string> accountNamesEmma = new List<string>() { "Lönekonto", "Sparkonto", "Matkonto", "Semesterkonto", "Pensionskonto" };

            int[] pinCodes = { 12345, 54321, 67890, 09876, 01234 };

            int loggInAttempts = 0;
            int pinCode;

            Console.WriteLine("Välkommen till Sparbanken!");
            Thread.Sleep(2000);

            // Let user logg in to account
            while (loggInAttempts < 3)
            {
                Console.Clear();
                Console.Write("Ange användarnamn:");
                string username = Console.ReadLine().ToUpper();

                Console.Write("Ange pinkod:");

                while (!int.TryParse(Console.ReadLine(), out pinCode))
                {
                    Console.WriteLine("Du kan enbart ange siffror");
                    Console.Write("Ange pinkod:");
                }

                int pinCodeIndex = Array.IndexOf(pinCodes, pinCode);

                loggInAttempts++;

                if (username == "DANIEL" && pinCodeIndex == 0)
                {
                    Menu(accountsDaniel, accountNamesDaniel, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "TOBIAS" && pinCodeIndex == 1)
                {
                    Menu(accountsTobias, accountNamesTobias, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "MARKUS" && pinCodeIndex == 2)
                {
                    Menu(accountsMarkus, accountNamesMarkus, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "SANDRA" && pinCodeIndex == 3)
                {
                    Menu(accountsSandra, accountNamesSandra, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "EMMA" && pinCodeIndex == 4)
                {
                    Menu(accountsEmma, accountNamesEmma, pinCode);
                    loggInAttempts = 0;
                }

                else 
                {
                    Console.WriteLine("Fel användarnamn eller pinkod");
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Du har skrivit in fel användarnamn eller pinkod tre gånger.\nTryck på enter för att avlsuta");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

        }        
            
        public static void Menu(List<decimal> accounts, List<string> accountNames, int pinCode)
        {
            // Printing a menu with functions and let user select a funktion

            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Vad vill du göra?\n\n1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Sätt in pengar\n5. Öppna nytt konto\n6. Logga ut");

                int menuOption = GetParsedInt();

                switch (menuOption)
                {
                    case 1:
                        PrintAcounts(accounts, accountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 2:
                        TransferringMoney(accounts, accountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 3:
                        WithdrawMoney(accounts, accountNames, pinCode);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 4:
                        DepositMoney(accounts, accountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }      
                        break;

                    case 5:
                        OpenNewAccount(accounts,accountNames);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 6:
                        runMenu = false; 
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        Console.ReadKey();
                        break;
                }
            }
        }   

        // Print a list of account names and the corresponding balance on each of them.

        public static void PrintAcounts(List<decimal> accounts, List<string> accountNames) 
        {
            Console.Clear();
            for (int i = 0; i < accounts.Count; i++) 
            {
                Console.WriteLine($"{accountNames[i]}: {accounts[i]} sek"); 
            }
        }

        // Let user select two accounts and an amount of money that is moved from the first account to the second 

        public static void TransferringMoney(List<decimal> accounts, List<string> accountNames) 
        {
            Console.Clear();
            // Print available accounts. Exclude account with no money 
            Console.WriteLine("Välj vilket konto du vill flytta pengar från.");
            for (int i = 0; i < accounts.Count; i++)
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
                accountNumberFrom = GetParsedInt();

                if (accountNumberFrom > accounts.Count || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberFrom > accounts.Count || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0);

            Console.Clear();

            // Print available accounts for deposit. Excluding account where money is tranfered from.
            Console.WriteLine("Välj vilket konto du vill flytta pengar till.");
            for (int i = 0; i < accounts.Count; i++)
            {
                if (i != accountNumberFrom - 1)
                {
                    Console.WriteLine($"{i + 1} - {accountNames[i]}");
                }
            }

            // Acceptable user input is only the number of the accounts printed above
            int accountNumberTo;
            do
            {
                accountNumberTo = GetParsedInt();

                if (accountNumberTo > accounts.Count || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberTo > accounts.Count || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom);

            Console.Clear();

            Console.WriteLine("Hur mycket vill du överföra?");

            decimal amountToTransfer;

            // Accepted user input to transfer is more than 0 and tops at current balance
            // of the account transferred from.
            do
            {
                amountToTransfer = GetParsedDecimal();

                if (amountToTransfer <= 0) 
                { 
                    Console.WriteLine("Ange ett belopp större än noll"); 
                }

                else if (amountToTransfer > accounts[accountNumberFrom - 1])
                {
                    Console.WriteLine("Övertrassering ej tillåtet");
                }

            } while (amountToTransfer <= 0 || amountToTransfer > accounts[accountNumberFrom - 1]);

            Console.Clear();

            // Transferring money and printing the current balance
            accounts[accountNumberFrom - 1] -= amountToTransfer; 
            accounts[accountNumberTo - 1] += amountToTransfer;

            Console.WriteLine($"{accountNames[accountNumberFrom - 1]}: {accounts[accountNumberFrom - 1]} sek");
            Console.WriteLine($"{accountNames[accountNumberTo - 1]}: {accounts[accountNumberTo - 1]} sek");
        }

        // Let user select an account and an amount that is then deducted from the account if user enters the pin code used to logg in.
        public static void WithdrawMoney(List<decimal> accounts, List<string> accountNames, int pinCode)
        {
            Console.Clear();
            Console.WriteLine("Välj vilket konto du vill ta ut pengar från");

            // Print available accounts that has a balance greater than 0.
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i] > 0)
                {
                    Console.WriteLine($"{i + 1} - {accountNames[i]}");
                }
            }

            //Let user select accounts from available accounts
            int selectedAccount;

            do
            {
                selectedAccount = GetParsedInt();

                if (selectedAccount < 1 || selectedAccount > accounts.Count || accounts[selectedAccount - 1] == 0) 
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (selectedAccount < 1 || selectedAccount > accounts.Count || accounts[selectedAccount - 1] == 0);

            Console.Clear();

            // Let user enter amount to withdraw. Amount has to be greater than 0 and les than current balance.
            Console.WriteLine("Vilket belopp vill du ta ut?");

            decimal amountToWithdraw;

            do
            {
                amountToWithdraw = GetParsedDecimal();

                if (amountToWithdraw <= 0)
                {
                    Console.WriteLine("Ange ett belopp större än noll");
                }

                else if (amountToWithdraw > accounts[selectedAccount - 1])
                {
                    Console.WriteLine("Övertrassering ej tillåten");
                }

            } while (amountToWithdraw <= 0 || amountToWithdraw > accounts[selectedAccount - 1]);

            Console.Clear();

            // User have to enter pin code
            Console.WriteLine("Bekräfta uttag med din pinkod");

            int confirmPinCode = GetParsedInt();
            
            // Withdraw money from selected account and print new balance
            if (confirmPinCode == pinCode)
            {
                Console.Clear();
                accounts[selectedAccount - 1] -= amountToWithdraw;

                Console.WriteLine($"{accountNames[selectedAccount - 1]}: {accounts[selectedAccount - 1]} sek");
            }
            else 
            { 
                Console.WriteLine("Felaktig pinkod! Avbryter uttag..."); 
            }
        }
        
        // Let user select an account and an amount than will be added to the account
        public static void DepositMoney(List<decimal> accounts, List<string> accountNames)
        {
            Console.Clear();
            Console.WriteLine("Välj vilket konto du vill sätta in pengar på");

            // Print available accounts.
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {accountNames[i]}");
            }

            // Let user select accounts from available accounts
            int selectedAccount;

            do
            {
                selectedAccount = GetParsedInt();

                if (selectedAccount < 1 || selectedAccount > accounts.Count)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (selectedAccount < 1 || selectedAccount > accounts.Count);

            Console.Clear();

            // Let user enter amount to deposit. Amount has to be greater than 0.
            Console.WriteLine("Vilket belopp vill du sätta in?");

            decimal amountToDeposit;

            do
            {
                amountToDeposit = GetParsedDecimal();

                if (amountToDeposit <= 0)
                {
                    Console.WriteLine("Ange ett belopp större än noll");
                }
            } while (amountToDeposit <= 0);

            Console.Clear();

            // Add money to the account and print out the balance.
            accounts[selectedAccount - 1] += amountToDeposit;

            Console.WriteLine($"{accountNames[selectedAccount - 1]}: {accounts[selectedAccount - 1]} sek");
        }

        // Add a new empty account to the list of the user and let user enter a name for the account.
        public static void OpenNewAccount(List<decimal> accounts, List<string> accountNames)
        {
            Console.Clear();
            Console.WriteLine("Vad vill du att ditt konto ska heta?");
            accountNames.Add(Console.ReadLine());
            accounts.Add(0);
            Console.WriteLine("Kontot skapas...");
            Thread.Sleep(1000);
            Console.WriteLine("Kontot skapat.");
        }

        // Converts user input to int. Prints a message if not successful and iterates until successful.
        public static int GetParsedInt()
        {
            int parsedInteger;
            while (!int.TryParse(Console.ReadLine(), out parsedInteger))
            {
                Console.WriteLine("Ogiltigt val");
            }
            return parsedInteger;
        }


        // Converts user input to decimal. Prints a message if not successful and iterates until successful.
        public static decimal GetParsedDecimal()
        {
            decimal parsedDecimal;
            while (!decimal.TryParse(Console.ReadLine(), out parsedDecimal))
            {
                Console.WriteLine("Ogiltigt val");
            }
            return parsedDecimal;
        }
    }
}