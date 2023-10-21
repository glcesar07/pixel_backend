using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;

        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(DocumentDto request)
        {
            var result = _service.Create(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("create-file")]
        public IActionResult CreateWhiteFile(DocumentDto request)
        {
            var result = _service.CreateWhiteFile(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(DocumentDto request)
        {
            var result = _service.Update(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("action")]
        public IActionResult Action(DocumentDto request)
        {
            var result = _service.Action(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public IActionResult All(DocumentDto request)
        {
            var result = _service.All(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("one")]
        public IActionResult One(DocumentDto request)
        {
            var result = _service.One(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("search")]
        public IActionResult Search(DocumentDto request)
        {
            var result = _service.Search(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
