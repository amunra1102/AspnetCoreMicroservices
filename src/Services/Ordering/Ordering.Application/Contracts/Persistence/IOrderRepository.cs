using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Application.Behaviours.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
