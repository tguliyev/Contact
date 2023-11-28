using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Data.Configurations
{
    /// <summary>
    /// UserOTP model configurations
    /// </summary>
    public class UserOTPConfiguration : IEntityTypeConfiguration<UserOTP>
    {
        public void Configure(EntityTypeBuilder<UserOTP> builder)
        {
            builder.Property(x => x.OTP).HasMaxLength(6).IsRequired();
            builder.Property(x => x.Expired).IsRequired();
            builder.Property(x => x.OTPStatusId).IsRequired();
            builder.Property(x => x.OTPTypeId).IsRequired();
        }
    }

}
