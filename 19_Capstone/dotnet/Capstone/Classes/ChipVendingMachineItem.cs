namespace Capstone.Classes
{
    public class ChipVendingMachineItem : VendingMachineItem
    {
        #region Constant Members

        public const string ChipCategory = "Chip";
        private const string Noise = "Crunch Crunch, Yum!";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a ChipVendingMachineItem
        /// </summary>
        /// <param name="name">The item name</param>
        /// <param name="price">The item price</param>
        public ChipVendingMachineItem(string name, decimal price) : base(name, price, ChipCategory)
        {
        }

        #endregion

        #region Public Methods

        public override string MakeNoise()
        {
            return Noise;
        }

        #endregion
    }
}
