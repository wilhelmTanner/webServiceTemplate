namespace Tanner.Template.Base.Common.Extensions;

/// <summary>
/// Extensions for <see cref="Microsoft.Extensions.Logging.ILogger"/>
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Método para armar el formato de los mensajes a grabar
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="type"></param>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    public static void Log(this ILogger logger, LogLevel type, string method, IDictionary<string, object> parameters)
    {
        var sb = new StringBuilder(method);

        foreach (var parameter in parameters)
        {
            sb.Append(" ");

            if (parameter.Value == null)
                sb.AppendFormat("[{0}=]", parameter.Key);
            else if (parameter.Value is IEnumerable)
            {
                sb.AppendFormat("[{0}=", parameter.Key);

                foreach (object v in parameter.Value as IEnumerable)
                    sb.Append(v);

                sb.Append("]");
            }
            else
            {
                if (parameter.Value is Exception ex)
                {
                    sb.AppendFormat("[{0}={1}]", parameter.Key, ex.ToString());
                }
                else
                {
                    sb.AppendFormat("[{0}={1}]", parameter.Key, parameter.Value);
                }
            }
        }
        Console.WriteLine($"Message:{method}, Parameters: {sb.ToString()}");
        switch (type)
        {
            case LogLevel.Error:
                logger.LogError(sb.ToString());
                break;
            case LogLevel.Warning:
                logger.LogWarning(sb.ToString());
                break;
            case LogLevel.Information:
                logger.LogInformation(sb.ToString());
                break;
            case LogLevel.Trace:
                logger.LogTrace(sb.ToString());
                break;
            default:
                logger.LogInformation(sb.ToString());
                break;
        }
    }

    /// <summary>
    /// Graba mensaje con LogLevel.Information
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    public static void LogInformation(this ILogger logger, string method, IDictionary<string, object> parameters)
    {
        logger.Log(LogLevel.Information, method, parameters);
    }


    /// <summary>
    /// Graba mensaje con LogLevel.Error
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logger"></param>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    public static void LogException<T>(this ILogger logger, string method, IDictionary<string, object> parameters)
    {
        logger.Log(LogLevel.Error, method, parameters);
    }
}