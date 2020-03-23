using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CLIMenu : Menu
    {
        public CLIMenu (VendingMachine machine): base(machine)
        {

        }
        protected override string GetUserInput()
        {
            return Console.ReadLine();
        }

        protected override void DisplayOutput(string output)
        {
            Console.WriteLine(output);
        }

    }
}
