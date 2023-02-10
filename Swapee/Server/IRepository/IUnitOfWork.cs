using Swapee.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swapee.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Buyer> Buyers { get; }
        IGenericRepository<Seller> Sellers { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
    }
}