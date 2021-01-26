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
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IEventRepository _eventRepository;
        public UsersController(IUserRepository repo, IMessageRepository mRepo, IImageRepository imgRepo, IEventRepository eRepo)
        {
            _userRepository = repo;
            _messageRepository = mRepo;
            _imageRepository = imgRepo;
            _eventRepository = eRepo;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpGet("{id}/messages")]
        public IEnumerable<Message> GetMessagesForUser(int id)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return (IEnumerable<Message>)NotFound();
            return _messageRepository.GetAll().Where(m => m.User == user);
        }

        [HttpGet("{id}/images")]
        public IEnumerable<Image> GetImagesForUser(int id)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return (IEnumerable<Image>)NotFound();
            return _imageRepository.GetAll().Where(img => img.User == user);
        }

        [HttpGet("{id}/events")]
        public IEnumerable<Event> GetEventsForUser(int id)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return (IEnumerable<Event>)NotFound();

            List<Event> events = new List<Event>();
            foreach (var ev in _eventRepository.GetAll())
            {
                foreach(var ue in ev.UsersGoing)
                {
                    if (ue.User == user)
                    {
                        events.Add(ev);
                    }
                }
            }

            return events;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO dto)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return NotFound();
            user.DateOfBirth = dto.DateOfBirth;
            user.Description = dto.Description;
            user.Email = dto.Email;
            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.LinkedInURL = dto.LinkedInURL;
            Location loc = new Location(dto.Location.Street, dto.Location.HouseNr, dto.Location.Postalcode, dto.Location.City, dto.Location.Country);
            user.Location = loc;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}/ChangeApproved")]
        public IActionResult ApproveUser(int id, bool approve)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return NotFound();
            user.Approved = approve;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}/ChangeDarkMode")]
        public IActionResult ChangeDarkMode(int id, bool mode)
        {
            User user = _userRepository.GetBy(id);
            if (user == null) return NotFound();
            user.DarkMode = mode;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public ActionResult<User> PostUser(UserDTO dto)
        {
            Location loc = new Location(dto.Location.Street, dto.Location.HouseNr, dto.Location.Postalcode, dto.Location.City, dto.Location.Country);
            User user = new User(dto.Firstname, dto.Lastname, dto.DateOfBirth, dto.Email, loc, dto.LinkedInURL, dto.Description);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(GetUser),
                new { id = user.UserID }, user);
        }
    }
}
