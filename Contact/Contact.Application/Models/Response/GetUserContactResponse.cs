using System;
using Contact.Domain.DTOs;

namespace Contact.Application.Models.Response
{
    public class GetUserContactResponse
    {
        public List<UserContactDto> Contacts { get; set; }
        public int Page { get; set; }
        public int? Take { get; set; }
        public int TotalCount { get; set; }
    }
}

