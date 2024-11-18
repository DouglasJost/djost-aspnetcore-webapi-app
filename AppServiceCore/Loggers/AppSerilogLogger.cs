using AppServiceCore.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Serilog;

namespace AppServiceCore.Loggers
{
    public enum SerilogLoggerCategoryType
    {
        General,
        LoginAuthentication,
        OpenAiChatCompletions,
        AssessmentSuite,
        WeatherLibrary,
        BusStopSimulation,
        ParkingLotSimulation,
    }

    public static class AppSerilogLogger
    {
        private static Serilog.ILogger? _logger;
        //private static readonly ConcurrentDictionary<string, Serilog.ILogger> _loggers = new ConcurrentDictionary<string, Serilog.ILogger>();

        public static void InitializeLogger(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public static Serilog.ILogger GetLogger(SerilogLoggerCategoryType loggerCategoryType = SerilogLoggerCategoryType.General)
        {
            if (_logger == null)
            {
                throw new InvalidOperationException("Serilog.ILogger is not initialized. Call InitializeLogger() first.");
            }

            var requestedLogger = _logger;
            switch (loggerCategoryType)
            {
                case SerilogLoggerCategoryType.LoginAuthentication:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.LoginAuthentication.ToString());
                        break;
                    }
                case SerilogLoggerCategoryType.OpenAiChatCompletions:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.OpenAiChatCompletions.ToString());
                        break;
                    }
                case SerilogLoggerCategoryType.AssessmentSuite:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.AssessmentSuite.ToString());
                        break;
                    }
                case SerilogLoggerCategoryType.WeatherLibrary:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.WeatherLibrary.ToString());
                        break;
                    }
                case SerilogLoggerCategoryType.BusStopSimulation:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.BusStopSimulation.ToString());
                        break;
                    }
                case SerilogLoggerCategoryType.ParkingLotSimulation:
                    {
                        requestedLogger = _logger.ForContext("SourceContext", SerilogLoggerCategoryType.ParkingLotSimulation.ToString());
                        break;
                    }
                default:
                    {
                        requestedLogger = _logger;
                        break;
                    }
            }

            return requestedLogger;
        }
    }
}
