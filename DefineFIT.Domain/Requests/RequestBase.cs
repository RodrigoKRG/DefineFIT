using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace DefineFIT.Domain.Requests
{
    public abstract class RequestBase
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        protected RequestBase()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
