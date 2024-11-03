using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceCore.Models.AssessmentSuite;
using AppServiceCore;
using Microsoft.Extensions.Logging;
using ParkingLotLibrary.Models.Vehicles;
using ParkingLotLibrary.Services;
using ParkingLotLibrary.Models;
using ParkingLotLibrary.Interfaces;

namespace ParkingLotLibrary
{
    public class ParkingLotSimulation : IParkingLotSimulation
    {
        public CommandResult<ParkingLotSimulationResponseDto> RunSimulation()
        {
            var response = new ParkingLotSimulationResponseDto
            {
                SimulationName = "Parking Lot",
                SimulationResults = new List<string>(),
            };

            try
            {
                var parkingLotService = new ParkingLotService(5, 5, 5);

                response.SimulationResults.Add("Initialized Parking Lot Service with 5 Motorcycles, 5 Cars, and 5 Vans.");
                response.SimulationResults.Add($"Total spots: {parkingLotService.GetTotalSpots()}");
                response.SimulationResults.Add($"Remaining spots: {parkingLotService.GetRemainingSpots()}");
                response.SimulationResults.Add($"Is Full: {parkingLotService.IsFull()}");
                response.SimulationResults.Add($"Is Empty: {parkingLotService.IsEmpty()}");

                var motorcycle = new MotorCycle();
                var car = new Car();
                var van = new Van();

                parkingLotService.ParkVehicle(motorcycle);
                response.SimulationResults.Add($"Parked a motorcycle.");

                parkingLotService.ParkVehicle(car);
                response.SimulationResults.Add($"Parked a car.");

                parkingLotService.ParkVehicle(van);
                response.SimulationResults.Add($"Parked a van.");

                response.SimulationResults.Add($"Remaining spots after parking: {parkingLotService.GetRemainingSpots()}");
                response.SimulationResults.Add($"Is Full: {parkingLotService.IsFull()}");
                response.SimulationResults.Add($"Is Empty: {parkingLotService.IsEmpty()}");
                response.SimulationResults.Add($"Are motorcycle spots full: {parkingLotService.AreMotorcycleSpotsFull()}");
                response.SimulationResults.Add($"Spots taken by vans: {parkingLotService.GetSpotsTakenByVans()}");

                response.SimulationResults.Add($"Ending simulation.");
            }
            catch (Exception ex)
            {
                return CommandResult<ParkingLotSimulationResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<ParkingLotSimulationResponseDto>.Success(response);
        }
    }
}
