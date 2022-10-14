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

            int inputPin;
            byte tries = 0;
            do {

                //Error handling, if it can't parse it as an int then cw
                if (!int.TryParse(Console.ReadLine(), out inputPin))
                    Console.WriteLine("Nummer endast.");

                //Checks if the dictionary contains the key
                if (userAccounts.ContainsKey(inputPin)) {
                    NavMenu(inputPin);
                    return;
                }
                if (!userAccounts.ContainsKey(inputPin)) {
                    Console.WriteLine("Tyvärr inte ett korrekt pin, försök igen...");
                    tries++;
                }

            } while (tries < 3);

            Console.WriteLine("\nTyvärr dina tre försök är upp...");

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
                        TransferFunds(pin);
                        EnterBack();
                        break;
                    case 3: //Withdraw funds from account
                        WithdrawFunds(pin); //Pin for which account to log into
                        EnterBack();
                        break;
                    case 4: //Log out
                        Console.WriteLine($"\nLoggar ut ur {pin}");
                        run = false;
                        LogIn();
                        break;
                }

            } while (run);

        }


        private static void TransferFunds(int pin) {

            //Displays all accounts in user
            UserShowcase(pin);
            Console.WriteLine("\nVälj konto att flytta pengar infrån och till vilket.");

            //Store the two inputs in a byte array
            byte[] accountChoice = new byte[2];
            for (int i = 0; i < 2; i++) {

                Console.WriteLine($"Konto nummer:");

                while (true) {

                    //Error handling, will not crash if char is input and stores number
                    if (!byte.TryParse(Console.ReadLine(), out accountChoice[i]))
                        Console.WriteLine("\nNummer endast.");

                    //If valid key, break the while loop
                    if (accountChoice[i] == 1 || accountChoice[i] == 2)
                        break;

                    else
                        Console.WriteLine("Inte en valid key.\nFörsök igen:");
                }

            }

            //Layout: users pin, which account in user and what part of the list to display
            Console.WriteLine($"\n{userAccounts[pin][accountChoice[0]][1]}kr i första kontot " +
                              $"| {userAccounts[pin][accountChoice[1]][1]}kr i andra kontot\nHur mycket ska flyttas över?:");

            while (true) {

                //Error handling, if it can't parse it as an int then cw
                float amount;
                if (!float.TryParse(Console.ReadLine(), out amount))
                    Console.WriteLine("\nNummer endast.");

                //If the given amount is bigger than the funds availible
                if (float.Parse(userAccounts[pin][accountChoice[0]][1]) < amount)
                    Console.WriteLine($"\nKontot har inte mer än {userAccounts[pin][accountChoice[0]][1]}kr\nFörsök igen:");

                else {

                    //Subracts the amount from the account we want to move the funds from
                    userAccounts[pin][accountChoice[0]][1] = (float.Parse(userAccounts[pin][accountChoice[0]][1]) - amount).ToString();

                    //Adds the amount subracted to the account we wanted to move the funds to
                    userAccounts[pin][accountChoice[1]][1] = (float.Parse(userAccounts[pin][accountChoice[1]][1]) + amount).ToString();

                    //Displays the new balances in the users accounts
                    UserShowcase(pin);

                    //Displays amount moved and to what account
                    Console.WriteLine($"Flyttade {amount}kr från {userAccounts[pin][accountChoice[0]][0]} till {userAccounts[pin][accountChoice[1]][0]}.");

                    return;
                }

            }

        }
        

        private static void WithdrawFunds(int pin) {

            //Displays all accounts in user
            UserShowcase(pin);
            Console.WriteLine("\nVälj konto att dra ifrån:");

            //Will continue to run until the user inputs either 1 or 2 (keys for the users accounts)
            byte accountChoice;
            while (true) {

                //Error handling, if it can't parse it as an int then cw
                if (!byte.TryParse(Console.ReadLine(), out accountChoice))
                    Console.WriteLine("\nNummer endast.");

                //If input is a valid key
                if (accountChoice == 1 || accountChoice == 2)
                    break;

                else
                    Console.WriteLine("Inte en valid key.\nFörsök igen:");
            }

            //Layout: users pin, which account in user and what part of the list to display
            Console.WriteLine($"\nFinns {userAccounts[pin][accountChoice][1]}kr på kontot: {userAccounts[pin][accountChoice][0]} \nHur mycket ska dras:");

            while (true) {

                //Error handling, if it can't parse it as an int then cw
                float amount;
                if (!float.TryParse(Console.ReadLine(), out amount))
                    Console.WriteLine("\nNummer endast.");

                //If the given amount is bigger than the funds availible
                if (float.Parse(userAccounts[pin][accountChoice][1]) < amount) 
                    Console.WriteLine($"\nKontot har inte mer än {userAccounts[pin][accountChoice][1]}kr\nFörsök igen:");

                else {

                    int inputPin;
                    byte tries = 0;

                    Console.WriteLine("\nAnge user pin för att dra ut pengar:");

                    do {
                        //Error handling
                        if (!int.TryParse(Console.ReadLine(), out inputPin)) {
                            Console.WriteLine("\nInte ett korrekt nummer.");
                        }
                        if (inputPin != pin) {
                            Console.WriteLine("Inte en valid key.\nFörsök igen:");
                            tries++;
                        }
                        if (inputPin == pin) {
                            //Parses the userAccounts[pin][accountChoice][1] (user bal)
                            //string to int and subtracts amount (user input) from it and parse it back to string
                            userAccounts[pin][accountChoice][1] = (float.Parse(userAccounts[pin][accountChoice][1]) - amount).ToString();
                            Console.WriteLine($"\nTog ut {amount}kr från {userAccounts[pin][accountChoice][0]}\n" +
                                              $"Finns nu {userAccounts[pin][accountChoice][1]}kr kvar på kontot.");
                            return;
                        }

                    } while (tries < 5);

                    //If the given five tries are up user is sent back to the nav menu.
                    if (tries == 5) {
                        Console.WriteLine("\nTyvärr dina fem försök är upp...");
                        return;
                    }

                }

            }

        }


        private static void UserShowcase(int pin) {
            Console.WriteLine($"\nDina nuvarande konton i {pin}:");

            //Gets all dictionaries currently in the user dictionary, displays key, name of the account and the balance
            foreach (var user in userAccounts[pin])
                Console.WriteLine($" Key: {user.Key}, Name: {user.Value[0]}, {user.Value[1]} ");
        }

        
        private static void EnterBack() {
            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn.");

            //If the user presses Enter, return from this method
            while (true) 
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    return;
        }


        private static void UserCreation() {

            //User(Pin(From pinArray)), { Account(Pin) { Name, Balance} }
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
