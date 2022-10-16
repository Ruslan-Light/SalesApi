using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public Guid SalesPointId { get; set; }
        public SalesPoint SalesPoint { get; set; }

        public Guid? BuyerId { get; set; }
        public Buyer? Buyer { get; set; }

        public List<SaleData> SalesData { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
