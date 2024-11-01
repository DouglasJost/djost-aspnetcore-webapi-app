using ParkingLotLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotLibrary.Models.Vehicles
{
    public class Car : Vehicle
    {
        public override int GetRequiredSpots()
        {
            return 1;
        }

        public override SpotType GetSpotType()
        {
            return SpotType.Compact;
        }
    }
}
