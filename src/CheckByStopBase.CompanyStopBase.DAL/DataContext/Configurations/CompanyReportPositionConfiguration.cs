using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckByStopBase.CompanyStopBase.DAL.DataContext.Configurations;

internal class CompanyReportPositionConfiguration : IEntityTypeConfiguration<CompanyReportPosition>
{
    public void Configure(EntityTypeBuilder<CompanyReportPosition> builder)
    {
        builder.ToTable("company_report_position");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.TaxNumber);

        builder.Property(c => c.CompanyType)
            .HasMaxLength(20)
            .HasColumnName("company_type")
            .HasConversion(
                v => v.ToString(),
                v => (CompanyTypeEnum)Enum.Parse(typeof(CompanyTypeEnum), v));

        builder.HasOne(c => c.Report)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.ReportId)
            .IsRequired();
    }
}