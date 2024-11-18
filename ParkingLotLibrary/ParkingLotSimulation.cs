using System;
using System.Collections.Generic;
using System.Linq;
using AppServiceCore;
using Microsoft.Extensions.Logging;
using ParkingLotLibrary.Services;
using ParkingLotLibrary.Models;
using ParkingLotLibrary.Interfaces;
using ParkingLotLibrary.Models.Enums;
using Serilog;
//using AppServiceCore.Logging;

namespace ParkingLotLibrary
{
    public class ParkingLotSimulation : IParkingLotSimulation
    {
        //
        // The following Serilog NuGet packages need to be installed
        //   Serilog and Serilog.Extensions.Logging 
        //

        // private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.ParkingLotSimulation);
        private readonly Serilog.ILogger _logger = AppServiceCore.Loggers.AppSerilogLogger.GetLogger(AppServiceCore.Loggers.SerilogLoggerCategoryType.ParkingLotSimulation);


        public CommandResult<ParkingLotSimulationResponseDto> RunSimulation()
        {
            _logger.Information($"RunSimulation");

            var response = new ParkingLotSimulationResponseDto
            {
                SimulationName = "Parking Lot",
                SimulationResults = new List<string>(),
            };

            try
            {
                //var _nonCategoryLogger = Log.Logger<ParkingLotSimulation>();
                //_nonCategoryLogger.Information("This is a test INFORMATION message");
                //_nonCategoryLogger.Debug("This is a test DEBUG message");

                var parkingLotService = new ParkingLotService(5, 5, 5);
                _logger.Information($"Initializing Parking Lot Service with 5 motorcycle, 5 car and 5 van parking slots.");

                response.SimulationResults.Add("Initialized Parking Lot Service with 5 Motorcycles, 5 Cars, and 5 Vans.");
                _logger.Debug($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Total spots: {parkingLotService.GetTotalSpots()}");
                _logger.Debug($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Remaining spots: {parkingLotService.GetRemainingSpots()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Is Full: {parkingLotService.IsFull()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Is Empty: {parkingLotService.IsEmpty()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                //var motorcycle = new MotorCycle();
                //var car = new Car();
                //var van = new Van();

                var motorcycle = VehicleFactory.CreateVehicle(VehicleType.Motocycle);
                var car = VehicleFactory.CreateVehicle(VehicleType.Car);
                var van = VehicleFactory.CreateVehicle(VehicleType.Van);

                parkingLotService.ParkVehicle(motorcycle);
                response.SimulationResults.Add($"Parked a motorcycle.");
                _logger.Information($"{response.SimulationResults.Last()}");

                parkingLotService.ParkVehicle(car);
                response.SimulationResults.Add($"Parked a car.");
                _logger.Information($"{response.SimulationResults.Last()}");

                parkingLotService.ParkVehicle(van);
                response.SimulationResults.Add($"Parked a van.");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Remaining spots after parking: {parkingLotService.GetRemainingSpots()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Is Full: {parkingLotService.IsFull()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Is Empty: {parkingLotService.IsEmpty()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Are motorcycle spots full: {parkingLotService.AreMotorcycleSpotsFull()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Spots taken by vans: {parkingLotService.GetSpotsTakenByVans()}");
                _logger.Information($"{response.SimulationResults.Last()}");

                response.SimulationResults.Add($"Ending simulation.");
                _logger.Information($"{response.SimulationResults.Last()}");
            }
            catch (Exception ex)
            {
                return CommandResult<ParkingLotSimulationResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<ParkingLotSimulationResponseDto>.Success(response);
        }
    }
}
