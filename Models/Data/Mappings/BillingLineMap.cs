using EmpresaNexer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpresaNexer.Data.Mappings
{
    public class BillingLineMap : IEntityTypeConfiguration<BillingLine>
    {
        public void Configure(EntityTypeBuilder<BillingLine> builder)
        {
            builder.ToTable("BillingLine");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder
                .HasOne(x => x.Product)
                .WithOne(x => x.BillingLine);
        }
    }
}