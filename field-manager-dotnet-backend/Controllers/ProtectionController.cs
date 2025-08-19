using FieldManagerDotnetBackend.Models;
using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FieldManagerDotnetBackend.Controllers
{
    /// <summary>
    /// Exposes risk assessments and crop protection recommendations. This
    /// corresponds to the "Crop Protection" feature in xarvio FIELD
    /// MANAGER, where growers receive early alerts about disease and
    /// insect risks and variable-rate application suggestions.
    /// </summary>
    [ApiController]
    [Route("api/fields/{fieldId:guid}/protection")]
    public class ProtectionController : ControllerBase
    {
        private readonly DemoDataService _dataService;

        public ProtectionController(DemoDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<ProtectionRecommendation> GetProtection(Guid fieldId)
        {
            var field = _dataService.GetField(fieldId);
            if (field is null || field.ProtectionRecommendation is null)
            {
                return NotFound();
            }
            return Ok(field.ProtectionRecommendation);
        }
    }
}