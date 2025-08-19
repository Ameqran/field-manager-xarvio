using FieldManagerDotnetBackend.Models;
using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FieldManagerDotnetBackend.Controllers
{
    /// <summary>
    /// Exposes seeding recommendations for a field, representing the
    /// variable rate seeding maps in xarvio FIELD MANAGER. Each field
    /// returns both an average density and a zone-specific rate map.
    /// </summary>
    [ApiController]
    [Route("api/fields/{fieldId:guid}/seeding")]
    public class SeedingController : ControllerBase
    {
        private readonly DemoDataService _dataService;

        public SeedingController(DemoDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<SeedingRecommendation> GetSeeding(Guid fieldId)
        {
            var field = _dataService.GetField(fieldId);
            if (field is null || field.SeedingRecommendation is null)
            {
                return NotFound();
            }
            return Ok(field.SeedingRecommendation);
        }
    }
}