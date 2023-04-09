using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckByStopBase.CompanyStopBase.DAL.DataContext.Configurations;

internal sealed class CompanyReportConfiguration : IEntityTypeConfiguration<CompanyReport>
{
    public void Configure(EntityTypeBuilder<CompanyReport> builder)
    {
        builder.ToTable("company_report");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Partner)
            .HasMaxLength(20)
            .HasColumnName("partner")
            .HasConversion(
                v => v.ToString(),
                v => (PartnerEnum)Enum.Parse(typeof(PartnerEnum), v));

        builder.Property(c => c.CreateDate)
            .HasColumnName("create_date");

        builder.HasMany(c => c.Companies)
            .WithOne(c => c.Report)
            .IsRequired();
    }
}