using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(Guid warehouseId, bool trackChanges);
        Order GetOrder(Guid warehouseId, Guid id, bool trackChanges);
    }
}
