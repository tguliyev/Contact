using Contact.Application.CQRS.Core;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserContactService _userContactService;


        public UserController(IUserService userService,
                              IUserContactService userContactService)
        {
            _userService = userService;
            _userContactService = userContactService;
        }

        #region GET Requests
        [HttpGet("contacts")]
        public async Task<ActionResult<ApiResult<GetUserContactResponse>>> GetUserContacts([FromQuery] GetUserContactRequest request)
         => await _userContactService.GetUserContacts(request, GetUser());


        [HttpGet("contacts/{contactId}")]
        public async Task<ActionResult<ApiResult<GetUserContactDetailByIdResponse>>>
            GetUserContactDetailById(int contactId)
        => await _userContactService.GetUserContactDetailById(contactId, GetUser());

        #endregion

        #region POST Requests
        [HttpPost("contacts")]
        public async Task<ActionResult<ApiResult<CreateUserContactResponse>>> CreateUserContact(CreateUserContactRequest request)
       => await _userContactService.CreateUserContact(request, GetUser());

        #endregion

        #region PUT Requests
        [HttpPost("contacts/{contactId}/update")]
        public async Task<ActionResult<ApiResult<UpdateUserContactResponse>>> UpdateUserContact(UpdateUserContactRequest request, int contactId)
         => await _userContactService.UpdateUserContact(request, contactId, GetUser());
        #endregion

        #region DELETE Request
        [HttpDelete("contacts/{contactId}")]
        public async Task<ActionResult<ApiResult<DeleteUserContactResponse>>>
            DeleteUserContact(int contactId)
         => await _userContactService.DeleteUserContact(contactId, GetUser());
        #endregion
    }
}