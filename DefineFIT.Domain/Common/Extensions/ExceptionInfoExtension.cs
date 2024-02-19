using DefineFIT.Domain.Common.Exceptions;
using System.Text;

namespace DefineFIT.Domain.Common.Extensions;

public static class ExceptionInfoExtension
{
    public static string ToMessage(this ExceptionInfo exceptionInfo) =>
        new List<ExceptionInfo> { exceptionInfo }.ToMessage();

    public static string ToMessage(this IEnumerable<ExceptionInfo> exceptionInfos)
    {
        var message = new StringBuilder();

        foreach (var exceptionInfo in exceptionInfos)
            message.Append(string.IsNullOrWhiteSpace(exceptionInfo.Detail)
                ? $"{exceptionInfo.Type.Trim()}|{exceptionInfo.Error.Trim()}||"
                : $"{exceptionInfo.Type.Trim()}|{exceptionInfo.Error.Trim()}|{exceptionInfo.Detail.Trim()}||");

        return message.ToString();
    }

    public static IEnumerable<ExceptionInfo> ToExceptionInfos(this string message) => message.Split("||")
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(exceptionInfo => exceptionInfo.Split('|'))
        .Select(properties => new ExceptionInfo
        {
            Type = properties[0],
            Error = properties[1],
            Detail = properties.Length == 3 ? properties[2] : string.Empty
        })
        .ToList();
}