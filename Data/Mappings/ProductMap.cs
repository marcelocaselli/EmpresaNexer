using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmpresaNexer.Models;

namespace EmpresaNexer.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.NameProduct)
                .IsRequired()
                .HasColumnName("NameProduct")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(180);

            builder
            .HasMany(x => x.BillingLines)
            .WithOne(x => x.Product)
            .HasConstraintName("FK_Product_BillingLine")
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}