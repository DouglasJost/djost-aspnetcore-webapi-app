using ParkingLotLibrary.Models.Enums;
using ParkingLotLibrary.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingLotLibrary.Services
{
    public class ParkingLotService
    {
        private List<ParkingSpotService> motorcycleSpots;
        private List<ParkingSpotService> compactSpots;
        private List<ParkingSpotService> largeSpots;

        public ParkingLotService(int motorcycleCount, int compactCount, int largeCount)
        {
            motorcycleSpots = new List<ParkingSpotService>();
            compactSpots = new List<ParkingSpotService>();
            largeSpots = new List<ParkingSpotService>();

            for (var i = 0; i < motorcycleCount; i++)
            {
                motorcycleSpots.Add(new ParkingSpotService(SpotType.Motorcycle));
            }

            for (var i = 0; i < compactCount; i++)
            {
                compactSpots.Add(new ParkingSpotService(SpotType.Compact));
            }

            for (var i = 0; i < largeCount; i++)
            {
                largeSpots.Add(new ParkingSpotService(SpotType.Large));
            }
        }

        public int GetTotalSpots()
        {
            return motorcycleSpots.Count + compactSpots.Count + largeSpots.Count;
        }

        public int GetRemainingSpots()
        {
            var remainingSpots =
                motorcycleSpots.Count(s => !s.IsOccupied) +
                compactSpots.Count(s => !s.IsOccupied) +
                largeSpots.Count(s => !s.IsOccupied);

            return remainingSpots;
        }

        public bool IsFull()
        {
            return GetRemainingSpots() == 0;
        }

        public bool IsEmpty()
        {
            return GetTotalSpots() == GetRemainingSpots();
        }

        public bool AreMotorcycleSpotsFull()
        {
            return motorcycleSpots.All(s => s.IsOccupied);
        }

        public int GetSpotsTakenByVans()
        {
            return largeSpots.Count(s => s.IsOccupied);
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            //
            // ParkVehicle is an example of the SOLID Liskov Substitution Principle (LSP).
            //
            // Using a subclass (child / derived class) whenever a Superclass (parent / Base Class) is expected.
            //
            var parkedSuccessfully = false;

            var requiredSpots = vehicle.GetRequiredSpots();

            if (vehicle is MotorCycle)
            {
                parkedSuccessfully = ParkInAvailableSpots(motorcycleSpots, requiredSpots) ||
                                     ParkInAvailableSpots(compactSpots, requiredSpots) ||
                                     ParkInAvailableSpots(largeSpots, requiredSpots);
            }
            else if (vehicle is Car)
            {
                parkedSuccessfully = ParkInAvailableSpots(compactSpots, requiredSpots) ||
                                     ParkInAvailableSpots(largeSpots, requiredSpots);
            }
            else if (vehicle is Van)
            {
                parkedSuccessfully = ParkInAvailableSpots(largeSpots, requiredSpots);
            }

            return parkedSuccessfully;
        }

        private bool ParkInAvailableSpots(List<ParkingSpotService> spots, int requiredSpots)
        {
            var parkedSuccessfully = false;

            var availableSpots = 0;

            for (var i = 0; i < spots.Count; i++)
            {
                if (!spots[i].IsOccupied)
                {
                    availableSpots++;
                }
                else
                {
                    availableSpots = 0;
                }

                if (availableSpots == requiredSpots)
                {
                    // Mark spot as occupied
                    for (var j = i; j > i - requiredSpots; j--)
                    {
                        spots[j].Park();
                    }
                    return true;
                }
            }

            return parkedSuccessfully;
        }

        public void LeaveVehicle(Vehicle vehicle)
        {
            var requiredSpots = vehicle.GetRequiredSpots();

            if (vehicle is MotorCycle)
            {
                LeaveSpots(motorcycleSpots, requiredSpots);
            }
            else if (vehicle is Car)
            {
                LeaveSpots(compactSpots, requiredSpots);
            }
            else if (vehicle is Van)
            {
                LeaveSpots(largeSpots, requiredSpots);
            }
        }

        private void LeaveSpots(List<ParkingSpotService> spots, int requiredSpots)
        {
            var count = 0;

            foreach (var spot in spots)
            {
                if (spot.IsOccupied)
                {
                    spot.Leave();
                    count++;

                    if (count == requiredSpots)
                    {
                        break;
                    }
                }
            }
        }
    }
}
