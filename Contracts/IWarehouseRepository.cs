using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWarehouseRepository
    {
        IEnumerable<Warehouse> GetAllWarehouse(bool trackChanges);
        IEnumerable<Warehouse> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Warehouse GetWarehouse(Guid warehouseId, bool trackChanges);
        void CreateWarehouse(Warehouse warehouse);
        void DeleteWarehouse(Warehouse warehouse);
    }
}
