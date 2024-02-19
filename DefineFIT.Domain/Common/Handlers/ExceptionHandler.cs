using System.Runtime.CompilerServices;
using DefineFIT.Domain.Common.Exceptions;
using Microsoft.Extensions.Logging;


namespace DefineFIT.Domain.Common.Handlers;

public static class ExceptionHandler
{
#pragma warning disable S3343 // Caller information parameters should come at the end of the parameter list
    public static T CreateException<T>(string message, ILogger logger, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string methodName = "MethodNotFound", params object?[] parameters) where T : Exception
#pragma warning restore S3343 // Caller information parameters should come at the end of the parameter list
    {
        logger.Log(logLevel, $"{methodName}: {message}", parameters);
        var exceptionInfo = new ExceptionInfo { Type = typeof(T).Name, Error = string.Format(message, parameters) };
        return (T?)Activator.CreateInstance(typeof(T), exceptionInfo) ?? Activator.CreateInstance<T>();
    }

#pragma warning disable S3343 // Caller information parameters should come at the end of the parameter list
    public static T CreateException<T>(List<string> messagesList, ILogger logger, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string methodName = "MethodNotFound", List<string[]>? parameters = null) where T : Exception
#pragma warning restore S3343 // Caller information parameters should come at the end of the parameter list
    {
        var exceptionInfosList = new List<ExceptionInfo>();
        for (int i = 0; i < messagesList.Count; i++)
        {
            if (parameters != null && parameters[i] != null)
            {
                logger.Log(logLevel, $"{methodName}: {messagesList[i]}", parameters[i]);
                exceptionInfosList.Add(new ExceptionInfo { Type = typeof(T).Name, Error = string.Format(messagesList[i], parameters[i]) });
            }
            else
            {
                logger.Log(logLevel, $"{methodName}: {messagesList[i]}");
                exceptionInfosList.Add(new ExceptionInfo { Type = typeof(T).Name, Error = messagesList[i] });
            }
        }
        return (T?)Activator.CreateInstance(typeof(T), exceptionInfosList) ?? Activator.CreateInstance<T>();
    }
}