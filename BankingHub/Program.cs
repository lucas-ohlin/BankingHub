using System;
using System.Collections.Generic;

namespace BankingHub {

    internal class Program {

        public static Dictionary<int, List<string>> userAccounts = new Dictionary<int, List<string>>();

        static void Main(string[] args) {

            //Creates the 5 initial users
            UserCreation();

            Console.WriteLine("Välkommen till Trönninge Bank!\n\nVänligen ange bank pin:");
            for (int i = 0; i < 3; i++) {
                int userPin = int.Parse(Console.ReadLine());

                if (userAccounts.ContainsKey(userPin))
                    NavMenu(userPin);
                else
                    Console.WriteLine("Tyvärr inte ett korrekt pin, försök igen...");
            }

            Console.WriteLine("Tyvärr dina tre försök är upp...");
        }

        private static void NavMenu(int userPin) {
            Console.WriteLine($"\nLoggade in till kontot: {userAccounts[userPin][0]}");
            Console.WriteLine("1. Se dina konton och saldo\r\n2. Överföring mellan konton\r\n3. Ta ut pengar\r\n4. Logga ut");
        }

        private static void UserCreation() {
            //Key(Pin), { Full Name, Balance }
            userAccounts.Add(123, new List<string>() { "Lucas Öhlin"    , "1000" });
            userAccounts.Add(222, new List<string>() { "Kalle Banan"    , "1" });
            userAccounts.Add(333, new List<string>() { "Bob Bobson"     , "200" });
            userAccounts.Add(444, new List<string>() { "Kungen"         , "0" });
            userAccounts.Add(555, new List<string>() { "Leffe Attans"   , "454" });
        }
    }
}
