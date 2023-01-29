using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string GoodName { get; set; }
        public string Price { get; set; }
    }
}
