namespace DefineFIT.Domain.Responses
{
    public class RegisterResponse
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public RegisterResponse() =>
            Errors = new List<string>();

        public RegisterResponse(bool success = true) : this() =>
            Success = success;

        public void AddError(IEnumerable<string> error) =>
            Errors.AddRange(error);
    }
}
