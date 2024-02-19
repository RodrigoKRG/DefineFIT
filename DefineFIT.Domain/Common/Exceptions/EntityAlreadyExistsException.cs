using DefineFIT.Domain.Common.Extensions;
using System.Runtime.Serialization;

namespace DefineFIT.Domain.Common.Exceptions;

[Serializable]
public class EntityAlreadyExistsException : Exception, IBusinessException
{
    public EntityAlreadyExistsException(ExceptionInfo exceptionInfo) : base(exceptionInfo.ToMessage())
    {
    }

    public EntityAlreadyExistsException(IEnumerable<ExceptionInfo> exceptionInfos)
        : base(exceptionInfos.ToMessage())
    {
    }

    protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}