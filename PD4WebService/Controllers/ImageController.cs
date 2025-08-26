using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly MazeGameContext _context;

        private ImageRepository _imageRepository;

        public ImageController(MazeGameContext context)
        {
            _context = context;
            _imageRepository = new ImageRepository(_context);
        }

        //Get by imageID
        [HttpGet("get/by-id/{imageID}")]
        public Image? GetByID([FromRoute] int imageID)
        {
            Image? image = _imageRepository.GetByID(imageID);
            if (image != null)
            {
                return image; // Return the image found with the given ID
            }
            return null; // No image found with the given ID
        }

        //get by imageName
        [HttpGet("get/by-name/{imageName}")]
        public Image? GetByName([FromRoute] string imageName)
        {
            List<Image> images = _imageRepository.GetByName(imageName);
            if (images.Count > 0)
            {
                return images[0]; // Return the first image found with the given name
            }
            return null; // No image found with the given name
        }
        //create, update, delete image
        [HttpPost("post/{imageName},{imageLink}")]
        public void Post([FromRoute] string imageName, [FromRoute] string imageLink)
        {
            _imageRepository.AddNewImage(imageName, imageLink);
        }

        [HttpPut("put/by-id/{imageID}/{imageName},{imageLink}")]
        public void Put([FromRoute] int imageID, [FromRoute] string imageName, [FromRoute] string imageLink)
        {
            _imageRepository.UpdateByID(imageID, imageName, imageLink);
        }

        //update by imageName
        [HttpPut("put/by-name/{imageName},{imageLink}")]
        public void PutByName([FromRoute] string imageName, [FromRoute] string imageLink)
        {
            _imageRepository.UpdateByName(imageName, imageLink);
        }

        [HttpDelete("delete/by-id/{imageID}")]
        public void Delete([FromRoute] int imageID)
        {
            _imageRepository.DeleteByID(imageID);
        }

        //delete by imageName
        [HttpDelete("delete/by-name/{imageName}")]
        public void DeleteByName([FromRoute] string imageName)
        {
            _imageRepository.DeleteByName(imageName);
        }
    }
}
