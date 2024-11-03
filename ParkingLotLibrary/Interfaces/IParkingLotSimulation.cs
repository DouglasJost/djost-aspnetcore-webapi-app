using AppServiceCore;
using AppServiceCore.Models.AssessmentSuite;
using ParkingLotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotLibrary.Interfaces
{
    public interface IParkingLotSimulation
    {
        CommandResult<ParkingLotSimulationResponseDto> RunSimulation();
    }
}
