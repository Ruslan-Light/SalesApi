using System;

namespace Domain.Models
{
    public class SaleData
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductQuantity { get; set; }

        public decimal ProductIdAmount { get; set; }

        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
