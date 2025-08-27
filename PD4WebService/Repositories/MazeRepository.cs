using Microsoft.EntityFrameworkCore;
using PD4ExamAPI.Models;
using System.Diagnostics;

namespace PD4ExamAPI.Repositories
{
    public class MazeRepository : RepositoryBaseClass
    {
        private readonly MazeGameContext _context;
        private MazeTileRepository _mazeTileRepository;

        public MazeRepository(MazeGameContext context) : base(context)
        {
            _context = context;
            _mazeTileRepository = new MazeTileRepository(context);
        }

        public Maze? GetMazeByID(int mazeID)
        {
            Maze maze = _context.Mazes
                .Include(e => e.MazeTiles)
                .FirstOrDefault(e => e.MazeId == mazeID);
            //Debug.WriteLineIf() if the maze or maze.MazeTiles is null
            Debug.WriteLineIf(maze == null, $"Maze with ID {mazeID} not found.");
            Debug.WriteLineIf(maze != null && maze.MazeTiles == null, $"Could not get tiles for maze with ID {mazeID}.");

            return maze;
        }

        public Maze? GetMazeByName(string name)
        {
            Maze maze = _context.Mazes
                .Include(e => e.MazeTiles)
                .FirstOrDefault(e => e.Name == name);

            return maze;
        }

        public void GenerateMaze(string name, int width, int height)
        {
            ////find any with the same name, then delete the old one
            //Maze? existingMaze = GetMazeByName(name);
            //if (existingMaze != null)
            //{
            //    DeleteMaze(existingMaze.MazeId);
            //}

            Maze newMaze = new()
            {
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Name = name,
                Density = 0,
                MazeTiles = new List<MazeTile>(),
                MazeId = _context.Mazes.Any() ? _context.Mazes.Max(m => m.MazeId) + 1 : 1
            };

            _context.Add<Maze>(newMaze);
            _context.SaveChanges(); // Ensure MazeId is set

            // Now generate and add tiles
            ICollection<MazeTile> tiles = GenerateMazeTiles(width, height, newMaze);
            newMaze.MazeTiles = tiles;

            // AddRange is more efficient and ensures tracking
            _context.MazeTiles.AddRange(tiles);
            _context.SaveChanges();
        }

        private ICollection<MazeTile> GenerateMazeTiles(int width, int height, Maze newMaze)
        {
            int lastTileID = _context.MazeTiles.Any() ? _context.MazeTiles.Max(t => t.TileId) + 1 : 1;

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    MazeTile tile;

                    if (x == 0 || x == height - 1 || y == 0 || y == width - 1)
                    {
                        double density = 100; //currently hardcoded as 100
                        tile = _mazeTileRepository.GenerateTile(x, y, "W", density, newMaze.MazeId);
                    }
                    else
                    {
                        //Comment this and uncomment the random part to have random walls
                        tile = _mazeTileRepository.GenerateTile(x, y, "T", 0, newMaze.MazeId);
                    }
                    //TileId = _context.MazeTiles.Any() ? _context.MazeTiles.Max(t => t.TileId) + 1 : 1, // Ensure unique TileId
                    //Console.WriteLine($"CWL: Tile {tile.TileId} generated: {tile.RowIndex} x {tile.ColumnIndex}");
                    //Debug.WriteLine($"DWL: Tile {tile.TileId} generated: {tile.RowIndex} x {tile.ColumnIndex}");
                    tile.TileId = lastTileID++;
                    newMaze.MazeTiles.Add(tile);
                    _context.Add(tile);
                }
            }
            return newMaze.MazeTiles;
        }

        //create a new maze with originalMazeID
        public void GenerateMaze(string name, int originalMazeID)
        {
            //find any with the same name, then delete the old one
            Maze? existingMaze = GetMazeByName(name);
            if (existingMaze != null)
            {
                DeleteMaze(existingMaze.MazeId);
            }
            Maze? originalMaze = GetMazeByID(originalMazeID);
            if (originalMaze == null)
            {
                throw new ArgumentException("Original maze not found.");
            }
            Maze newMaze = new Maze()
            {
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                Name = name,
                Density = originalMaze.Density,
                OriginalMazeId = originalMaze.MazeId
            };

            //make sure maze ID is unique
            newMaze.MazeId = _context.Mazes.Any() ? _context.Mazes.Max(m => m.MazeId) + 1 : 1;
            _context.Add<Maze>(newMaze);
            _context.SaveChanges();
        }

        //delete maze by id, and delete all tiles associated with it
        public void DeleteMaze(int mazeID)
        {
            Maze? mazeToRemove = GetMazeByID(mazeID);
            if (mazeToRemove != null)
            {
                _context.RemoveRange(mazeToRemove.MazeTiles);
                _context.Remove(mazeToRemove);
                _context.SaveChanges();
            }
        }
    }
}
