using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Application.CQRS.Core;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.Domain.Entities;

namespace Contact.Application.Interfaces
{
    /// <summary>
    /// User Contact Service is responsible about contact CRUD operations
    /// </summary>
    public interface IUserContactService
    {
        Task<ApiResult<GetUserContactResponse>> GetUserContacts(GetUserContactRequest model, int userId);

        Task<ApiResult<GetUserContactDetailByIdResponse>> GetUserContactDetailById(int contactId, int userId);

        Task<ApiResult<CreateUserContactResponse>> CreateUserContact(CreateUserContactRequest model, int userId);


        Task<ApiResult<UpdateUserContactResponse>> UpdateUserContact(UpdateUserContactRequest model, int contactId, int userId);

        Task<ApiResult<DeleteUserContactResponse>> DeleteUserContact(int contactId, int userId);

    }
}
