using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    [Table(name: "store_products")]
    public class StoreProduct : Entity
    {
        [Column("STORE_ID")]
        public string StoreId { get; set; }
        public virtual Store Store { get; set; }

        [Column("PRODUCT_ID")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public static class StoreProductDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : StoreProduct
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.StoreId).IsRequired();
            self.Property(x => x.ProductId).IsRequired();

            return self;
        }
    }
}
