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
    public class WarehouseRepository : RepositoryBase<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<Warehouse> GetAllWarehouse(bool trackChanges) =>
        FindAll(trackChanges)
        .OrderBy(c => c.GoodName)
        .ToList();

        public Warehouse GetWarehouse(Guid warehouseId, bool trackChanges)
            => FindByCondition(c => c.Id.Equals(warehouseId), trackChanges).SingleOrDefault();
    }
}
