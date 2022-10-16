using Domain.Models;
using System;
using System.Linq;

namespace Infrastructure
{
    public static class Initialize
    {
        public static void Initializer(ApiContext dbContext)
        {
            if (!dbContext.Buyers.Any() && !dbContext.Products.Any() && !dbContext.SalesPoints.Any())
            {
                var buyer1 = new Buyer
                {
                    Name = "TestName_1"
                };

                var buyer2 = new Buyer
                {
                    Name = "TestName_2"
                };

                var buyer3 = new Buyer
                {
                    Name = "TestName_3"
                };

                dbContext.Buyers.AddRange(buyer1, buyer2, buyer3);

                var product1 = new Product
                {
                    Name = "TestProduct_1",
                    Price = 1234.44m
                };
                var product2 = new Product
                {
                    Name = "TestProduct_2",
                    Price = 345.58m
                };
                var product3 = new Product
                {
                    Name = "TestProduct_3",
                    Price = 6542.54m
                };
                var product4 = new Product
                {
                    Name = "TestProduct_4",
                    Price = 16389.12m
                };
                var product5 = new Product
                {
                    Name = "TestProduct_5",
                    Price = 145.87m
                };

                dbContext.Products.AddRange(product1, product2, product3, product4, product5);

                var salesPoint1 = new SalesPoint
                {
                    Name = "TestSalePoint_1"
                };
                var salesPoint2 = new SalesPoint
                {
                    Name = "TestSalePoint_2"
                };
                var salesPoint3 = new SalesPoint
                {
                    Name = "TestSalePoint_3"
                };

                dbContext.SalesPoints.AddRange(salesPoint1, salesPoint2, salesPoint3);

                var providedProduct1 = new ProvidedProduct
                {
                    ProductQuantity = 48,
                    Product = product1,
                    SalesPoint = salesPoint1
                };
                var providedProduct2 = new ProvidedProduct
                {
                    ProductQuantity = 93,
                    Product = product2,
                    SalesPoint = salesPoint2
                };
                var providedProduct3 = new ProvidedProduct
                {
                    ProductQuantity = 25,
                    Product = product3,
                    SalesPoint = salesPoint3
                };
                var providedProduct4 = new ProvidedProduct
                {
                    ProductQuantity = 88,
                    Product = product4,
                    SalesPoint = salesPoint1
                };
                var providedProduct5 = new ProvidedProduct
                {
                    ProductQuantity = 36,
                    Product = product5,
                    SalesPoint = salesPoint2
                };
                var providedProduct6 = new ProvidedProduct
                {
                    ProductQuantity = 39,
                    Product = product1,
                    SalesPoint = salesPoint3
                };
                var providedProduct7 = new ProvidedProduct
                {
                    ProductQuantity = 54,
                    Product = product2,
                    SalesPoint = salesPoint1
                };
                var providedProduct8 = new ProvidedProduct
                {
                    ProductQuantity = 81,
                    Product = product3,
                    SalesPoint = salesPoint2
                };
                var providedProduct9 = new ProvidedProduct
                {
                    ProductQuantity = 15,
                    Product = product4,
                    SalesPoint = salesPoint3
                };

                dbContext.ProvidedProducts.AddRange(
                    providedProduct1,
                    providedProduct2,
                    providedProduct3,
                    providedProduct4,
                    providedProduct5,
                    providedProduct6,
                    providedProduct7,
                    providedProduct8,
                    providedProduct9);

                var sale1 = new Sale
                {
                    Date = new DateTime(2022, 7, 20),
                    Time = new DateTime(2022, 7, 20, 12, 30, 25),
                    SalesPoint = salesPoint1,
                    Buyer = buyer1,
                    TotalAmount = 87235.5m
                };

                var sale2 = new Sale
                {
                    Date = new DateTime(2022, 8, 12),
                    Time = new DateTime(2022, 8, 12, 11, 28, 33),
                    SalesPoint = salesPoint2,
                    Buyer = buyer2,
                    TotalAmount = 63548.4m
                };

                dbContext.Sales.AddRange(sale1, sale2);

                var saleData1 = new SaleData
                {
                    Product = product1,
                    ProductQuantity = 15,
                    ProductIdAmount = 15242.3m,
                    Sale = sale1
                };

                var saleData2 = new SaleData
                {
                    Product = product2,
                    ProductQuantity = 22,
                    ProductIdAmount = 10243.2m,
                    Sale = sale1
                };

                var saleData3 = new SaleData
                {
                    Product = product3,
                    ProductQuantity = 33,
                    ProductIdAmount = 2213.1m,
                    Sale = sale2
                };

                var saleData4 = new SaleData
                {
                    Product = product3,
                    ProductQuantity = 33,
                    ProductIdAmount = 2213.1m,
                    Sale = sale2
                };

                dbContext.SalesData.AddRange(saleData1, saleData2, saleData3, saleData4);

                dbContext.SaveChanges();
            }
        }
    }
}
