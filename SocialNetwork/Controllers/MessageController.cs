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
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageController(IMessageRepository repo, IUserRepository uRepo)
        {
            _messageRepository = repo;
            _userRepository = uRepo;
        }

        [HttpGet]
        public IEnumerable<Message> GetMessages()
        {
            return _messageRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Message> GetMessage(int id)
        {
            Message mess = _messageRepository.GetBy(id);
            if (mess == null) return NotFound();
            return mess;
        }

        [HttpPost]
        public ActionResult<Message> PostMessage(MessageDTO dto)
        {
            User user = _userRepository.GetByEmail(dto.User.Email);
            Image img = new Image(dto.Image.ImagePath, user);
            Message mess = new Message(dto.MessageText, user, img);
            _messageRepository.Add(mess);
            _messageRepository.SaveChanges();
            return CreatedAtAction(nameof(GetMessage),
                new { id = mess.MessageID }, mess);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            Message mess = _messageRepository.GetBy(id);
            if (mess == null) return NotFound();
            _messageRepository.Delete(mess);
            _messageRepository.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(int id, MessageDTO dto)
        {
            Message mess = _messageRepository.GetBy(id);
            if (mess == null) return NotFound();
            mess.MessageText = dto.MessageText;
            _messageRepository.Update(mess);
            _messageRepository.SaveChanges();
            return NoContent();
        }
    }
}
