using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Order> GetAllOrder(bool trackChanges) =>
        FindAll(trackChanges)
        .OrderBy(c => c.Goods)
        .ToList();

        public IEnumerable<Order> GetOrders(Guid warehouseId, bool trackChanges) =>
        FindByCondition(c => c.WarehouseId.Equals(warehouseId), trackChanges)
        .OrderBy(c => c.Goods);

        public Order GetOrder(Guid warehouseId, Guid id, bool trackChanges) =>
            FindByCondition(c => c.WarehouseId.Equals(warehouseId) && c.Id.Equals(id), trackChanges).SingleOrDefault();
        
        public void CreateOrderForWarehouse(Guid warehouseId, Order order)
        {
            order.WarehouseId = warehouseId;
            Create(order);
        }
        public void DeleteOrder(Order order)
        {
            Delete(order);
        }
    }
}
