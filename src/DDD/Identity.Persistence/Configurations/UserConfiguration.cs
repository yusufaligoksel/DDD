using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "User");

            builder.HasKey(x => x.Id);
            
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(e => e.Firstname)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(e => e.Lastname)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(250);
            
            builder.Property(e => e.OldPasswordHash)
                .HasMaxLength(250);
            
            builder.Property(e => e.IncorrectLoginCount)
                .IsRequired();
            
            builder.Property(e => e.BirthDay)
                .HasColumnType("date");
            
            builder.Property(e => e.CreatedDate)
                .HasColumnType("date");
            
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("date");
            
            builder.Property(e => e.UserStatus)
                .IsRequired()
                .HasColumnType("bigint");
            
            builder.Property(e => e.LoginStatus)
                .IsRequired()
                .HasColumnType("bigint");
            
        }
    }
}
