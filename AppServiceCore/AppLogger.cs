using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace AppServiceCore.Logging
{
    public enum LoggerCategoryType
    {
        AppLogger = 0,
        OpenAiChatCompletions = 1,
        AssessmentSuite = 2,
        WeatherLibrary = 3,
        BusStopSimulation = 4,
        ParkingLotSimulation = 5,
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
        private static ILoggerFactory? _loggerFactory;
        private static readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();

        // InitializeLogger() is called by Program.cs
        public static void InitializeLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static ILogger GetLogger(LoggerCategoryType loggerCategoryType = LoggerCategoryType.AppLogger)
        {
            if (_loggerFactory == null)
            {
                throw new InvalidOperationException("LoggerFactory is not initialized. Call InitializeLogger() first.");
            }

            var categoryName = loggerCategoryType.ToString();
            return _loggers.GetOrAdd(categoryName, newCategoryName => _loggerFactory.CreateLogger(categoryName));
        }
    }

    //public static class MyLogger
    //{
    //    private static ILoggerFactory? _loggerFactory;
    //    private static ILogger _logger;
    //
    //    public static void InitializeLogger(ILoggerFactory loggerFactory)
    //    {
    //        _loggerFactory = loggerFactory;
    //        _logger = _loggerFactory.CreateLogger("AppLogger");
    //    }
    //
    //    public static ILogger Logger
    //    {
    //        get
    //        {
    //            return _logger;
    //        }
    //    }
    //}
}
