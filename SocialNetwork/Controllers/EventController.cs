using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        public EventController(IEventRepository repo, IUserRepository uRepo, IMessageRepository mRepo)
        {
            _eventRepository = repo;
            _userRepository = uRepo;
            _messageRepository = mRepo;
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
            User user = _userRepository.GetBy(dto.FounderID);
            Location loc = new Location(dto.Location.Street="", dto.Location.HouseNr=0, dto.Location.Postalcode=0, dto.Location.City="", dto.Location.Country="Belgium");
            Event ev;
            if (dto.Image == null)
            {
                ev = new Event(dto.EventName, dto.Date, user, loc, null, dto.Description="");
            }
            else
            {
                ev = new Event(dto.EventName, dto.Date, user, loc, new Image(dto.Image.ImagePath, user), dto.Description="");
            }
            _eventRepository.Add(ev);
            _eventRepository.SaveChanges();
            return CreatedAtAction(nameof(GetEvent),
                new { id = ev.EventID }, ev);
        }

        [HttpPost("message")]
        public ActionResult<Event> PostMessageToEvent(int id, MessageDTO dto)
        {
            Event ev = _eventRepository.GetBy(id);
            if (ev == null) return NotFound();
            User user = _userRepository.GetBy(dto.UserID);
            Message mess;
            if(dto.Image == null)
            {
                mess = new Message(dto.MessageText, user);
            }
            else
            {
                Image img = new Image(dto.Image.ImagePath, user);
                mess = new Message(dto.MessageText, user, img);
            }
            _messageRepository.Add(mess);
            ev.PostMessage(mess);
            _messageRepository.SaveChanges();
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
