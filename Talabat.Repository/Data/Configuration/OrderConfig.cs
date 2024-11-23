using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.Order_Aggra;

namespace Talabat.Repository.Data.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.Status)
                          .HasConversion(Ostatues=>Ostatues.ToString(), Ostatues=>(OrderStatus) Enum.Parse(typeof(OrderStatus), Ostatues));

            builder.Property(o => o.SubTotal)
                   .HasColumnType("decimal(18,2)");
            
            builder.OwnsOne(o => o.ShippingAddress, x => x.WithOwner());
       
           
        
        }
    }
}
