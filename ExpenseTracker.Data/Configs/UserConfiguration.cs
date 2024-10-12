using ExpenseTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Configs;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);

        builder.Property(u => u.BankAccountId)
            .IsRequired();

        builder.HasOne(u => u.BankAccountEntity).WithOne()
            .HasForeignKey<UserEntity>(u => u.BankAccountId).OnDelete(DeleteBehavior.Cascade);
    }
}