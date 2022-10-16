using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    [Index("Id", "Name")]
    public class Buyer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
