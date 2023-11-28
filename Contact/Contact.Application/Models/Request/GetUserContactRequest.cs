using System;
using Contact.Domain.Enums;

namespace Contact.Application.Models.Request
{
    public class GetUserContactRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? Take { get; set; }
        public int Page { get; set; } = 1;
        public byte SortById { get; set; } = (byte)SortBy.Date;
    }
}

