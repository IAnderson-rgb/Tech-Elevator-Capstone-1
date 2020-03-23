namespace Capstone.Classes
{
    public class GumVendingMachineItem : VendingMachineItem
    {
        #region Constant Members

        public const string GumCategory = "Gum";
        private const string Noise = "Chew Chew, Yum!";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a GumVendingMachineItem
        /// </summary>
        /// <param name="name">The item name</param>
        /// <param name="price">The item price</param>
        public GumVendingMachineItem(string name, decimal price) : base(name, price, GumCategory)
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
