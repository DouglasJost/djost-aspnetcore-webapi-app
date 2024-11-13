using ParkingLotLibrary.Models.Enums;

namespace ParkingLotLibrary.Models.Vehicles
{
    public class Van : Vehicle
    {
        public override int GetRequiredSpots()
        {
            return 3;
        }

        public override SpotType GetSpotType()
        {
            return SpotType.Large;
        }
    }
}
