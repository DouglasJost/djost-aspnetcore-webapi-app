using AppServiceCore.Logging;
using BusStopSimulation.Models.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BusStopSimulation.Entities
{
    public class Bus
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.BusStopSimulation);

        public const int Capacity = 5;
        private LinkedList<Passenger> _passengers = new LinkedList<Passenger>();

        public int Space
        {
            get
            {
                return Capacity - _passengers.Count;
            }
        }

        public bool Load(Passenger passenger)
        {
            if (Space < 1)
            {
                return false;
            }

            _passengers.AddLast(passenger);
            _logger.LogInformation($"{passenger} got on the bus");
            return true;
        }

        public void ArriveAt(DestinationType destination)
        {
            _logger.LogInformation($"Bus arriving at {destination.ToString()}");
            if (_passengers.Count <= 0)
            {
                _logger.LogInformation($"Bus is empty.  No one is departing.");
                return;
            }

            var numberOfPassengersDeparted = 0;
            LinkedListNode<Passenger>? currentNode = _passengers.First;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                if (currentNode.Value.Destination == destination)
                {
                    _logger.LogInformation($"{currentNode.Value.Name} is departing the bus.");
                    _passengers.Remove(currentNode);
                    numberOfPassengersDeparted++;
                }
                currentNode = currentNode.Next;
            }

            _logger.LogInformation($"{numberOfPassengersDeparted} passengers departed at {destination.ToString()}");
            _logger.LogInformation($"{_passengers.Count} people are left on the bus");
        }
    }
}
