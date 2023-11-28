using System;
using Contact.Domain.Entities;

namespace Contact.Application.Interfaces
{
    /// <summary>
    /// JWT service has been made in order to increase use case of JWT processes
    /// </summary>
    public interface IJWTService
    {
        public string GenerateJwtToken(User user);
        public string ValidateJwtToken(string token);
    }
}

