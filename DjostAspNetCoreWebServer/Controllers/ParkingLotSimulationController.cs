using Microsoft.AspNetCore.Mvc;
using ParkingLotLibrary.Interfaces;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParkingLotSimulationController : ControllerBase
    {
        private readonly IParkingLotSimulation _parkingLotSimulation;

        public ParkingLotSimulationController(IParkingLotSimulation parkingLotSimulation)
        {
            _parkingLotSimulation = parkingLotSimulation;
        }

        [Route("RunSimulation")]
        [HttpGet]
        public IActionResult RunSimulation()
        {
            var response = _parkingLotSimulation.RunSimulation();
            return Ok(response);
        }
    }
}
