using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSuiteLibrary.Services
{
    public sealed class Singleton
    {
        // sealed : prevents inheritance of the singleton class
        private static int _counter = 0;
        private static Singleton _instance = null;
        private static readonly object Instancelock = new object();
        private ILogger<AssessmentSuiteService> _logger;

        // private constructor
        private Singleton(ILogger<AssessmentSuiteService> logger)
        {
            _logger = logger;
            _counter++;
            _logger.LogInformation($"Singleton Constructor Executed {_counter} Time(s)");
        }

        public static Singleton GetSingletonInstance(ILogger<AssessmentSuiteService> logger)
        {
            // Create singleton, if needed.  Then, return a reference to the singleton.
            lock (Instancelock)
            {
                if (_instance == null)
                {
                    _instance = new Singleton(logger);
                }
                else if (_instance._logger == null)
                {
                    _instance._logger = logger;
                }

                return _instance;
            }
        }

        public void LogMessage(string message)
        {
            _logger?.LogInformation($"Counter: {_counter}   message: {message}");
        }
    }
}
