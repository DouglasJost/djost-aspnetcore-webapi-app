using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotLibrary.Models
{
    public class ParkingLotSimulationResponseDto
    {
        public string SimulationName { get; set; } = string.Empty;
        public List<string>? SimulationResults { get; set; } 
    }
}
