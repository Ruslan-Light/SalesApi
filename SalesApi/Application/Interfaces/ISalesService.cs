using Application.UseCases.SaleCases.ViewModels;
using Domain.Models;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ISalesService
    {
        public List<SaleData> GetSaleData(List<BuyedProduct> BuyedProduct, Sale sale);
    }
}
