using Contact.Application.Models.Request;
using Contact.Application.Models.Response;

namespace Contact.MVCTelerik.Models
{
    public class ContactViewModel
    {
        public GetUserContactDetailByIdResponse Response { get; set; }
        public UpdateUserContactRequest UpdateRequest { get; set; }
        public CreateUserContactRequest CreateRequest { get; set; }
    }
}
