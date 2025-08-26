using PD4ExamAPI.Models;

namespace PD4ExamAPI.Repositories
{
    public class MazeTileRepository : RepositoryBaseClass
    {

        public MazeTileRepository(MazeGameContext context) : base(context)
        {

        }
        
        public MazeTile? GetFromMazeCoordinate(int mazeID, int x, int y)
        {
            MazeTile? tile = _context.MazeTiles
                .FirstOrDefault(e => e.MazeId == mazeID && e.ColumnIndex == x && e.RowIndex == y);
            return tile;
        }

        public IEnumerable<MazeTile> GetAllFromMaze(int mazeID)
        {
            MazeTile[] tiles = _context.MazeTiles
                .Where(e => e.MazeId == mazeID)
                .ToArray();
            return tiles;
        }

        public MazeTile GenerateTile(int x, int y, string type, double density, int mazeID)
        {

            //return new MazeTile() { ColumnIndex = x, RowIndex = y, TileType = "T", Maze = maze };

            return new MazeTile
            {
                TileId = _context.MazeTiles.Any() ? _context.MazeTiles.Max(t => t.TileId) + 1 : 1, // Ensure unique TileId
                ColumnIndex = x,
                RowIndex = y,
                TileType = type,
                DensityFallOff = density,
                MazeId = mazeID
            };
        }

        public void SetTileType(int x, int y, int mazeID, string tileType)
        {
            MazeTile tile = GetFromMazeCoordinate(mazeID, x, y);

            tile.TileType = tileType;

            _context.SaveChanges();
        }


        public void SetTileDensity(int x, int y, int mazeID, double density)
        {
            MazeTile tile = GetFromMazeCoordinate(mazeID, x, y);

            tile.DensityFallOff = density;


            _context.SaveChanges();
        }

        public void DeleteTile(int mazeID, int x, int y)
        {
            MazeTile tileToRemove = GetFromMazeCoordinate(mazeID, x, y);
            _context.Remove(tileToRemove);
            _context.SaveChanges();
        }
    }
}
