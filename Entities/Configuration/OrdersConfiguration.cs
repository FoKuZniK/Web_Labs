using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
            (
            new Order
            {
                Id = new Guid("86abbca8-664d-4b20-b5de-024705497d4a"),
                Cost = 20.1,
                Goods = "Ручка",
                Date = 20012023,
                WarehouseId = new Guid("8615e23f-2548-4ef7-a440-af6edc214fb0")
            },
            new Order
            {
                Id = new Guid("87abbca8-664d-4b20-b5de-024705497d4a"),
                Cost = 203.1,
                Goods = "Карандаш",
                Date = 20012023,
                WarehouseId = new Guid("8615e23f-2548-4ef7-a440-af6edc214fb0")
            },
            new Order
            {
                Id = new Guid("88abbca8-664d-4b20-b5de-024705497d4a"),
                Cost = 210.1,
                Goods = "Линейка",
                Date = 20012023,
                WarehouseId = new Guid("713a847a-2875-469d-aefb-fd7bb283a8d4")
            });
        }
    }
}
