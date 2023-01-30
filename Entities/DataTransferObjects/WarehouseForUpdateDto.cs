using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WarehouseForUpdateDto
    {
        public string GoodName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public IEnumerable<OrderForCreationDto> Order { get; set; }
    }
}
