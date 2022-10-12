using System;
using System.Collections;
using System.Collections.Generic;

namespace BankingHub {

    internal class Program {

        //Account Dictionary
        public static Dictionary<int, Dictionary<int, List<string>>> userAccounts = new Dictionary<int, Dictionary<int, List<string>>>();
        //Availble Account Pins
        public static int[] pinArray = new int[5] { 111, 222, 333, 444, 555 };


        private static void Main(string[] args) {

            //Creates the 5 initial users
            UserCreation();

            //Calls the login method
            LogIn();

        }


        private static void LogIn() {

            Console.WriteLine("Välkommen till Trönninge Bank!\n\nVänligen ange bank pin:");

            byte tries = 0;
            do {

                int inputPin = int.Parse(Console.ReadLine());

                //Checks if the dictionary contains the key
                if (userAccounts.ContainsKey(inputPin)) {
                    NavMenu(inputPin);
                    return;
                }

                if (!userAccounts.ContainsKey(inputPin)) {
                    Console.WriteLine("\nTyvärr inte ett korrekt pin, försök igen...");
                    tries++;
                }

            } while (tries < 3);

            Console.WriteLine("Tyvärr dina tre försök är upp...");

        }


        private static void NavMenu(int pin) {

            //Prints out the logged in account name
            Console.WriteLine($"\nLoggade in till kontot: {pin}");

            bool run = true;
            do {

                Console.WriteLine("\n1. Se dina konton och saldo\r\n2. Överföring mellan konton\r\n3. Ta ut pengar\r\n4. Logga ut");
                byte userChoice = byte.Parse(Console.ReadLine());

                switch (userChoice) {
                    default: //If not 1,2,3 or 4
                        Console.WriteLine("Ogiltigt val.");
                        break;

                    case 1: //See all accounts
                        UserShowcase(pin);
                        EnterBack();
                        break;

                    case 2: //Transfer funds between accounts
                        EnterBack();
                        break;

                    case 3: //Withdraw funds from account
                        WithdrawFunds(pin); //Pin for which account to log into
                        EnterBack();
                        break;

                    case 4: //Log out
                        Console.WriteLine($"\nLoggar ut ur {userAccounts[pin][0]}");
                        run = false;
                        LogIn();
                        break;
                }

            } while (run);

        }
        

        private static void WithdrawFunds(int pin) {

            UserShowcase(pin);

            Console.WriteLine("\nVälj konto att dra ifrån:");
            int accountChoice = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nFinns {userAccounts[pin][accountChoice][1]}kr på kontot: {userAccounts[pin][accountChoice][0]} \nHur mycket ska dras:");

            while (true) {

                float amount;
                if (!float.TryParse(Console.ReadLine(), out amount))
                    Console.WriteLine("\nInte ett korrekt nummer.");

                //If the given amount is bigger than the funds
                if (float.Parse(userAccounts[pin][accountChoice][1]) < amount) {
                    Console.WriteLine("Kontot har inte tillräckligt med pengar.");
                } 
                else {
                    //Parses the userAccounts[pin][accountChoice][1] string to int and subtracts amount from it and parse it back to string
                    userAccounts[pin][accountChoice][1] = (float.Parse(userAccounts[pin][accountChoice][1]) - amount).ToString();
                    Console.WriteLine($"\nTog ut {amount}kr från {userAccounts[pin][accountChoice][0]}\nFinns nu {userAccounts[pin][accountChoice][1]}kr kvar på kontot.");

                    break;
                }

            }

        }


        private static void UserShowcase(int pin) {

            Console.WriteLine($"\nDina nuvarande konton i {pin}:");

            //Gets all dictionaries currently in the given dictionary, displays key, name of the account and the balance on t
            foreach (var user in userAccounts[pin])
                Console.WriteLine($" Key: {user.Key}, Name: {user.Value[0]}, {user.Value[1]} ");

        }

        
        private static void EnterBack() {

            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn.");

            //If the user presses Enter, return from this method
            while (true) {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    return;
            }

        }


        private static void UserCreation() {

            //Key(Pin), { Key(num) { Account, Balance} }
            userAccounts.Add(pinArray[0], new Dictionary<int, List<string>> {
                {1, new List<string>() { "Lönekonto" , 1000.0f.ToString() } },
                {2, new List<string>() { "Sparkonto" , 1000.0f.ToString() } },
            });

            userAccounts.Add(pinArray[1], new Dictionary<int, List<string>> {
                {1, new List<string>() { "Lönekonto" , 2000.0f.ToString() } },
                {2, new List<string>() { "Sparkonto" , 2000.0f.ToString() } },
            });

            userAccounts.Add(pinArray[2], new Dictionary<int, List<string>> {
                {1, new List<string>() { "Lönekonto" , 3000.0f.ToString() } },
                {2, new List<string>() { "Sparkonto" , 3000.0f.ToString() } },
            });

            userAccounts.Add(pinArray[3], new Dictionary<int, List<string>> {
                {1, new List<string>() { "Lönekonto" , 4000.0f.ToString() } },
                {2, new List<string>() { "Sparkonto" , 4000.0f.ToString() } },
            });

            userAccounts.Add(pinArray[4], new Dictionary<int, List<string>> {
                {1, new List<string>() { "Lönekonto" , 5000.0f.ToString() } },
                {2, new List<string>() { "Sparkonto" , 5000.0f.ToString() } },
            });

        }


    }

}
