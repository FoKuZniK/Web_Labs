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
    public class WarehousesConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasData
            (
            new Warehouse
            {
                Id = new Guid("8615e23f-2548-4ef7-a440-af6edc214fb0"),
                GoodName = "Pen",
                Count = 4,
                Price = 13490.0
            },
            new Warehouse
            {
                Id = new Guid("713a847a-2875-469d-aefb-fd7bb283a8d4"),
                GoodName = "Stick",
                Count = 10,
                Price = 3790.0
            });
        }
    }
}
