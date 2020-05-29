using FitnessApp.Models;
using FitnessApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/check")]
    [ApiController]
    public class FitnessController : ControllerBase
    {
        private readonly IDataStoreClient dataStore;
        public FitnessController(IDataStoreClient data)
        {
            dataStore = data;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FitnessRecord>>> Get()
        {
            return await dataStore.GetAllRecords();
        }
        [HttpGet("{title}")]
        public async Task<ActionResult<FitnessRecord>> GetRecord(string title)
        {
            return await dataStore.GetRecordByTitle(title);
        }
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<FitnessRecord>>> GetRecordsByType(string type)
        {
            return await dataStore.GetRecordsByWorkoutType(type);
        }
        [HttpPost]
        public async Task<IActionResult> NewRecord([FromBody] FitnessRecord newRecord)
        {
            if(await dataStore.WriteRecord(newRecord))
            {
                return Ok("new record successfully written");
            }
            return StatusCode(400);
        }
        [HttpPatch("{title}/comments")]
        public async Task<IActionResult> UpdateComments(string title, [FromBody] string newComments)
        {
            if(await dataStore.UpdateRecord(title, newComments))
            {
                return Ok("record successfully updated");
            }
            return StatusCode(400);
        }
        [HttpDelete("{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            if(await dataStore.DeleteRecord(title))
            {
                return Ok("Successfully deleted");
            }
            return StatusCode(400);
        }
    }
}
