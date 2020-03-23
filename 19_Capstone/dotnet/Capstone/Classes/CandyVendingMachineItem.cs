namespace Capstone.Classes
{
    public class CandyVendingMachineItem : VendingMachineItem
    {
        #region Constant Members

        public const string CandyCategory = "Candy";
        private const string Noise = "Munch Munch, Yum!";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a CandyVendingMachineItem
        /// </summary>
        /// <param name="name">The item name</param>
        /// <param name="price">The item price</param>
        public CandyVendingMachineItem(string name, decimal price) : base(name, price, CandyCategory)
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
