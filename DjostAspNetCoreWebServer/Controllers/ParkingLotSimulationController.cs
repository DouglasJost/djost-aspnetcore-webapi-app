using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotLibrary.Interfaces;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    [Authorize]
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
