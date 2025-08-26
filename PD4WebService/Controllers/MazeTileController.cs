using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [Route("api/maze-tile")]
    [ApiController]
    public class MazeTileController : Controller
    {
        private readonly MazeGameContext _context;
        
        private MazeTileRepository _mazeTileRepository;

        public MazeTileController(MazeGameContext context)
        {
            _context = context;
            _mazeTileRepository = new MazeTileRepository(_context);
        }


        [HttpGet("get/{mazeID}/{x},{y}")]
        public MazeTile? Get([FromRoute] int mazeID, [FromRoute] int x, [FromRoute] int y)
        {
            return _mazeTileRepository.GetFromMazeCoordinate(mazeID, x, y);
        }

        //get by maze name
        [HttpGet("get/all/{mazeID}")]
        public IEnumerable<MazeTile> GetAllFromMaze([FromRoute] int mazeID)
        {
            return _mazeTileRepository.GetAllFromMaze(mazeID);
        }

        //delete all tiles from mazeid
        [HttpDelete("delete/all/{mazeID}")]
        public void DeleteAllFromMaze([FromRoute] int mazeID)
        {
            IEnumerable<MazeTile> tiles = _mazeTileRepository.GetAllFromMaze(mazeID);
            foreach (MazeTile tile in tiles)
            {
                _mazeTileRepository.DeleteTile(mazeID, tile.ColumnIndex, tile.RowIndex);
            }
        }


        //post
        [HttpPost("post/{mazeID}/{x},{y},{type},{density}")]
        public MazeTile Post([FromRoute] int mazeID, [FromRoute] int x, [FromRoute] int y, [FromRoute] string type, [FromRoute] double density)
        {
            //check if the tile already exists
            MazeTile? existingTile = _mazeTileRepository.GetFromMazeCoordinate(mazeID, x, y);
            if (existingTile != null)
            {
                return existingTile; // Tile already exists, return it
            }
            //create a new tile
            MazeTile newTile = _mazeTileRepository.GenerateTile(x, y, type, density, mazeID);
            newTile.MazeId = mazeID;
            _context.Add(newTile);
            _context.SaveChanges();
            return newTile;
        }


        [HttpPut("put/type/{mazeID}/,{x},{y}/{tileType}")]
        public void Put([FromRoute] int mazeID, [FromRoute] string tileType, [FromRoute] int x, [FromRoute] int y)
        {
            _mazeTileRepository.SetTileType(x, y, mazeID, tileType);
        }

        [HttpPut("put/density/{mazeID}/,{x},{y}/{density}")]
        public void Put([FromRoute] int mazeID, [FromRoute] int x, [FromRoute] int y, [FromRoute] double density)
        {
            _mazeTileRepository.SetTileDensity(x, y, mazeID, density);
        }

        [HttpDelete("delete/{mazeID}/{x},{y}")]
        public void Delete([FromRoute] int mazeID, [FromRoute] int x, [FromRoute] int y)
        {
            //check if the tile exists before attempting to delete it
            MazeTile? tile = _mazeTileRepository.GetFromMazeCoordinate(mazeID, x, y);
            if (tile == null)
            {
                return; // Tile does not exist, no action needed
            }
            _mazeTileRepository.DeleteTile(mazeID, x, y);
        }
    }
}
