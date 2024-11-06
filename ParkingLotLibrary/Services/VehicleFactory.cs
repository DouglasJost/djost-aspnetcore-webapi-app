using ParkingLotLibrary.Models.Enums;
using ParkingLotLibrary.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotLibrary.Services
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(VehicleType vehicleType)
        {
            //var motorcycle = new MotorCycle();
            //var car = new Car();
            //var van = new Van();

            Vehicle requestedVehicle;
            switch (vehicleType)
            {
                case VehicleType.Motocycle:
                    requestedVehicle = new MotorCycle();
                    break;

                case VehicleType.Car:
                    requestedVehicle = new Car();
                    break;

                case VehicleType.Van:
                    requestedVehicle = new Van();
                    break;

                default:
                    throw new ArgumentException("Invalid vehicle type");
            }

            return requestedVehicle;
        }
    }
}
