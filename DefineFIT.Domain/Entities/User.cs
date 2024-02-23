using DefineFIT.Domain.Enums;
using DefineFIT.Domain.Requests;

namespace DefineFIT.Domain.Entities
{
    public class User : AuditEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public byte[] Salt { get; set; } = new byte[16];
        public string PhoneNumber { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }
        public Roles Role { get; private set; } = Roles.User;
        public bool Active { get; private set; }


        public static User Create(UserCreateRequest userRequest)
        {
            var user =  new User
            {
                Name = userRequest.Name,
                Cpf = userRequest.Cpf,
                Email = userRequest.Email,
                Password = userRequest.Password,
                PhoneNumber = userRequest.PhoneNumber,
                BirthDate = userRequest.BirthDate,
                Active = true
            };

            user.SetAudit(userRequest.User);
            return user;
        }

        public void Update(UserUpdateRequest userRequest)
        {
            Name = userRequest.Name ?? Name;
            Cpf = userRequest.Cpf ?? Cpf;
            Email = userRequest.Email ?? Email;
            PhoneNumber = userRequest.PhoneNumber ?? PhoneNumber;
            BirthDate = userRequest.BirthDate ?? BirthDate;
            Active = userRequest.Active ?? Active;
            
            SetAudit(userRequest.User);
        }

        public void SetPassword(string password, byte[] salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
