using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [Route("api/maze")]
    [ApiController]
    public class MazeController : Controller
    {
        private readonly MazeGameContext _context;

        private MazeRepository _mazeRepository;

        public MazeController(MazeGameContext context)
        {
            _context = context;
            _mazeRepository = new MazeRepository(_context);
        }

        [HttpGet("get/by-id/{mazeID}")]
        public Maze? GetByID([FromRoute] int mazeID)
        {
            return _mazeRepository.GetMazeByID(mazeID);
        }

        [HttpGet("get/by-name/{name}")]
        public Maze? GetByName([FromRoute] string name)
        {
            return _mazeRepository.GetMazeByName(name);
        }

        [HttpPost("post/{name},{width},{height}")]
        public void Post([FromRoute] string name, [FromRoute] int width, [FromRoute] int height)
        {
            _mazeRepository.GenerateMaze(name, width, height);
        }


        [HttpPost("post/secondary-maze/{name},{originalMazeID}")]
        public void Post([FromRoute] string name, [FromRoute] int originalMazeID)
        {
            // This method is not used in the current implementation, but can be used to generate a maze based on an existing one.
            _mazeRepository.GenerateMaze(name,  originalMazeID);
        }

        [HttpPut("put/{name},{width},{height}")]
        public void Put([FromRoute] string name, [FromRoute] int width, [FromRoute] int height)
        {
            _mazeRepository.GenerateMaze(name, width, height);
        }

        [HttpDelete("delete/by-id/{mazeID}")]
        public void Delete([FromRoute] int mazeID)
        {
            _mazeRepository.DeleteMaze(mazeID);
        }
    }
}
