using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public decimal Balance { get; set; }
        public string BalanceAsString
        {
            get
            {
                return string.Format("{0:0.00}", Balance);
                //return String.Format("{0:C2}", Balance);
            }
        }

        Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();

        public VendingMachine()
        {
            Balance = 0;
        }
        //This organizes the inventory.
        public void GetInventory(string s)
        {
            string[] itemInfoArray = s.Split("|");
            decimal price = Decimal.Parse(itemInfoArray[2]);
            VendingMachineItem vmi = new VendingMachineItem(itemInfoArray[1], price, itemInfoArray[3]);
            inventory.Add(itemInfoArray[0], vmi);
        }

        //This displays the items in Vending Machine.
        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
            {
               
                s += $"{item.Key.ToString()} {item.Value.ToString()} \n";
                
            }
            return s;

        }
        //Purchases Vending Machine Item
        public void Purchase(VendingMachineItem purchase)
        {
            if(purchase.Price <= Balance && purchase.Quantity>0)
            {
                Balance -= purchase.Price;
                
                if (purchase.Category == "Chip")
                {
                    Console.WriteLine("Crunch, Crunch, Yum!");
                }
                if (purchase.Category == "Candy")
                {
                    Console.WriteLine();
                    Console.WriteLine("Munch, Munch, Yum!");
                }
                if (purchase.Category == "Drink")
                {
                    Console.WriteLine();
                    Console.WriteLine("Glug, Glug, Yum!");
                }
                if (purchase.Category == "Gum")
                {
                    Console.WriteLine();
                    Console.WriteLine("Chew, Chew, Yum!");
                }
                Console.WriteLine($"You have ${BalanceAsString} remaing.");
                Console.WriteLine();
            }
            else if(purchase.Quantity == 0)
            {
                Console.WriteLine("Sold Out!");
            }
            else 
            {
                string neededToDeposit = String.Format("{0:0.00}", purchase.Price - Balance);
                Console.WriteLine($"You still need to deposit ${neededToDeposit}.");
            }

            purchase.NumTimesSold = purchase.NumTimesSold + 1;
            purchase.Quantity = purchase.Quantity - 1;

            string fullPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/Audit.txt";
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                string keyValue = " ";
               
                foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
                {
                    if(item.Value == purchase)
                    {
                        keyValue = item.Key;
                    }
                }
                sw.WriteLine($"{DateTime.Now} {purchase.Name} {keyValue} ${purchase.Price} ${BalanceAsString}");
                
                



            }
        }

        //Deposits the users desired balance in the vending machine.
        public void Deposit()
        {
            Console.WriteLine("How much would you like to deposit?");
            decimal depositAmount = 0;
            string userAnswer = Console.ReadLine();
            bool parseWorked = decimal.TryParse(userAnswer, out depositAmount);

            while (!parseWorked || depositAmount < 0)
            {
                Console.WriteLine("Please enter a valid deposit amount");
                userAnswer = Console.ReadLine();
                parseWorked = decimal.TryParse(userAnswer, out depositAmount);
            }
            
               
            Balance += depositAmount;
            string depositAmountString = String.Format("{0:0.00}", depositAmount);
            Console.WriteLine("You've deposited $" + depositAmountString);
            Console.WriteLine("You're current balance is $" + BalanceAsString);
            string fullPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/Audit.txt";
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                if (depositAmount > 0)
                {
                    sw.WriteLine($"{DateTime.Now} FEED MONEY: ${depositAmountString} ${BalanceAsString} ");
                }

            }
        }

        //Checks to make sure product location exists, and purchases product if it does
        public void SelectProduct()
        {
            
            foreach(KeyValuePair<string, VendingMachineItem> item in inventory)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Please enter the Product Location");

            string itemNumber = Console.ReadLine().ToUpper();

            if (inventory.ContainsKey(itemNumber))
            {
                Purchase(inventory[itemNumber]);
                //Console.WriteLine("Purchase successful");
            }
            else
            {
                Console.WriteLine("Please select a valid Product Location");
            }
        }

        public void GetChange()
        {
            int numQuarters = 0;
            int numDimes = 0;
            int numNickels = 0;
            int numPennies = 0;
           
            while(Balance >= .25M)
            {
                numQuarters++;
                Balance = Balance - .25M;
            }
            while(Balance >= .10M)
            {
                numDimes++;
                Balance = Balance - .10M;
            }
            while(Balance >= .05M)
            {
                numNickels++;
                Balance = Balance - .05M;
            }
            while (Balance > .00M)
            {
                numPennies++;
                Balance = Balance - .01M;
                //Balance = 0;
            }
            /*Change given keeps giving an extra penny to the user if the user selects an item then feeds money again and selects another item. 
            when the user finishes transaction.*/

            Console.WriteLine($"Your change is: {numQuarters} Quarters, {numDimes} Dimes, {numNickels} Nickels, and {numPennies} Pennies.");
            decimal changeGiven = numQuarters * .25M + numDimes * .10M + numNickels * .05M + numPennies * .01M;
            string changeGivenString = String.Format("{0:0.00}", changeGiven);
            string fullPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/Audit.txt";
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                if (changeGiven > 0)
                {
                    sw.WriteLine($"{DateTime.Now} GIVE CHANGE: ${changeGivenString} ${BalanceAsString}");
                }


                }
            }

        public void PrintSalesReport()
        {
            decimal totalSales = 0.0M;
            string salesReportPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/SalesReport.txt";
            using (StreamWriter sw = new StreamWriter(salesReportPath, false))
            {
                foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
                {
                    sw.WriteLine((String.Format("{0,-20} | {1,-20}",item.Value.Name, item.Value.NumTimesSold)));
                    totalSales += item.Value.Price * item.Value.NumTimesSold;
                }
                sw.WriteLine($"**TOTAL SALES** ${totalSales}");
            }
        }


        /* public void PrintTransactionHist()
         {
             using (StreamWriter sw = new StreamWriter(fullPath, true))
             {
                 // Prints the current datetime
                 sw.WriteLine(DateTime.UtcNow);


                 sw.Write("Hello ");
                 sw.Write("World!");


                 sw.WriteLine();


                 sw.WriteLine("Tech");
                 sw.WriteLine("Elevator");
             }
         } */
    }

}
