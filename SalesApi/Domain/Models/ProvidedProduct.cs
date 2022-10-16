using System;

namespace Domain.Models
{
    public class ProvidedProduct
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductQuantity { get; set; }

        public Guid SalesPointId { get; set; }

        public SalesPoint SalesPoint { get; set; }
    }
}
