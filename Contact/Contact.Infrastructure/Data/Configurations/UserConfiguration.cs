using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Data.Configurations
{
    /// <summary>
    /// User model configurations
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(32).IsRequired();
            builder.Property(x => x.Salt).HasMaxLength(32).IsRequired();
            builder.Property(x => x.UserStatusId).IsRequired();
        }
    }
}
