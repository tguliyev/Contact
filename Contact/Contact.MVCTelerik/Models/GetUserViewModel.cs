using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.Domain.Enums;

namespace Contact.MVCTelerik.Models
{
    public class GetUserViewModel
    {
        public GetUserContactRequest Request {  get; set; }
        public GetUserContactResponse Response { get; set; }
    }
}
