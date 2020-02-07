using System;
using System.IO;
using Capstone.Classes;
namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
            //When program is launched this stocks the Vending machine and creates a new vending machine.
            string inventoryFile = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/VendingMachine.txt";
            VendingMachine machineSub0 = new VendingMachine();
            using (StreamReader sr = new StreamReader(inventoryFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    machineSub0.GetInventory(line);
                }
            }
            //Console.WriteLine(machineSub0.ToString());

            //Now that we have created our vending machine, and stocked it, we are going to display our menu to the user
            Menu startMenu = new Menu(machineSub0);
            string purchaseChoice = " ";
            string userChoice = " ";
            while(userChoice != "3")
            {
                startMenu.DisplayStartMenu();
                userChoice = Console.ReadLine();
                if (userChoice == "1")
                {
                   startMenu.DisplayItems();
                }
                if(userChoice == "2")
                {
                    startMenu.DisplayPurchaseMenu();

                    purchaseChoice = Console.ReadLine();
                    startMenu.PurchaseChoice(purchaseChoice);
                    while (purchaseChoice != "3")
                    {
                        startMenu.DisplayPurchaseMenu();
                        purchaseChoice = Console.ReadLine();
                        startMenu.PurchaseChoice(purchaseChoice);
                    }

                }
                if(userChoice == "4")
                {
                    machineSub0.PrintSalesReport();
                    Console.WriteLine("Sales Report Generated!");
                }

            }

            //Here we take a deposit from the user if they press 2 to purchase then 1 to deposit.Deposit is added to the user's balance.
        
        }

       
    }
}
