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
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUserRepository _userRepository;
        public ImageController(IImageRepository repo, IUserRepository uRepo)
        {
            _imageRepository = repo;
            _userRepository = uRepo;
        }

        [HttpGet]
        public IEnumerable<Image> GetImages()
        {
            return _imageRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            Image img = _imageRepository.GetBy(id);
            if (img == null) return NotFound();
            return img;
        }

        [HttpPost]
        public ActionResult<Image> PostImage(ImageDTO dto)
        {
            User user = _userRepository.GetBy(dto.UserID);
            Image img = new Image(dto.ImagePath, user);
            _imageRepository.Add(img);
            _imageRepository.SaveChanges();
            return CreatedAtAction(nameof(GetImage),
                new { id = img.ImageID }, img);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            Image img = _imageRepository.GetBy(id);
            if (img == null) return NotFound();
            _imageRepository.Delete(img);
            _imageRepository.SaveChanges();
            return NoContent();
        }
    }
}
