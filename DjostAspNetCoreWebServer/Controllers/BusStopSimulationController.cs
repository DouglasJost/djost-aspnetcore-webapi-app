using Asp.Versioning;
using BusStopSimulation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    [Authorize]
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
