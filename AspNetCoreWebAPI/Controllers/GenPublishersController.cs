using AspNetCoreWebAPI.Entities;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/genpublishers")]
    public class GenPublishersController : Controller
    {
        IGenericEFRepository _rep;

        public GenPublishersController(IGenericEFRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var item = _rep.Get<Publisher>();
            var DTOs = Mapper.Map<IEnumerable<PublisherDTO>>(item);
            return Ok(DTOs);
        }

        [HttpGet("{id}", Name = "GetGenericPublisher")]
        public IActionResult Get(int id, bool includeRelatedEntities = false)
        {
            var item = _rep.Get<Publisher>(id, includeRelatedEntities);

            if (item == null) return NotFound();

            var DTO = Mapper.Map<PublisherDTO>(item);
            return Ok(DTO);
        }

    }
}
