namespace Capstone.Classes
{
    public class DrinkVendingMachineItem : VendingMachineItem
    {
        #region Constant Members

        public const string DrinkCategory = "Drink";
        private const string Noise = "Glug Glug, Yum!";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a DrinkVendingMachineItem
        /// </summary>
        /// <param name="name">The item name</param>
        /// <param name="price">The item price</param>
        public DrinkVendingMachineItem(string name, decimal price) : base(name, price, DrinkCategory)
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
