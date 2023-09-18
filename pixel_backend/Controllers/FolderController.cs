using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _service;

        public FolderController(IFolderService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(FolderDto request)
        {
            var result = _service.Create(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(FolderDto request)
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
        public IActionResult Action(FolderDto request)
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
        public IActionResult All(FolderDto request)
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
        public IActionResult One(FolderDto request)
        {
            var result = _service.One(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("subfolder")]
        public IActionResult Subfolder(FolderDto request)
        {
            var result = _service.Subfolder(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
