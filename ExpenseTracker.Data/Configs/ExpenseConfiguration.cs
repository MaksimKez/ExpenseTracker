using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Configs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExpenseConfiguration : IEntityTypeConfiguration<ExpenseEntity>
{
    public void Configure(EntityTypeBuilder<ExpenseEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Sum)
            .IsRequired();

        builder.Property(e => e.ExpenseSource)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.HasOne<BankAccountEntity>()
            .WithMany(b => b.Expenses)
            .HasForeignKey(e => e.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
