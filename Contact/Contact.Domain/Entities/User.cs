using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Contact.Domain.Enums;

namespace Contact.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public byte UserStatusId { get; set; }


        public User()
        {
            UserContacts = new HashSet<UserContact>();
        }
        public User(string name, string surname, string email, string username)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Username = username;
            Created = DateTime.Now;
            UserStatusId = (byte)UserStatus.Active;
        }

        public ICollection<UserContact> UserContacts { get; set; }

        public void AddPassword(string password)
        {
            Guid guid = Guid.NewGuid();

            using (SHA256 sha256 = SHA256.Create())
            {
                var salt = sha256.ComputeHash(Encoding.UTF8.GetBytes(guid.ToString()));



                using (HMACSHA256 hmacSha256 = new HMACSHA256(salt))
                {
                    var buffer = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(password));


                    Salt = salt;
                    Password = buffer;
                }
            }

        }

        public bool CheckPassword(string password)
        {
            using (HMACSHA256 hmacSha256 = new HMACSHA256(Salt))
            {
                var buffer = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                return buffer.SequenceEqual(Password);
            }
        }
    }
}
