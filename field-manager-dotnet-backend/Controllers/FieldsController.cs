using FieldManagerDotnetBackend.Models;
using FieldManagerDotnetBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagerDotnetBackend.Controllers
{
    /// <summary>
    /// Allows clients to list all available fields in the demo. Each field
    /// includes its identifier and name, which can be used to retrieve
    /// detailed recommendations via other endpoints.
    /// </summary>
    [ApiController]
    [Route("api/fields")]
    public class FieldsController : ControllerBase
    {
        private readonly DemoDataService _dataService;

        public FieldsController(DemoDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetFields()
        {
            var fields = _dataService.GetAllFields()
                .Select(f => new { f.Id, f.Name });
            return Ok(fields);
        }
    }
}