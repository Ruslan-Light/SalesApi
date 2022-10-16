using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EntityDbContext
{
    public interface IApiContext
    {
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleData> SalesData { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
