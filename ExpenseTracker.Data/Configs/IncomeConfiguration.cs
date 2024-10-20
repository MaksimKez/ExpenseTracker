using ExpenseTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configs;

public class IncomeConfiguration : IEntityTypeConfiguration<IncomeEntity>
{
    public void Configure(EntityTypeBuilder<IncomeEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Sum)
            .IsRequired();

        builder.Property(i => i.IncomeSource)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.HasOne<BankAccountEntity>()
            .WithMany(b => b.Incomes)
            .HasForeignKey(i => i.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
