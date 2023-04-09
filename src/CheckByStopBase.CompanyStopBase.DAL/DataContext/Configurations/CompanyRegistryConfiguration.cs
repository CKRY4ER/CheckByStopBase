using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckByStopBase.CompanyStopBase.DAL.DataContext.Configurations;

internal sealed class CompanyRegistryConfiguration : IEntityTypeConfiguration<CompanyRegistry>
{
    public void Configure(EntityTypeBuilder<CompanyRegistry> builder)
    {
        builder.ToTable("company_registry");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.TaxNumber);

        builder.Property(r => r.CompanyType)
            .HasMaxLength(20)
            .HasColumnName("company_type")
            .HasConversion(
                v => v.ToString(),
                v => (CompanyTypeEnum)Enum.Parse(typeof(CompanyTypeEnum), v));

        builder.Property(r => r.TaxNumber)
            .HasMaxLength(15)
            .HasColumnName("tax_number");

        builder.Property(r => r.AddDate)
            .HasColumnName("add_date");

        builder.Property(r => r.CreateDate)
            .HasColumnName("create_date");
    }
}