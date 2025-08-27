using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/player")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly MazeGameContext _context;
        
        private PlayerRepository _playerRepository;

        public PlayerController(MazeGameContext context)
        {
            _context = context;
            _playerRepository = new PlayerRepository(_context);
        }

        [HttpGet("get/{playerID}")]
        [EnableCors("AllowAll")]
        public Player? Get([FromRoute] int playerID)
        {
            return _playerRepository.GetById(playerID);
        }

        [HttpPost("post/{name}")]
        [EnableCors("AllowAll")]
        public void Post([FromRoute] string name)
        {
            _playerRepository.AddNewPlayer(name);
        }

        //put
        [HttpPut("put/{playerID}/{name}")]
        [EnableCors("AllowAll")]
        public void Put([FromRoute] int playerID, [FromRoute] string name)
        {
            _playerRepository.ChangePlayerByID(playerID, name);
        }


        [HttpDelete("delete/{playerID}")]
        [EnableCors("AllowAll")]
        public void Delete([FromRoute] int playerID)
        {
            if(playerID == 0)
            {
                return;
            }
            _playerRepository.DeletePlayer(playerID);
        }
    }
}
