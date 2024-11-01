using ParkingLotLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotLibrary.Models.Vehicles
{
    public abstract class Vehicle
    {
        public abstract int GetRequiredSpots();  // Number of spots required by the vehicle
        public abstract SpotType GetSpotType();  // Type of spot the vehicle can park in
    }
}
