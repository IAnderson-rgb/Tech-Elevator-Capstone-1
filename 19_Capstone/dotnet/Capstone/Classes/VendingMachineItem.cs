using System;

namespace Capstone.Classes
{
    public abstract class VendingMachineItem
    {
        #region Public Properties

        /// <summary>
        /// Gets the item name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the item price
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Gets the number of items sold
        /// </summary>
        public int NumTimesSold { get; set; }
        
        /// <summary>
        /// Gets the item category
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the item quantity
        /// </summary>
        public int Quantity { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a VendingMachineItem
        /// </summary>
        /// <param name="name">The item name</param>
        /// <param name="price">The item price</param>
        /// <param name="category">The item category</param>
        public VendingMachineItem(string name, decimal price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
            Quantity = 5;
            NumTimesSold = 0;
        }

        #endregion

        #region Public Methods

        //Displays SOLD OUT if the item is sold out.
        /// <summary>
        /// Gets the string representation of the item (Name, price). Returns "SOLD OUT" if there the item quantity is zero.
        /// </summary>
        /// <returns>The string representation of the item</returns>
        public override string ToString()
        {
            if (Quantity == 0)
            {
                return $"{Name.ToString()} **SOLD OUT**";
            }
            else
            {
                string pricesString = String.Format("{0:0.00}", Price);
                return $"{Name.ToString()} ${pricesString}";
            }
        }

        /// <summary>
        /// Gets a string specific to the VendingMachineIem type to display when the item is purchased.
        /// </summary>
        /// <returns>The Noise string</returns>
        public abstract string MakeNoise();

        #endregion

    }
}
