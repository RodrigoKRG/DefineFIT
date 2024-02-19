using DefineFIT.Domain.Common.Exceptions;

namespace DefineFIT.Domain.Common.Extensions;

public static class ExceptionExtension
{
    public static IEnumerable<ExceptionInfo> BuildErrors(this Exception e) =>
        e.GetType().GetInterface(nameof(IBusinessException)) == null
            ? new List<ExceptionInfo>
            {
                new()
                {
                    Type = e.GetType().Name.Replace("Exception", string.Empty),
                    Error = e.Message,
                    Detail = string.Empty
                }
            }
            : e.Message.ToExceptionInfos();        
}