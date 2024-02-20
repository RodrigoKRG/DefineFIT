using DefineFIT.Domain.Entities;

namespace DefineFIT.Domain.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthTime { get; set; }
        public bool active { get; set; }

        public static UserResponse Build(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Cpf = user.Cpf,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                BirthTime = user.BirthDate,
                active = user.Active
            };
        }
    }
}
