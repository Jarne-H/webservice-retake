using PD4ExamAPI.Models;

namespace PD4ExamAPI.Repositories
{
    public class PlayerRepository : RepositoryBaseClass
    {
        public PlayerRepository(MazeGameContext context) : base(context)
        {

        }
        
        public Player GetById(int playerID)
        {
            Player player = _context.Players
            .FirstOrDefault(e => e.PlayerId == playerID);

            return player;
        }

        //get player by playfabid
        public Player GetByPlayfabID(string playfabID)
        {
            Player player = _context.Players
            .FirstOrDefault(e => e.PlayfabAccountID == playfabID);
            return player;
        }

        public void AddNewPlayer(string name)
        {
            //create a new player with the given name
            Player newPlayer = new Player() { Name = name, CreationDate = DateOnly.FromDateTime(DateTime.Now) };
            //make the player have a unique ID
            newPlayer.PlayerId = _context.Players.Any() ? _context.Players.Max(p => p.PlayerId) + 1 : 1;
            newPlayer.PlayfabAccountID = "";
            _context.Add(newPlayer);
            //save the player to the database
            _context.SaveChanges();
        }

        public void AddNewPlayer(string name, string PlayfabID)
        {
            {
                //create a new player with the given name
                Player newPlayer = new Player() { Name = name, CreationDate = DateOnly.FromDateTime(DateTime.Now) };
                //make the player have a unique ID
                newPlayer.PlayerId = _context.Players.Any() ? _context.Players.Max(p => p.PlayerId) + 1 : 1;
                newPlayer.PlayfabAccountID = PlayfabID;
                _context.Add(newPlayer);
                //save the player to the database
                _context.SaveChanges();
            }
        }

        public void ChangePlayerByID(int playerID, string newName)
        {
            Player playerToChange = GetById(playerID);
            //adjust the name of the player
            if (playerToChange != null)
            {
                playerToChange.Name = newName;
                _context.Update(playerToChange);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Player not found");
            }
        }

        public void DeletePlayer(int playerID)
        {
            Player playerToRemove = GetById(playerID);
            //if it does not exist, return
            if(playerToRemove == null)
            {
                return;
            }
            _context.Remove(playerToRemove);
            _context.SaveChanges();
        }
    }
}
