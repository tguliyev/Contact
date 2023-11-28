using System;
namespace Contact.Application.Models.Request
{
    public class UpdateUserContactRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}

