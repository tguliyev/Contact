using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.Application.Models.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

