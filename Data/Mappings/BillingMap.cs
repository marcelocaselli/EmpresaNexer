using EmpresaNexer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpresaNexer.Data.Mappings
{
    public class BillingMap : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.ToTable("Billing");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.DataVencimento)
                .IsRequired()
                .HasColumnName("DataVencimento")
                .HasColumnType("datetime");

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Billings)
                .HasConstraintName("FK_Billing_Customer")
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}