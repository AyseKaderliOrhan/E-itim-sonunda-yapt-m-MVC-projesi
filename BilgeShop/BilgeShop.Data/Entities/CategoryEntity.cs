using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeShop.Data.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Relational Propertry

        public List<ProductEntity> Product { get; set; }
    }
    public class CategoryEntityConfiguration : BaseConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.Description)
                .IsRequired(false);

            base.Configure(builder); // Modifieddate'i nullable yapan kısım.
        }
    }
}
