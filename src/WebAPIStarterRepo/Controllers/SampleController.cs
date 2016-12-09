using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using StarterAPI.Models;
using StarterAPI.Services;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace StarterAPI.Controllers
{
    [Route("api/[controller]")]
    public class SampleController : Controller
    {
        private readonly ISampleService _service;
        private readonly ILogger<SampleController> _logger;

        public SampleController(ISampleService service, ILogger<SampleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Sample
        [HttpGet]
        public IActionResult Get([FromQuery] string filter, [FromQuery] int? count)
        {
            if (!String.IsNullOrEmpty(filter) && (count != null))
            {
                return Filter(filter, count.GetValueOrDefault());
            }
            if (!String.IsNullOrEmpty(filter))
            {
                return Filter(filter);
            }
            else
            {
                return Ok("Unsupported");
            }
        }

        // GET api/Sample/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Sample result = _service.GetByID(id);

            return new ObjectResult(result);
        }

        /** FILTER OPTIONS **/
        /**  /Sample/?filter=FieldName.Contains("9999") **/
        /**  /Sample/?filter=insertDate gt "12/01/2015" and insertDate lt "12/02/2015"&count=10 **/

        public IActionResult Filter(string filter)
        {
            IQueryable<Sample> result = _service.Find(filter);

            return new ObjectResult(result);
        }

        public IActionResult Filter(string filter, int count)
        {
            IQueryable<Sample> result = _service.Find(filter, count);

            return new ObjectResult(result);
        }

        // POST api/Sample
        [HttpPost]
        public IActionResult Post([FromBody]Sample sample)
        {
            try
            {
                if (sample == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.Add(sample);

                return Ok(sample);
            }
            catch (Exception exception)
            {
                _logger.LogError(1000, "Controller Error: " + exception);
                return BadRequest(exception);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Sample> patch)
        {
            var sample = _service.GetByID(id);

            if (sample == null)
            {
                return NotFound();
            }

            patch.ApplyTo(sample, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Update(sample);

            return Ok();
        }

        // PUT: api/Sample/5
        [HttpPut("{id}")]
        public IActionResult PutSample([FromRoute] int id, [FromBody] Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sample.SampleID)
            {
                return BadRequest();
            }

            _service.Update(sample);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (_service.DeleteByID(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Unable to delete");
            }
        }
    }
}
