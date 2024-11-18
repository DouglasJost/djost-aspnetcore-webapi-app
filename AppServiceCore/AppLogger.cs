//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace AppServiceCore.Logging
{
    // Make sure to add the LogLevel to appsettings.json
    public enum LoggerCategoryType
    {
        AppLogger,
        LoginAuthentication,
        OpenAiChatCompletions,
        AssessmentSuite,
        WeatherLibrary,
        BusStopSimulation,
        ParkingLotSimulation,
    }


    // AppLogger singleton:
    //   * class is static which cannot be instantiated.
    //   * static class design enforces a single instance implicitly, as it cannot be instantiated and all members are shared.
    //   * shares single instance of ILogger.
    //   * AppLogger.InitializeLogger() is called by Program.cs.
    public static class AppLogger
    {
        //
        // Default Log Level is set in appsettings.Development.json and appsettings.json
        //
        private static Microsoft.Extensions.Logging.ILoggerFactory? _loggerFactory;
        private static readonly ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = new ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger>();

        // InitializeLogger() is called by Program.cs
        public static void InitializeLogger(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static Microsoft.Extensions.Logging.ILogger GetLogger(LoggerCategoryType loggerCategoryType = LoggerCategoryType.AppLogger)
        {
            if (_loggerFactory == null)
            {
                throw new InvalidOperationException("LoggerFactory is not initialized. Call InitializeLogger() first.");
            }

            var categoryName = loggerCategoryType.ToString();
            return _loggers.GetOrAdd(categoryName, newCategoryName => _loggerFactory.CreateLogger(categoryName));
        }
    }
}
