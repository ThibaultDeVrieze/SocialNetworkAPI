using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locRepository;
        public LocationsController(ILocationRepository repo)
        {
            _locRepository = repo;
        }

        [HttpGet]
        public IEnumerable<Location> GetAll()
        {
            return _locRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetBy(int id)
        {
            Location loc = _locRepository.GetBy(id);
            if (loc == null) return NotFound();
            return loc;
        }

        [HttpPost]
        public ActionResult<Location> PostLocation(LocationDTO dto)
        {
            Location loc = new Location { City = dto.City, Country = dto.Country, HouseNr = dto.HouseNr, Postalcode = dto.Postalcode, Street = dto.Street };
            _locRepository.Add(loc);
            _locRepository.SaveChanges();
            return CreatedAtAction(nameof(GetBy),
                new { id = loc.LocationID }, loc);
        }
    }
}
