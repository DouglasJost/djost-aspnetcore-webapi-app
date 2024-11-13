using AppServiceCore;
using BusStopSimulation.Models;

namespace BusStopSimulation.Interfaces
{
    public interface IBusStopSimulation
    {
        CommandResult<BusStopSimulationResponseDto> RunSimulation();
    }
}
