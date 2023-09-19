using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private readonly IOcrService _service;

        public OcrController(IOcrService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("by-pages")]
        public IActionResult Create(OcrDto request)
        {
            var result = _service.OCR(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
