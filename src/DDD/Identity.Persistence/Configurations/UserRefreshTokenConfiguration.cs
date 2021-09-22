using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configurations
{
    public class UserRefreshTokenConfiguration:IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable(name: "UserRefreshToken");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}