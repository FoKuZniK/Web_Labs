using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrdersId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Order good is a required field.")]
        [MaxLength(300, ErrorMessage = "Maximum length for the Good is 300 characters.")]
        public string Goods { get; set; }
        [Required(ErrorMessage = "Cost is a required field.")]
        public double Cost { get; set; }
        [Required(ErrorMessage = "Date is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the date is 20 characters.")]
        public long Date { get; set; }

        [ForeignKey(nameof(Models.Warehouse))]
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
