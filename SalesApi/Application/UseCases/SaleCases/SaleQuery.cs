using Application.EntityDbContext;
using Application.Interfaces;
using Application.UseCases.SaleCases.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.SaleCases
{
    public class SaleQuery : IRequest<Unit>
    {
        public List<BuyedProduct> BuyedProduct { get; set; }
        public Guid SalesPointId { get; set; }

        private class Handler : IRequestHandler<SaleQuery, Unit>
        {
            private readonly IApiContext _context;
            private readonly ILogger<SaleQuery> _logger;
            private readonly IUserManagingService _userManagingService;
            private readonly ISalesService _salesService;

            public Handler(
                IApiContext context, 
                ILogger<SaleQuery> logger,
                IUserManagingService userManagingService,
                ISalesService salesService)
            {
                _context = context;
                _logger = logger;
                _userManagingService = userManagingService;
                _salesService = salesService;
            }

            public async Task<Unit> Handle(SaleQuery request,CancellationToken cancellationToken)
            {
                if (!request.BuyedProduct.Any())
                {
                    throw new Exception("Корзина пуста.");
                }

                var sale = new Sale
                {
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now,
                    SalesPoint = _context.SalesPoints.FirstOrDefault(s => s.Id == request.SalesPointId),
                    Buyer = _userManagingService.CurrentUser,
                };

                var salesData = _salesService.GetSaleData(request.BuyedProduct, sale);
                sale.TotalAmount = salesData.Select(s => s.ProductIdAmount).Sum();

                _context.Sales.Add(sale);
                _context.SalesData.AddRange(salesData);

                _context.SaveChanges();


                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
