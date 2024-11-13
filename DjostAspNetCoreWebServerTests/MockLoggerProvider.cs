using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DjostAspNetCoreWebServerTests
{
    public class MockLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, MockLogger> _loggers = new ConcurrentDictionary<string, MockLogger>();

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new MockLogger(name));
        }

        public MockLogger GetLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new MockLogger(name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        public class MockLogger : ILogger
        {
            private readonly string _categoryName = string.Empty;
            public readonly List<string> LoggedTraceMessages = new List<string>();
            public readonly List<string> LoggedDebugMessages = new List<string>();
            public readonly List<string> LoggedInformationMessages = new List<string>();
            public readonly List<string> LoggedWarningMessages = new List<string>();
            public readonly List<string> LoggedErrorMessages = new List<string>();
            public readonly List<string> LoggedCriticalMessages = new List<string>();
            public readonly List<string> LoggedFatalMessages = new List<string>();

            public MockLogger(string categoryName)
            {
                _categoryName = categoryName;
            }

            public IDisposable? BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                var message = formatter(state, exception);
                var logMessage = $"{logLevel}: {_categoryName} - {message}";

                switch (logLevel)
                {
                    case LogLevel.Trace:
                    {
                        LoggedTraceMessages.Add(logMessage);
                        break;
                    }

                    case LogLevel.Debug:
                    {
                        LoggedDebugMessages.Add(logMessage);
                        break;
                    }

                    case LogLevel.Information:
                    {
                        LoggedInformationMessages.Add(logMessage);
                        break;
                    }

                    case LogLevel.Warning:
                    {
                        LoggedWarningMessages.Add(logMessage);
                        break;
                    }

                    case LogLevel.Error:
                    {
                        LoggedErrorMessages.Add(logMessage);
                        break;
                    }

                    case LogLevel.Critical:
                    {
                        LoggedCriticalMessages.Add(logMessage);
                        break;
                    }

                    default:
                    {
                        LoggedInformationMessages.Add(logMessage);
                        break;
                    }
                }

                return;
            }
        }
    }
}
