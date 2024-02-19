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
        public string PhoneNumber { get; private set; } = string.Empty;
        public DateTime BirthDate { get; private set; }
        public Roles Role { get; private set; } = Roles.User;


        public static User Create(UserRequest userRequest)
        {
            var user =  new User
            {
                Name = userRequest.Name,
                Cpf = userRequest.Cpf,
                Email = userRequest.Email,
                Password = userRequest.Password,
                PhoneNumber = userRequest.PhoneNumber,
                BirthDate = userRequest.BirthDate,
            };

            user.SetAudit(userRequest.User);
            return user;
        }

        public void Update(UserRequest userRequest)
        {
            Name = userRequest.Name;
            Cpf = userRequest.Cpf;
            Email = userRequest.Email;
            Password = userRequest.Password;
            PhoneNumber = userRequest.PhoneNumber;
            BirthDate = userRequest.BirthDate;

            SetAudit(userRequest.User);
        }
    }
}
