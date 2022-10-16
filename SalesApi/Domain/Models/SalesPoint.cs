using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    [Index("Id", "Name")]
    public class SalesPoint
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }

        public Sale Sale { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; } = new List<ProvidedProduct>();
    }
}
