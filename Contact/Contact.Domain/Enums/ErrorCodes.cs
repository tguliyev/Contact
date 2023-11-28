using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Enums
{
    public enum ErrorCodes
    {

        [Description("Email or password is not correct")]
        EMAIL_OR_PASSWORD_IS_NOT_CORRECT = 1_0_0,

        [Description("There is a user with this email")]
        USER_IS_ALREADY_EXISTS_WITH_THIS_EMAIL = 1_0_1,

        [Description("User is not exists")]
        USER_IS_NOT_EXISTS = 1_0_2,


        [Description("User contact is not exists")]
        USER_CONTACT_IS_NOT_EXISTS = 2_0_0,

        [Description("Contact is already exists with this email")]
        USER_CONTACT_IS_ALREADY_EXISTS = 2_0_1,


        [Description("Auth token is empty")]
        AUTH_TOKEN_IS_EMPTY = 3_0_0





    }
}
