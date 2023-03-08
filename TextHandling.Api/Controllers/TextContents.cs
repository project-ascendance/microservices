using System.Text.Json;
using Ascendance.Models;
using Microsoft.AspNetCore.Mvc;
using TextHandling.Api.Services.Contracts;

namespace TextHandling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextContents : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        public TextContents(IDataHandler handler)
        {
            _dataHandler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> PostTextContentAsync([FromBody]string jsonPostString)
        {
            var result = JsonSerializer.Deserialize<TextContent>(jsonPostString);

            if (result == null) { return BadRequest(result); }

            try
            {
                await _dataHandler.SaveTextContentAsync(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTextContentAsync(int id)
        {
            try
            {
                return Ok(await _dataHandler.GetTextContentAsync(id));
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                {
                    return NotFound(e.Message);
                }

                return Conflict(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutTextContentAsync([FromBody] string jsonPutString)
        {
            var result = JsonSerializer.Deserialize<TextContent>(jsonPutString);
            if (result == null) { return BadRequest(result); }

            try
            {
                await _dataHandler.UpdateTextContentAsync(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTextContentAsync(int id)
        {
            try
            {
                await _dataHandler.RemoveTextContentAsync(id);
                return Ok("Item deleted successfully");
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                {
                    return NotFound(e.Message);
                }

                return Conflict(e.Message);
            }
        }
    }
}
