using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Enums
{
    public enum OTPType:byte
    {
        Register = 10,
        ForgotPassword = 20
    }
}
