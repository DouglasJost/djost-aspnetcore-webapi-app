using ParkingLotLibrary.Models.Enums;

namespace ParkingLotLibrary.Models.Vehicles
{
    public class MotorCycle : Vehicle
    {
        public override int GetRequiredSpots()
        {
            return 1;
        }

        public override SpotType GetSpotType()
        {
            return SpotType.Motorcycle;
        }
    }
}
