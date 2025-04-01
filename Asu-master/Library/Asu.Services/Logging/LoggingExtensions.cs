using System;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Logging;

namespace Asu.Services.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Debug, message, exception, customer);
        }
        public static void Information(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Information, message, exception, customer);
        }
        public static void Warning(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Warning, message, exception, customer);
        }
        public static void Error(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Error, message, exception, customer);
        }
        public static void Fatal(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Fatal, message, exception, customer);
        }

        private static void FilteredLog(ILogger logger, LogLevel level, string message, Exception exception = null, Customer customer = null)
        {
            try
            {
                //don't log thread abort exception
                if (exception is System.Threading.ThreadAbortException)
                    return;

                if (logger.IsEnabled(level))
                {
                    string fullMessage = exception == null ? string.Empty : exception.ToString();
                    logger.InsertLog(level, message, fullMessage, customer);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
