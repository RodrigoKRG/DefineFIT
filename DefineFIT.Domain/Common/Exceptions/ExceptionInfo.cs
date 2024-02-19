namespace DefineFIT.Domain.Common.Exceptions;

public class ExceptionInfo
{
    public string Type { get; init; }
    public string Error { get; init; }
    public string? Detail { get; init; }
}