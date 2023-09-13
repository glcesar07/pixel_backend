using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueService _service;

        public CatalogueController(ICatalogueService service)
        {
            _service = service;
        }

        [HttpPost]        
        [Route("create")]
        public IActionResult Create(CatalogueDto request)
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
        public IActionResult Update(CatalogueDto request)
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
        public IActionResult Action(CatalogueDto request)
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
        public IActionResult All(CatalogueDto request)
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
        public IActionResult One(CatalogueDto request)
        {
            var result = _service.One(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("label")]
        public IActionResult Label(CatalogueDto request)
        {
            var result = _service.Label(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
