using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintDocumentController : ControllerBase
    {
        private readonly IPrintDocumentService _service;

        public PrintDocumentController(IPrintDocumentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("htmltopdf")]
        public IActionResult GeneratePdfFromHtml(JObject request)
        {
            var result = _service.PrintHtmlToPDF(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
