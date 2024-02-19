namespace DefineFIT.Domain.Common.Exceptions;

public class BaseException
{
    public string? ContextTraceId { get; set; }
    public IEnumerable<ExceptionInfo>? Errors { get; set; }
}