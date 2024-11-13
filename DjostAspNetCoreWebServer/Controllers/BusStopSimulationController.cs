using BusStopSimulation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BusStopSimulationController : ControllerBase
    {
        private readonly IBusStopSimulation _busStopSimulation;
        public BusStopSimulationController(IBusStopSimulation busStopSimulation)
        {
            _busStopSimulation = busStopSimulation;
        }

        [Route("RunSimulation")]
        [HttpGet]
        public IActionResult RunSimulation()
        {
            var response = _busStopSimulation.RunSimulation();
            return Ok(response);
        }
    }
}
