using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace DabeaV2.Logger.Internal
{
    public class BatchingLogger : ILogger
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly BatchingLoggerProvider _provider;
        private readonly string _category;

        public BatchingLogger(BatchingLoggerProvider loggerProvider,  string categoryName, Func<string, LogLevel, bool> filter)
        {
            _provider = loggerProvider;
            _category = categoryName;
            //_filter = filter;
            _filter = filter ?? ((category, logLevel) => true);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // NOTE: Differs from source
            return _provider.ScopeProvider?.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            var result = logLevel != LogLevel.None && (_filter == null || _filter(_category, logLevel));

            return result;
        }

        public void Log<TState>(DateTimeOffset timestamp, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var builder = new StringBuilder();
            builder.Append(timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
            builder.Append(" [");
            builder.Append(logLevel.ToString());
            builder.Append("] ");
            builder.Append(_category);

            var scopeProvider = _provider.ScopeProvider;
            if (scopeProvider != null)
            {
                scopeProvider.ForEachScope((scope, stringBuilder) =>
                {
                    stringBuilder.Append(" => ").Append(scope);
                }, builder);

                builder.AppendLine(":");
            }
            else
            {
                builder.Append(": ");
            }

            builder.AppendLine(formatter(state, exception));

            if (exception != null)
            {
                builder.AppendLine(exception.ToString());
            }

            _provider.AddMessage(timestamp, builder.ToString());
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log(DateTimeOffset.Now, logLevel, eventId, state, exception, formatter);
        }
    }
}