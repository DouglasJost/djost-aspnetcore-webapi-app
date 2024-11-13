using BusStopSimulation.Entities;
using BusStopSimulation.Models.Enums;

namespace BusStopSimulation.Services
{
    public static class PassengerGeneratorService
    {
        private static int _numberOfPassengers = 0;

        public static Passenger CreatePassenger(DestinationType destination)
        {
            return new Passenger($"Person: {++_numberOfPassengers}", destination);
        }
    }
}
