using System.Collections.Generic;

namespace BusStopSimulation.Models
{
    public class BusStopSimulationResponseDto
    {
        public string SimulationName { get; set; } = string.Empty;
        public List<string>? SimulationResults { get; set; }
    }
}
