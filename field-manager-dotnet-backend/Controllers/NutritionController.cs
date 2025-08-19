using FieldManagerDotnetBackend.Models;
using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FieldManagerDotnetBackend.Controllers
{
    /// <summary>
    /// Provides nutrient recommendations and fertilizer suggestions for a
    /// field. This mimics the "Crop Nutrition" feature of xarvio FIELD
    /// MANAGER, where users can create field-zone specific application
    /// maps for nutrients.
    /// </summary>
    [ApiController]
    [Route("api/fields/{fieldId:guid}/nutrition")]
    public class NutritionController : ControllerBase
    {
        private readonly DemoDataService _dataService;

        public NutritionController(DemoDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<NutritionRecommendation> GetNutrition(Guid fieldId)
        {
            var field = _dataService.GetField(fieldId);
            if (field is null || field.NutritionRecommendation is null)
            {
                return NotFound();
            }
            return Ok(field.NutritionRecommendation);
        }
    }
}