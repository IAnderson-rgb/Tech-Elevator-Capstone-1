using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Menu
    {
        public VendingMachine Machine { get; private set; }
      
        public Menu(VendingMachine machine)
        {
            Machine = machine;
            
        }
        //Displays a list of options from the start menu.
        public void DisplayStartMenu()
        {
            string startDisplay = "1.) Display Items\n2.) Purchase Items\n3.) Exit";
            Console.WriteLine(startDisplay);
        }
        //Displays the menu for the user if they press 2 at the start menu.
        public void DisplayPurchaseMenu()
        {
            string purchaseDisplay = "1.) Feed Money\n2.) Select Product\n3.) Finish Transaction";
            Console.WriteLine(purchaseDisplay);

        }
        //Takes in the chaoice of the user in the purchase menu and exicutes the appropriate method.
        public void PurchaseChoice(string choice)
        {
            if(choice == "1")
            {   
                Machine.Deposit();
            }
            else if(choice == "2")
            {
                Machine.SelectProduct();
                
            }
            else if (choice == "3")
            {
                Machine.GetChange();
                
            }
        }
        //Displays the inventory items.
        public void DisplayItems()
        {
            Console.WriteLine(Machine.ToString());
        }
        
    }
}
