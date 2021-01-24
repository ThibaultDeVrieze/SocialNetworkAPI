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
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        public EventController(IEventRepository repo, IUserRepository uRepo)
        {
            _eventRepository = repo;
            _userRepository = uRepo;
        }

        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return _eventRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            Event ev = _eventRepository.GetBy(id);
            if (ev == null) return NotFound();
            return ev;
        }

        [HttpPost]
        public ActionResult<Event> PostEvent(EventDTO dto)
        {
            User user = _userRepository.GetByEmail(dto.Founder.Email);
            Location loc = new Location(dto.Location.Street, dto.Location.HouseNr, dto.Location.Postalcode, dto.Location.City, dto.Location.Country);
            Event ev = new Event(dto.EventName, dto.Date, user, loc, new Image(dto.Image.ImagePath, user), dto.Description);
            _eventRepository.Add(ev);
            _eventRepository.SaveChanges();
            return CreatedAtAction(nameof(GetEvent),
                new { id = ev.EventID }, ev);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            Event ev = _eventRepository.GetBy(id);
            if (ev == null) return NotFound();
            _eventRepository.Delete(ev);
            _eventRepository.SaveChanges();
            return NoContent();
        }
    }
}
