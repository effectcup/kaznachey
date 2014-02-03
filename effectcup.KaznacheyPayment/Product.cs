using System;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Product info
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product items num
        /// </summary>
        public Decimal ProductItemsNum { get; set; }

        /// <summary>
        /// Product image url
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public Decimal ProductPrice { get; set; }

        /// <summary>
        /// Merchants cms product unique id
        /// </summary>
        public String ProductId { get; set; }
    }
}
