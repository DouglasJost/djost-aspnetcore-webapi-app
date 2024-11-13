using ParkingLotLibrary.Models.Enums;
using ParkingLotLibrary.Models.Vehicles;

namespace ParkingLotLibrary.Services
{
    public class ParkingSpotService
    {
        public SpotType Type { get; private set; }
        public bool IsOccupied { get; private set; }

        public ParkingSpotService(SpotType type)
        {
            Type = type;
            IsOccupied = false;
        }

        public void Park()
        {
            IsOccupied = true;
        }

        public void Leave()
        {
            IsOccupied = false;
        }

        public bool CanFitVehicle(Vehicle vehicle)
        {
            return !IsOccupied && (vehicle.GetSpotType() <= Type);
        }
    }
}
