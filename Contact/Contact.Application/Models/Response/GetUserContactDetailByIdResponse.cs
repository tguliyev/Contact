using System;
using Contact.Domain.DTOs;

namespace Contact.Application.Models.Response
{
    public class GetUserContactDetailByIdResponse
    {
        public UserContactDto Contact { get; set; }
    }
}

