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
    /// UserContact model configurations
    /// </summary>
    public class UserContactConfiguration : IEntityTypeConfiguration<UserContact>
    {
        public void Configure(EntityTypeBuilder<UserContact> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(150).IsRequired(required: false);
            builder.Property(x => x.Address).HasMaxLength(150).IsRequired(required: false);
        }
    }

}
