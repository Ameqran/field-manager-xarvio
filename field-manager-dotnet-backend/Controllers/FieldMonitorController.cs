using FieldManagerDotnetBackend.Models;
using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FieldManagerDotnetBackend.Controllers
{
    /// <summary>
    /// Provides endpoints for retrieving field zone information such as
    /// biomass indices, weather forecasts and soil properties. This
    /// corresponds to the "Field Monitor" feature in xarvio FIELD
    /// MANAGER.
    /// </summary>
    [ApiController]
    [Route("api/fields/{fieldId:guid}/monitor")]
    public class FieldMonitorController : ControllerBase
    {
        private readonly DemoDataService _dataService;

        public FieldMonitorController(DemoDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Retrieves zone data for a specified field. Returns 404 if no
        /// matching field exists.
        /// </summary>
        /// <param name="fieldId">Identifier of the field</param>
        /// <returns>ZoneData representing biomass and weather information</returns>
        [HttpGet]
        public ActionResult<ZoneData> GetFieldMonitor(Guid fieldId)
        {
            var field = _dataService.GetField(fieldId);
            if (field is null || field.ZoneData is null)
            {
                return NotFound();
            }
            return Ok(field.ZoneData);
        }
    }
}