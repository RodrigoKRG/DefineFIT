using DefineFIT.Domain.Common.Extensions;
using System.Runtime.Serialization;

namespace DefineFIT.Domain.Common.Exceptions;

[Serializable]
public class MissingRequiredFilterException : Exception, IBusinessException
{
    public MissingRequiredFilterException(ExceptionInfo exceptionInfo)
        : base(exceptionInfo.ToMessage())
    {
    }

    public MissingRequiredFilterException(IEnumerable<ExceptionInfo> exceptionInfos)
        : base(exceptionInfos.ToMessage())
    {
    }

    protected MissingRequiredFilterException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}