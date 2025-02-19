using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fullname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ProfilePhoto).HasMaxLength(1000);
            builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(320);
            builder.Property(x => x.Address).HasMaxLength(1000);
            builder.Property(x => x.Zipcode).HasMaxLength(10);


            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);

            builder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.ToTable("AccountPermissions");
                navigationBuilder.HasKey(x => x.Id);
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(x => x.Account);
            });
        }
    }
}
