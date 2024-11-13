using AppServiceCore.Logging;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BusStopSimulation.Entities
{
    public class BusStop
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.BusStopSimulation);

        private Queue<Passenger> _peopleWaiting = new Queue<Passenger>();

        public void PersonArrive(Passenger passenger)
        {
            _peopleWaiting.Enqueue(passenger);
            _logger.LogInformation($"{passenger} queuing at the bus stop");
        }

        public void BusArrive(Bus bus)
        {
            _logger.LogInformation($"Bus arriving at bus stop to load passengers.");
            while (bus.Space > 0 && _peopleWaiting.Count > 0)
            {
                var passenger = _peopleWaiting.Dequeue();
                bus.Load(passenger);
            }
        }
    }
}
