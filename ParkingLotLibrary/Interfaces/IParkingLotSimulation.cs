using AppServiceCore;
using ParkingLotLibrary.Models;

namespace ParkingLotLibrary.Interfaces
{
    public interface IParkingLotSimulation
    {
        CommandResult<ParkingLotSimulationResponseDto> RunSimulation();
    }
}
