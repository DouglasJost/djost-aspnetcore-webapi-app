using ParkingLotLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
