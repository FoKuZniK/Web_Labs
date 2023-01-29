using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrderForCreationDto
    {
        public double Cost { get; set; }
        public string Goods { get; set; }
        public long Date { get; set; }
    }
}
