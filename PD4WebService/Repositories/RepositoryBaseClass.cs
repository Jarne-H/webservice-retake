using PD4ExamAPI.Models;

namespace PD4ExamAPI.Repositories
{
    public class RepositoryBaseClass
    {
        protected readonly MazeGameContext _context;

        public RepositoryBaseClass(MazeGameContext context)
        {
            _context = context;
        }
    }
}
