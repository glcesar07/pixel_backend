using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(PersonDto request)
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
        public IActionResult Update(PersonDto request)
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
        public IActionResult Action(PersonDto request)
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
        public IActionResult All(PersonDto request)
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
        public IActionResult One(PersonDto request)
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
        public IActionResult Label(PersonDto request)
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
