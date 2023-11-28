using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.Application.Models.Response
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

