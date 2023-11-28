using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Enums
{
    public enum UserStatus : byte
    {
        Active = 10,
        Deactive = 20,
        Blocked = 30
    }
}
