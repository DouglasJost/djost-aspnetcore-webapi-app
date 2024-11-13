using ParkingLotLibrary.Models.Enums;

namespace ParkingLotLibrary.Models.Vehicles
{
    public abstract class Vehicle
    {
        public abstract int GetRequiredSpots();  // Number of spots required by the vehicle
        public abstract SpotType GetSpotType();  // Type of spot the vehicle can park in
    }
}
