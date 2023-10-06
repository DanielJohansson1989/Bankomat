﻿namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] accountsDaniel = { 500, 2000 };
            decimal[] accountsTobias = { 100055.50m };
            decimal[] accountsMarkus = { 1531.19m, 0, 2525.0m };
            decimal[] accountsSandra = { 0, 50000, 460.99m, 23000 };
            decimal[] accountsEmma = { 777.7m, 0, 0, 50679.35m, 479549.50m };
            string[] accountNames = { "Lönekonto", "Sparkonto", "Matkonto", "Semesterkonto", "Pensionskonto" };

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

                loggInAttempts++;

                if (username == "DANIEL" && pinCode == 12345)
                {
                    Menu(accountsDaniel, accountNames, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "TOBIAS" && pinCode == 54321)
                {
                    Menu(accountsTobias, accountNames, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "MARKUS" && pinCode == 67890)
                {
                    Menu(accountsMarkus, accountNames, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "SANDRA" && pinCode == 09876)
                {
                    Menu(accountsSandra, accountNames, pinCode);
                    loggInAttempts = 0;
                }

                else if (username == "EMMA" && pinCode == 01234)
                {
                    Menu(accountsEmma, accountNames, pinCode);
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
            
        public static void Menu(decimal[] accounts, string[] accountNames, int pinCode)
        {
            // Printing a menu with functions and let user select a funktion

            bool runMenu = true;
            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("Vad vill du göra?\n\n1. Se dina konton och saldo\n2. Överföring mellan konton\n3. Ta ut pengar\n4. Sätt in pengar\n5. Logga ut");

                int menuOption = GetParsedInt();

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
                        WithdrawMoney(accounts, accountNames, pinCode);
                        Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        break;

                    case 4:
                        DepositMoney(accounts, accountNames);
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }      
                        break;

                    case 5:
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
            Console.Clear();
            for (int i = 0; i < accounts.Length; i++) 
            {
                Console.WriteLine($"{accountNames[i]}: {accounts[i]} sek"); 
            }
        }

        // method for transfering money between accounts

        public static void TransferringMoney(decimal[] accounts, string[] accountNames) 
        {
            Console.Clear();
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
                accountNumberFrom = GetParsedInt();

                if (accountNumberFrom > accounts.Length || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberFrom > accounts.Length || accountNumberFrom <= 0 || accounts[accountNumberFrom - 1] == 0);

            Console.Clear();

            // Print available accounts for deposit. Excluding account where money is tranfered from.
            Console.WriteLine("Välj vilket konto du vill flytta pengar till.");
            for (int i = 0; i < accounts.Length; i++)
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

                if (accountNumberTo > accounts.Length || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (accountNumberTo > accounts.Length || accountNumberTo <= 0 || accountNumberTo == accountNumberFrom);

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

        // method for withdrawing money
        public static void WithdrawMoney(decimal[] accounts, string[] accountNames, int pinCode)
        {
            Console.Clear();
            Console.WriteLine("Välj vilket konto du vill ta ut pengar från");

            // Print available accounts that has a balance greater than 0.
            for (int i = 0; i < accounts.Length; i++)
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

                if (selectedAccount < 1 || selectedAccount > accounts.Length || accounts[selectedAccount - 1] == 0) 
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (selectedAccount < 1 || selectedAccount > accounts.Length || accounts[selectedAccount - 1] == 0);

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
        
        public static void DepositMoney(decimal[] accounts, string[] accountNames)
        {
            Console.Clear();
            Console.WriteLine("Välj vilket konto du vill sätta in pengar på");

            // Print available accounts.
            for (int i = 0; i < accounts.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {accountNames[i]}");
            }

            // Let user select accounts from available accounts
            int selectedAccount;

            do
            {
                selectedAccount = GetParsedInt();

                if (selectedAccount < 1 || selectedAccount > accounts.Length)
                {
                    Console.WriteLine("Ogiltigt val");
                }

            } while (selectedAccount < 1 || selectedAccount > accounts.Length);

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