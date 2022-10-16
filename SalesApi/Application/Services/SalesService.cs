using Application.EntityDbContext;
using Application.Interfaces;
using Application.UseCases.SaleCases.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly IApiContext _context;

        public SalesService(IApiContext context)
        {
            _context = context;
        }

        public List<SaleData> GetSaleData(List<BuyedProduct> buyedProducts, Sale sale)
        {
            var salesData = new List<SaleData>();

            var products = _context.Products
                    .Include(p => p.ProvidedProduct)
                    .Where(p => buyedProducts.Select(b => b.Id).Any(x => p.Id == x));

            if (!products.Any())
            {
                throw new Exception("Требуемый товар не найден.");
            }

            foreach (var product in products)
            {
                var quantity = buyedProducts?.FirstOrDefault(b => b.Id == product.Id
                        && b.ProductQuantity <= product.ProvidedProduct.ProductQuantity)
                    ?.ProductQuantity ?? throw new Exception($"Недостаточно товара в наличии: {product.Name}");

                var saleData = new SaleData
                {
                    Product = product,
                    ProductQuantity = quantity,
                    ProductIdAmount = product.Price * quantity,
                    Sale = sale
                };

                product.ProvidedProduct.ProductQuantity = product.ProvidedProduct.ProductQuantity - quantity;
                salesData.Add(saleData);
            }

            _context.SaveChanges();

            return salesData;
        }
    }
}
