using System;
using System.Runtime.CompilerServices;
using Common.Log;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Lykke.Common.Log
{
    /// <summary>
    /// Extension methods for the Lykke logging system, based on Microsoft.Extensions.Logging
    /// </summary>
    [PublicAPI]
    public static class MicrosoftLoggingBasedLogExtensions
    {
        private static Func<LogEntryParameters, Exception, string> _defaultMessageFormatter = (parameters, exception) => parameters.Message;

        #region Trace

        /// <summary>
        /// <p>
        /// Writes entry to the trace log.
        /// </p>
        /// <p>
        /// Trace - logs that contain the most detailed messages. 
        /// These messages may contain sensitive application data. 
        /// These messages are disabled by default and should never be enabled in a production environment.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Trace(ILog, string, string, object, Exception, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Trace(
            [NotNull] this ILog log, 
            [NotNull] string message, 
            [CanBeNull] object context = null, 
            [CanBeNull] Exception exception = null, 
            [CanBeNull] DateTime? moment = null, 
            [CallerFilePath] string callerFilePath = null, 
            [CallerMemberName] string process = null, 
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Trace, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process ?? throw new ArgumentNullException(nameof(process)), 
                callerLineNumber,
                message ?? throw new ArgumentNullException(nameof(message)),
                context,
                exception, 
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the trace log.
        /// </p>
        /// <p>
        /// Trace - logs that contain the most detailed messages. 
        /// These messages may contain sensitive application data. 
        /// These messages are disabled by default and should never be enabled in a production environment.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Trace(ILog, string, object, Exception, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Trace(
            [NotNull] this ILog log,
            [NotNull] string process,
            [NotNull] string message,
            [CanBeNull] object context = null,
            [CanBeNull] Exception exception = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Trace, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process, 
                callerLineNumber,
                message ?? throw new ArgumentNullException(nameof(message)),
                context, 
                exception, 
                moment);
        }

        #endregion


        #region Debug

        /// <summary>
        /// <p>
        /// Writes entry to the debug log.
        /// </p>
        /// <p>
        /// Debug - logs that are used for interactive investigation during development.
        /// These logs should primarily contain information useful for debugging and have no long-term value.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Debug(ILog, string, string, object, Exception, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Debug(
            [NotNull] this ILog log,
            [NotNull] string message,
            [CanBeNull] object context = null,
            [CanBeNull] Exception exception = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string process = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Debug, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process ?? throw new ArgumentNullException(nameof(process)), 
                callerLineNumber,
                message ?? throw new ArgumentNullException(nameof(message)),
                context, 
                exception,
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the debug log.
        /// </p>
        /// <p>
        /// Debug - logs that are used for interactive investigation during development.
        /// These logs should primarily contain information useful for debugging and have no long-term value..
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Debug(ILog, string, object, Exception, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Debug(
            [NotNull] this ILog log,
            [NotNull] string process,
            [NotNull] string message,
            [CanBeNull] object context = null,
            [CanBeNull] Exception exception = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Debug, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process, 
                callerLineNumber,
                message ?? throw new ArgumentNullException(nameof(message)),
                context,
                exception, 
                moment);
        }

        #endregion


        #region Info

        /// <summary>
        /// <p>
        /// Writes entry to the info log.
        /// </p>
        /// <p>
        /// Info - logs that track the general flow of the application.These logs should have long-term value.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Info(ILog, string, string, object, Exception, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Info(
            [NotNull] this ILog log,
            [NotNull] string message,
            [CanBeNull] object context = null,
            [CanBeNull] Exception exception = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string process = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Information, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)),
                process ?? throw new ArgumentNullException(nameof(process)),
                callerLineNumber,
                message ?? throw new ArgumentNullException(nameof(message)),
                context, 
                exception,
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the info log.
        /// </p>
        /// <p>
        /// Info - logs that track the general flow of the application.These logs should have long-term value.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Info(ILog, string, object, Exception, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Info(
            [NotNull] this ILog log,
            [NotNull] string process,
            [NotNull] string message,
            [CanBeNull] object context = null,
            [CanBeNull] Exception exception = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Information,
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process, 
                callerLineNumber, 
                message ?? throw new ArgumentNullException(nameof(message)), 
                context,
                exception, 
                moment);
        }

        #endregion


        #region Warning

        /// <summary>
        /// <p>
        /// Writes entry to the warning log.
        /// </p>
        /// <p>
        /// Warning - logs that highlight an abnormal or unexpected event in the application flow, 
        /// but do not otherwise cause the application execution to stop.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Warning(ILog, string, string, Exception, object, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Warning(
            [NotNull] this ILog log,
            [CanBeNull] string message,
            [CanBeNull] Exception exception = null,
            [CanBeNull] object context = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string process = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Warning, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process ?? throw new ArgumentNullException(nameof(process)), 
                callerLineNumber,
                message, 
                context,
                exception,
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the warning log.
        /// </p>
        /// <p>
        /// Warning - logs that highlight an abnormal or unexpected event in the application flow, 
        /// but do not otherwise cause the application execution to stop.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Warning(ILog, string, Exception, object, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Warning(
            [NotNull] this ILog log,
            [NotNull] string process,
            [CanBeNull] string message,
            [CanBeNull] Exception exception = null,
            [CanBeNull] object context = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Warning, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)),
                process, 
                callerLineNumber, 
                message,
                context, 
                exception,
                moment);
        }

        #endregion


        #region Error

        /// <summary>
        /// <p>
        /// Writes entry to the error log.
        /// </p>
        /// <p>
        /// Error - logs that highlight when the current flow of execution is stopped due to a failure.
        /// These should indicate a failure in the current activity, not an application-wide failure.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Error(ILog, string, Exception, string, object, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Error(
            [NotNull] this ILog log,
            [CanBeNull] Exception exception = null,
            [CanBeNull] string message = null,
            [CanBeNull] object context = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string process = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Error, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process ?? throw new ArgumentNullException(nameof(process)), 
                callerLineNumber, 
                message, 
                context, 
                exception,
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the error log.
        /// </p>
        /// <p>
        /// Error - logs that highlight when the current flow of execution is stopped due to a failure.
        /// These should indicate a failure in the current activity, not an application-wide failure.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Error(ILog, Exception, string, object, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Error(
            [NotNull] this ILog log,
            [NotNull] string process,
            [CanBeNull] Exception exception = null,
            [CanBeNull] string message = null,
            [CanBeNull] object context = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Error, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process, 
                callerLineNumber,
                message, 
                context, 
                exception, 
                moment);
        }

        #endregion


        #region Critical

        /// <summary>
        /// <p>
        /// Writes entry to the critical log.
        /// </p>
        /// <p>
        /// Critical - logs that describe an unrecoverable application or system crash, 
        /// or a catastrophic failure that requires immediate attention.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="process">
        /// Name of the method where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler.
        /// If you want to specify custom process name, use <see cref="Critical(ILog, string, Exception, string, object, DateTime?, string, int)"/> overload
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Critical(
            [NotNull] this ILog log,
            [CanBeNull] Exception exception = null,
            [CanBeNull] string message = null,
            [CanBeNull] object context = null,
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string process = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Critical, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process ?? throw new ArgumentNullException(nameof(process)),
                callerLineNumber, 
                message,
                context,
                exception,
                moment);
        }

        /// <summary>
        /// <p>
        /// Writes entry to the critical log.
        /// </p>
        /// <p>
        /// Critical - logs that describe an unrecoverable application or system crash, 
        /// or a catastrophic failure that requires immediate attention.
        /// </p>
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="process">
        /// Custom name of the process where the entry was made. 
        /// If you want to use method name as the process name, use <see cref="Critical(ILog, Exception, string, object, DateTime?, string, string, int)"/> overload
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made. 
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Do not pass argument to this parameter, it will be done by the compiler
        /// </param>
        public static void Critical(
            [NotNull] this ILog log, 
            [NotNull] string process,
            [CanBeNull] Exception exception = null,
            [CanBeNull] string message = null,
            [CanBeNull] object context = null, 
            [CanBeNull] DateTime? moment = null,
            [CallerFilePath] string callerFilePath = null, 
            [CallerLineNumber] int callerLineNumber = 0)
        {
            log.Log(
                LogLevel.Critical, 
                callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath)), 
                process, 
                callerLineNumber,
                message, 
                context, 
                exception, 
                moment);
        }

        #endregion


        #region Common

        /// <summary>
        /// This is low level method and usually should not be used directly in the app.
        /// It's inteded to be used only by the extension methods.
        /// App developer usually should use one of:
        /// <see cref="Trace(ILog, string, object, Exception, DateTime?, string, string, int)"/>,
        /// <see cref="Debug(ILog, string, object, Exception, DateTime?, string, string, int)"/>,
        /// <see cref="Info(ILog, string, object, Exception, DateTime?, string, string, int)"/>,
        /// <see cref="Warning(ILog, string, Exception, object, DateTime?, string, string, int)"/>,
        /// <see cref="Error(ILog, Exception, string, object, DateTime?, string, string, int)"/>,
        /// <see cref="Critical(ILog, Exception, string, object, DateTime?, string, string, int)"/>
        /// extension methods or they overloads
        /// </summary>
        /// <param name="log">The log</param>
        /// <param name="logLevel">Log severity level</param>
        /// <param name="callerFilePath">
        /// Path of the source code file, where the entry was made.
        /// Use <see cref="CallerFilePathAttribute"/> in containing extension method, to pass actual file path.
        /// </param>
        /// <param name="process">
        /// Name of the process where the entry was made. It could be method name or custom name.
        /// Use <see cref="CallerMemberNameAttribute"/> in containing extension method, to pass actual method name.
        /// </param>
        /// <param name="callerLineNumber">
        /// Source code file line number, where the entry was made.
        /// Use <see cref="CallerLineNumberAttribute"/> in containing extension method, to pass actual value.
        /// </param>
        /// <param name="message">Message of the entry</param>
        /// <param name="context">Context of the entry</param>
        /// <param name="exception">Exception of the entry</param>
        /// <param name="moment">Moment of the entry. Current momemnt will be used by default.</param>
        public static void Log(
            [NotNull] this ILog log,
            LogLevel logLevel,
            [NotNull] string callerFilePath, 
            [NotNull] string process, 
            int callerLineNumber, 
            [CanBeNull] string message, 
            [CanBeNull] object context, 
            [CanBeNull] Exception exception, 
            [CanBeNull] DateTime? moment)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }
            if (string.IsNullOrWhiteSpace(callerFilePath))
            {
                throw new ArgumentNullException(nameof(callerFilePath));
            }
            if (string.IsNullOrWhiteSpace(process))
            {
                throw new ArgumentNullException(nameof(process));
            }
            if (callerLineNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(callerLineNumber), callerLineNumber, "Should be positive number");
            }
            if(string.IsNullOrWhiteSpace(message) && exception == null)
            {
                throw new ArgumentException("Either message or exception should be specified at least");
            }

            log.Log(
                logLevel,
                0,
                new LogEntryParameters(
                    AppEnvironment.Name,
                    AppEnvironment.Version,
                    string.IsNullOrWhiteSpace(AppEnvironment.EnvInfo) ? "?" : AppEnvironment.EnvInfo,
                    callerFilePath,
                    process,
                    callerLineNumber,
                    message,
                    context,
                    moment),
                exception,
                _defaultMessageFormatter);
        }

        #endregion
    }
}
