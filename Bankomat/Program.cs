namespace Bankomat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            // Greet user at startup

            Console.WriteLine("Välkomen till Sparbanken!");

            while (isRunning)
            {
                // Logging in to account
                bool loggInSuccess;
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

                    loggInSuccess = LoggingIn(username, pinCode);

                    loggInAtempts++;

                }while (!loggInSuccess && loggInAtempts < 3);

                if (loggInSuccess)
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

                        while (!int.TryParse(Console.ReadLine(),out menuOption))
                        {
                            Console.WriteLine("Ogiltigt val");
                        }

                        switch (menuOption)
                        {
                            case 1:
                                // method see acounts and balance
                                Console.WriteLine("Klicka enter för att komma till huvudmenyn");
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                break;

                            case 2:
                                // method transfere between acounts
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
                else { isRunning = false; }
                
            }
        }

        // method for logging in

        public static bool LoggingIn(string username, int pinCode)
        {
            
            if ( (username == "DANIEL" && pinCode == 12345) || (username == "TOBIAS" && pinCode == 54321) || (username == "MARKUS" && pinCode == 67890) || (username == "SANDRA" && pinCode == 09876) || (username == "EMMA" && pinCode == 01234) )
            {
                return true;
            }

            else { return false; }

        }

        // method for printing user acounts and current balance

        // method for transfering money between acounts

        // method for withdrawing money
    }
}