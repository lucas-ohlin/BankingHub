using System;
using System.Collections.Generic;

namespace BankingHub {

    internal class Program {

        //Dictionary with the accounts
        public static Dictionary<int, List<string>> userAccounts = new Dictionary<int, List<string>>();
        //Pins to use by the accounts and the fund dictionary
        public static int[] pinArray = new int[5] { 123, 222, 333, 444, 555 };
        //Dictionary with funds linked to accounts using pinArray
        public static Dictionary<int, int> fundsDict = new Dictionary<int, int>();


        static void Main(string[] args) {
            //Creates the 5 initial users
            UserCreation();
               
            Console.WriteLine("Välkommen till Trönninge Bank!\n\nVänligen ange bank pin:");

            byte tries = 0;
            do {
                int userPin = int.Parse(Console.ReadLine());

                //Checks if the dictionary contains the key
                if (userAccounts.ContainsKey(userPin)) {
                    NavMenu(userPin);
                    return;
                }
                if (!userAccounts.ContainsKey(userPin)) {
                    Console.WriteLine("Tyvärr inte ett korrekt pin, försök igen...");
                    tries++;
                }

            } while (tries < 3);

            Console.WriteLine("Tyvärr dina tre försök är upp...");
        }


        private static void NavMenu(int pin) {
            //userAccounts[pin][0] = name
            Console.WriteLine($"\nLoggade in till kontot: {userAccounts[pin][0]}");

            bool run = true;
            while (run) {
                //Prints out the choices
                Console.WriteLine("\n1. Se dina konton och saldo\r\n2. Överföring mellan konton\r\n3. Ta ut pengar\r\n4. Logga ut");
                byte userChoice = byte.Parse(Console.ReadLine());

                switch (userChoice) {
                    //See all accounts
                    case 1:
                        Console.WriteLine("\nAlla nuvarande konton:");
                        UserShowcase();
                        break;
                    //Transfer funds between accounts
                    case 2:
                        break;
                    //Withdraw funds from account
                    case 3:
                        //pin is logged into account
                        WithdrawFunds(pin);
                        break;
                    //Log out
                    case 4:
                        Console.WriteLine("\nLoggar ut...");
                        return;
                }
            }

        }
        

        private static void WithdrawFunds(int pin) {
            //userAccounts[pin][0] = name | userAccounts[pin][1] = bal
            Console.WriteLine($"\nFinns {userAccounts[pin][1]}kr på kontot: {userAccounts[pin][0]}\nHur mycket ska dras:");

            while (true) {
                int amount = int.Parse(Console.ReadLine());

                //If the given amount is bigger than the funds
                if (fundsDict[pin] < amount) {
                    Console.WriteLine("Kontot har inte tillräckligt med pengar.");
                } 
                else {
                    //Parses the userAccounts[pin][1] string to int and subtracts amount from it and parse it back to string
                    userAccounts[pin][1] = (int.Parse(userAccounts[pin][1]) - amount).ToString();
                    Console.WriteLine($"Tog ut {amount}kr från {userAccounts[pin][0]}\nFinns nu {userAccounts[pin][1]}kr kvar på kontot.");

                    return;
                }
            }

        }


        private static void UserShowcase() {
            //Gets all users in userAccounts dictionary, displays key, name and balance
            foreach (var user in userAccounts)
                Console.WriteLine($" Key: {user.Key}, Name: {user.Value[0]}, Balance: {int.Parse(user.Value[1])}");
        }


        private static void UserCreation() {
            //Default funds in each pin
            fundsDict.Add(pinArray[0], 1000);
            fundsDict.Add(pinArray[1], 2000);
            fundsDict.Add(pinArray[2], 3000);
            fundsDict.Add(pinArray[3], 100);
            fundsDict.Add(pinArray[4], 10);

            //Key(Pin), { Full Name, Balance from funds dict }
            userAccounts.Add(pinArray[0], new List<string>() { "Lucas Öhlin"    , fundsDict[pinArray[0]].ToString() });
            userAccounts.Add(pinArray[1], new List<string>() { "Kalle Banan"    , fundsDict[pinArray[1]].ToString() });
            userAccounts.Add(pinArray[2], new List<string>() { "Bob Bobson"     , fundsDict[pinArray[2]].ToString() });
            userAccounts.Add(pinArray[3], new List<string>() { "Kungen"         , fundsDict[pinArray[3]].ToString() });
            userAccounts.Add(pinArray[4], new List<string>() { "Leffe Attans"   , fundsDict[pinArray[4]].ToString() });
        }


    }


}
