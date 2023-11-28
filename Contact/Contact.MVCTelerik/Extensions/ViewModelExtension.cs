using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.MVCTelerik.Models;

namespace Contact.MVCTelerik.Extensions
{
    public static class ViewModelExtensions
    {
        public static UpdateUserContactRequest AsUpdateUserContactRequest(this GetUserContactDetailByIdResponse response)
        {
            return new UpdateUserContactRequest
            {
                Name = response.Contact.Name,
                Surname = response.Contact.Surname,
                Email = response.Contact.Email,
                Phone = response.Contact.Phone,
                Address = response.Contact.Address
            };
        }
    }
}
