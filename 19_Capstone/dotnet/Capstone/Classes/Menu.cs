using System;
using System.Collections.Generic;

namespace Capstone.Classes
{
    public abstract class Menu
    {
        #region Support Classes

        private class MenuItem
        {
            /// <summary>
            /// Gets the ID
            /// </summary>
            public string Id { get; }

            /// <summary>
            /// Gets the Display Text
            /// </summary>
            public string DisplayText { get; }

            /// <summary>
            /// Gets a flag indicating whether or not the menu item should be hidden during menu display 
            /// </summary>
            public bool Hidden { get; }

            /// <summary>
            /// Constructs a MenuItem
            /// </summary>
            /// <param name="id">The ID</param>
            /// <param name="displayText">The Display Text</param>
            /// <param name="hidden">A flag indicating whether or not the menu item should be hidden during menu display</param>
            public MenuItem(string id, string displayText, bool hidden = false)
            {
                Id = id;
                DisplayText = displayText;
                Hidden = hidden;
            }

            /// <summary>
            /// Gets the string representation of the MenuItem
            /// </summary>
            /// <returns>The string representation</returns>
            public override string ToString()
            {
                return DisplayText;
            }
        }

        #endregion

        #region Private Members

        private readonly MenuItem displayItemsMenuItem = new MenuItem("1", "Display items");
        private readonly MenuItem purchaseItemsMenuItem = new MenuItem("2", "Purchase items");
        private readonly MenuItem exitMenuItem = new MenuItem("3", "Exit");
        private readonly MenuItem printSalesReportMenuItem = new MenuItem("4", "Print Sales Report", true);

        private readonly List<MenuItem> startMenu;

        private readonly MenuItem feedMoneyMenuItem = new MenuItem("1", "Feed Money");
        private readonly MenuItem selectProductMenuItem = new MenuItem("2", "Select Product");
        private readonly MenuItem finishTransactionMenuItem = new MenuItem("3", "Finish Transaction");

        private readonly List<MenuItem> purchaseMenu;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Vending Maching
        /// </summary>
        public VendingMachine Machine { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a Menu
        /// </summary>
        /// <param name="machine">The VendingMachine</param>
        public Menu(VendingMachine machine)
        {
            Machine = machine;
            startMenu = new List<MenuItem> { displayItemsMenuItem, purchaseItemsMenuItem, exitMenuItem, printSalesReportMenuItem };
            purchaseMenu = new List<MenuItem> { feedMoneyMenuItem, selectProductMenuItem, finishTransactionMenuItem };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs the menu until the user exits.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                DisplayMenu(startMenu);
                string userChoice = GetUserInput();
                if (userChoice == displayItemsMenuItem.Id)
                {
                    DisplayItems();
                }
                else if (userChoice == purchaseItemsMenuItem.Id)
                {

                    while (true)
                    {
                        DisplayMenu(purchaseMenu);
                        string purchaseChoice = Console.ReadLine();
                        PurchaseChoice(purchaseChoice);
                        if (purchaseChoice == exitMenuItem.Id)
                        {
                            return;
                        }
                    }
                }
                else if(userChoice == exitMenuItem.Id)
                {
                    return;
                }
                else if (userChoice == printSalesReportMenuItem.Id)
                {
                    Machine.PrintSalesReport();
                    DisplayOutput("Sales Report Generated!");
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the user input
        /// </summary>
        /// <returns>The user input</returns>
        protected abstract string GetUserInput();

        /// <summary>
        /// Displays output to the user
        /// </summary>
        /// <param name="output">The output to display</param>
        protected abstract void DisplayOutput(string output);

        #endregion

        #region Private Methods

        //Displays a list of options from the start menu.
        private void DisplayMenu(IEnumerable<MenuItem> menu)
        {
            foreach(MenuItem item in menu)
            {
                if (!item.Hidden)
                {
                    string menuText = $"{item.Id}.) {item}";
                    DisplayOutput(menuText);
                }
            }
        }
       
        //Takes in the choice of the user in the purchase menu and executes the appropriate method.
        private void PurchaseChoice(string choice)
        {
            if(choice == feedMoneyMenuItem.Id)
            {
                DisplayOutput("How much would you like to deposit?");
                string userAnswer = GetUserInput();
                decimal depositAmount;
                bool parseWorked = decimal.TryParse(userAnswer, out depositAmount);

                while (!parseWorked || depositAmount < 0)
                {
                    DisplayOutput("Please enter a valid deposit amount");
                    userAnswer = GetUserInput();
                    parseWorked = decimal.TryParse(userAnswer, out depositAmount);
                }

                string result = Machine.Deposit(depositAmount);
                DisplayOutput(result);
            }
            else if(choice == selectProductMenuItem.Id)
            {
                DisplayOutput(Machine.ToString());
                DisplayOutput("Please enter the Product Location");

                string itemId = GetUserInput().ToUpper();
                string result = Machine.SelectProduct(itemId);
                DisplayOutput(result);
            }
            else if (choice == finishTransactionMenuItem.Id)
            {
                string result = Machine.GetChange();
                DisplayOutput(result);
            }
        }

        //Displays the inventory items.
        private void DisplayItems()
        {
            DisplayOutput(Machine.ToString());
        }

        #endregion

    }
}
