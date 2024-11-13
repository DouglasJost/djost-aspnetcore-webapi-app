using System.Collections.Generic;

namespace ParkingLotLibrary.Models
{
    public class ParkingLotSimulationResponseDto
    {
        public string SimulationName { get; set; } = string.Empty;
        public List<string>? SimulationResults { get; set; } 
    }
}
