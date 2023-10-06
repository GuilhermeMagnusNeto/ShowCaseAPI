using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowCaseAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    [Table(name: "showcase_styles")]
    public class ShowcaseStyle : Entity
    {
        [Column("SHOWCASE_ID")]
        public string ShowcaseId { get; set; }
        public virtual Showcase Showcase { get; set; }

        #region STYLE
        [Column("TEMPLATE_ID")]
        public string TemplateId { get; set; }
        public virtual Template Template { get; set; }

        [Column("BACKGROUND_COLOR_CODE")]
        public string BackgroundColorCode { get; set; }

        [Column("SHOW_PRODUCT_VALUE")]
        public bool ShowProductValue { get; set; }

        [Column("SHOW_STORE_LOGO")]
        public bool ShowStoreLogo { get; set; }
        #endregion //STYLE
    }

    public static class ShowcaseStyleDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : ShowcaseStyle
        {
            self.HasKey(x => x.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.Property(x => x.TemplateId).IsRequired();
            self.Property(x => x.ShowProductValue).HasDefaultValue(true);
            self.Property(x => x.ShowStoreLogo).HasDefaultValue(true);

            return self;
        }
    }
}
