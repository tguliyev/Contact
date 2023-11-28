using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Enums
{
    public enum OTPStatus : byte
    {
        Active = 10,
        Expired = 20,
        Blocked = 30
    }
}
