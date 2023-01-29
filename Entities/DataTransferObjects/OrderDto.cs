using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Goods { get; set; }
        public string Date { get; set; }
    }
}
