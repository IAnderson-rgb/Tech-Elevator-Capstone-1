using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public int NumTimesSold { get; set; }
        
        public string Category { get; private set; }
        public int Quantity { get; set; }
        public VendingMachineItem(string name, decimal price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
            Quantity = 5;
            NumTimesSold = 0;
        }
        //Displays SOLD OUT if the item is sold out.
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

    }
}
