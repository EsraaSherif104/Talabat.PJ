﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                        .WithMany()
                        .HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p => p.productType)
                    .WithMany()
                    .HasForeignKey(p => p.productTypeId);

            builder.Property(p => p.Name).IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p=>p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2");
        
        }
    }

}