using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Entities
{
    public class UserOTP:Entity
    {
        public string OTP { get; set; }
       
        public DateTime Expired { get; set; }

        public byte OTPStatusId { get; set; }

        public byte OTPTypeId { get; set; }
    }
}
