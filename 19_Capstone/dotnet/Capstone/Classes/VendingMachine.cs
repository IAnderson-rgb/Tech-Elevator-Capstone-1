using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        #region Private Members

        private readonly Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the balance
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets the balance formatted in dollars and cents
        /// </summary>
        public string BalanceAsString
        {
            get
            {
                return string.Format("{0:0.00}", Balance);
                //return String.Format("{0:C2}", Balance);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a VendingMachine
        /// </summary>
        public VendingMachine()
        {
            Balance = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string representation of the items in the VendingMachine.
        /// </summary>
        /// <returns>The string representation of the items in the VendingMachine</returns>
        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
            {
                s += $"{item.Key.ToString()} {item.Value.ToString()} \n";
            }
            return s;
        }

        /// <summary>
        /// Adds an inventory item to the VendingMachine
        /// </summary>
        /// <param name="inventoryLine">The inventory item information</param>
        /// <returns>True if the inventory item is successfully added, false otherwise</returns>
        public bool GetInventory(string inventoryLine)
        {
            string[] itemInfoArray = inventoryLine.Split("|");
            
            string itemId = itemInfoArray[0];
            string itemName = itemInfoArray[1];
            decimal itemPrice = Decimal.Parse(itemInfoArray[2]);
            string itemCategory = itemInfoArray[3];
            VendingMachineItem item = null;

            if (itemCategory == CandyVendingMachineItem.CandyCategory)
            {
                item = new CandyVendingMachineItem(itemName, itemPrice);
            }

            else if (itemCategory == ChipVendingMachineItem.ChipCategory)
            {
                item = new ChipVendingMachineItem(itemName, itemPrice);
            }

            else if (itemCategory == DrinkVendingMachineItem.DrinkCategory)
            {
                item = new DrinkVendingMachineItem(itemName, itemPrice);
            }

            else if (itemCategory == GumVendingMachineItem.GumCategory)
            {
                item = new GumVendingMachineItem(itemName, itemPrice);
            }

            if(item == null)
            {
                return false;
            }
            
            inventory.Add(itemId, item);
            return true;
        }

        /// <summary>
        /// Purchases the specified VendingMachineItem
        /// </summary>
        /// <param name="purchasedItem">The VendingMachineItem to purchase</param>
        /// <returns>The result of the purchase</returns>
        public string Purchase(VendingMachineItem purchasedItem)
        {
            string response;

            if(purchasedItem.Price <= Balance && purchasedItem.Quantity > 0)
            {
                Balance -= purchasedItem.Price;
                response = "\n" + purchasedItem.MakeNoise();
                response += "\n" + $"You have ${BalanceAsString} remaining.\n";
            }
            else if(purchasedItem.Quantity == 0)
            {
                response = "Sold Out!";
            }
            else 
            {
                string neededToDeposit = String.Format("{0:0.00}", purchasedItem.Price - Balance);
                response = $"You still need to deposit ${neededToDeposit}.";
            }

            purchasedItem.NumTimesSold++;
            purchasedItem.Quantity--;

            // TODO: add item ID to VendingMachineItem and get rid of this key search
            string key = "";               
            foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
            {
                if(item.Value == purchasedItem)
                {
                    key = item.Key;
                    break;
                }
            }

            Log($"{DateTime.Now} {purchasedItem.Name} {key} ${purchasedItem.Price} ${BalanceAsString}");

            return response;
        }

        /// <summary>
        /// Deposits the user's desired balance in the VendingMachine.
        /// </summary>
        /// <param name="depositAmount">The amount to be deposited</param>
        /// <returns>The result of the deposit</returns>
        public string Deposit(decimal depositAmount)
        {
            Balance += depositAmount;
            string depositAmountString = String.Format("{0:0.00}", depositAmount);
            string response = $"You've deposited ${depositAmountString}\nYou're current balance is ${BalanceAsString}";

            if (depositAmount > 0)
            {
                Log($"{DateTime.Now} FEED MONEY: ${depositAmountString} ${BalanceAsString} ");
            }

            return response;
        }

        /// <summary>
        /// Determines if the product with the specified ID exists and purchases the product if it does.
        /// </summary>
        /// <param name="itemId">The ID of the item to purchase</param>
        /// <returns>The results of the purchase</returns>
        public string SelectProduct(string itemId)
        {
            if (inventory.ContainsKey(itemId))
            {
                return Purchase(inventory[itemId]);
            }

            return "Please select a valid Product Location";
        }

        /// <summary>
        /// Gets the user's change.
        /// </summary>
        /// <returns>The results of the operation</returns>
        public string GetChange()
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
            }

            string response = $"Your change is: {numQuarters} Quarters, {numDimes} Dimes, {numNickels} Nickels, and {numPennies} Pennies.";
            decimal changeGiven = numQuarters * .25M + numDimes * .10M + numNickels * .05M + numPennies * .01M;
            string changeGivenString = String.Format("{0:0.00}", changeGiven);
            if (changeGiven > 0)
            {
                Log($"{DateTime.Now} GIVE CHANGE: ${changeGivenString} ${BalanceAsString}");
            }

            return response;
        }

        /// <summary>
        /// Writes the VendingMachine's sales report to disk
        /// </summary>
        public void PrintSalesReport()
        {
            // TODO: fix hard-coded path
            string salesReportPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/SalesReport.txt";
            using (StreamWriter sw = new StreamWriter(salesReportPath, false))
            {
                decimal totalSales = 0.0M;
                foreach (KeyValuePair<string, VendingMachineItem> item in inventory)
                {
                    sw.WriteLine((String.Format("{0,-20} | {1,-20}",item.Value.Name, item.Value.NumTimesSold)));
                    totalSales += item.Value.Price * item.Value.NumTimesSold;
                }
                sw.WriteLine($"**TOTAL SALES** ${totalSales}");
            }
        }

        #endregion

        #region Private Methods

        // Log messages to the VendingMachine's audit log
        private void Log(string message)
        {
            // TODO: fix hard-coded path
            string fullPath = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/Audit.txt";
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.WriteLine(message);
            }
        }

        #endregion
    }
}
