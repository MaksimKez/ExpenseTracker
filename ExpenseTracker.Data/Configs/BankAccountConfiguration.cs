using ExpenseTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configs;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccountEntity>
{
    public void Configure(EntityTypeBuilder<BankAccountEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Balance)
            .IsRequired();

        builder.HasMany(b => b.Expenses).WithOne()
            .HasForeignKey(e => e.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Incomes)
            .WithOne()
            .HasForeignKey(i => i.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
