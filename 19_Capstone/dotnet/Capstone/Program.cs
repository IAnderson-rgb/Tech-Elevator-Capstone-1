using System.IO;
using Capstone.Classes;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
            // When program is launched this stocks the Vending machine and creates a new vending machine.
            // TODO: fix hard coded path
            string inventoryFile = @"C:\Users\Student\workspace\week-4-pair-exercises-c-team-1\19_Capstone\dotnet\Example Files/VendingMachine.txt";
            VendingMachine machine = new VendingMachine();
            using (StreamReader sr = new StreamReader(inventoryFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    machine.GetInventory(line);
                }
            }

            //Now that we have created our vending machine, and stocked it, we are going to display our menu to the user
            Menu startMenu = new CLIMenu(machine);
            startMenu.Run();
        }
    }
}
