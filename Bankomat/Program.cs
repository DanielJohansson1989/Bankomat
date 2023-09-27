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
                    int pinCode = Convert.ToInt32(Console.ReadLine()); // Add a try catch later

                    loggInSuccess = LoggingIn(username, pinCode);

                    loggInAtempts++;

                }while (!loggInSuccess && loggInAtempts < 3);

                if (loggInSuccess)
                {
                    //Console.WriteLine("Logg in succeeded");
                    // Menu with funktions
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