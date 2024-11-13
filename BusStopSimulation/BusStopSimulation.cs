using AppServiceCore;
using AppServiceCore.Logging;
using BusStopSimulation.Entities;
using BusStopSimulation.Interfaces;
using BusStopSimulation.Models;
using BusStopSimulation.Models.Enums;
using BusStopSimulation.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BusStopSimulation
{
    public class BusStopSimulation : IBusStopSimulation
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.BusStopSimulation);

        public CommandResult<BusStopSimulationResponseDto> RunSimulation()
        {
            _logger.LogInformation("Starting RunSimulation");

            var response = new BusStopSimulationResponseDto
            {
                SimulationName = "Bus Stop",
                SimulationResults = new List<string>(),
            };

            try
            {
                //  Denison,
                //  Sherman,
                //  Howe,
                //  VanAlstyne,
                //  Melissa,
                //  McKinney,
                //  Fairview,
                //  Allen,
                //  Plano,
                //  Richardson,
                //  Dallas,
                var busStop = new BusStop();
                var bus = new Bus();

                // Bus location is Denison.  Loading passengers.                
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Denison));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Sherman));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Howe));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.VanAlstyne));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.VanAlstyne));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Plano));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Plano));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Richardson));
                busStop.PersonArrive(PassengerGeneratorService.CreatePassenger(DestinationType.Dallas));


                busStop.BusArrive(bus);

                bus.ArriveAt(DestinationType.Sherman);
                bus.ArriveAt(DestinationType.Howe);
                bus.ArriveAt(DestinationType.VanAlstyne);
                bus.ArriveAt(DestinationType.Melissa);
                bus.ArriveAt(DestinationType.McKinney);
                bus.ArriveAt(DestinationType.Fairview);
                bus.ArriveAt(DestinationType.Allen);
                bus.ArriveAt(DestinationType.Plano);
                bus.ArriveAt(DestinationType.Richardson);
                bus.ArriveAt(DestinationType.Dallas);
            }
            catch (Exception ex)
            {
                return CommandResult<BusStopSimulationResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<BusStopSimulationResponseDto>.Success(response);
        }
    }
}
