using System;
namespace Contact.Domain.DTOs
{
    /// <summary>
    /// User contact data transfer object
    /// </summary>
    public class UserContactDto
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}

