using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [Route("api/gamesession")]
    [ApiController]
    public class GameSessionController : Controller
    {
        private readonly MazeGameContext _context;

        public GameSessionController(MazeGameContext context)
        {
            _context = context;
        }

        //Get by GameSessionId, MazeId
        [HttpGet("get/all/by-mazeid/{mazeID}")]
        public List<GameSession> GetByMazeID([FromRoute] int mazeID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            return gameSessionRepository.GetGameSessionsByMazeID(mazeID);
        }

        //get by GameSessionId, PlayerId
        [HttpGet("get/all/by-playerid/{playerID}")]
        public List<GameSession> GetByPlayerID([FromRoute] int playerID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            return gameSessionRepository.GetGameSessionsByPlayerID(playerID);
        }

        //get all game sessions
        [HttpGet("get/all")]
        public List<GameSession> GetAllGameSessions()
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            return gameSessionRepository.GetAllGameSessions();
        }

        //get by GameSessionId
        [HttpGet("get/by-id/{gameSessionID}")]
        public GameSession? GetByID([FromRoute] int gameSessionID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            return gameSessionRepository.GetGameSessionByID(gameSessionID);
        }

        //Create a new game session
        [HttpPost("post/{mazeID},{playerID}")]
        public void Post([FromRoute] int mazeID, [FromRoute] int playerID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            gameSessionRepository.CreateGameSession(mazeID, playerID);
        }

        //Update an existing game session
        [HttpPut("put/{gameSessionID},{mazeID},{playerID}")]
        public void Put([FromRoute] int gameSessionID, [FromRoute] int mazeID, [FromRoute] int playerID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            gameSessionRepository.UpdateGameSession(gameSessionID, mazeID, playerID);
        }

        //Delete a game session by GameSessionId
        [HttpDelete("delete/by-id/{gameSessionID}")]
        public void Delete([FromRoute] int gameSessionID)
        {
            GameSessionRepository gameSessionRepository = new GameSessionRepository(_context);
            gameSessionRepository.DeleteGameSession(gameSessionID);
        }
    }
}
