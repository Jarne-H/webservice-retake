using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [Route("api/external-resources")]
    [ApiController]
    public class ExternalResourceController : Controller
    {
        private readonly MazeGameContext _context;

        public ExternalResourceController(MazeGameContext context)
        {
            _context = context;
        }

        [HttpGet("get/by-id/{resourceID}")]
        public ExternalResource? Get([FromRoute] int resourceID)
        {
            return _context.ExternalResources.FirstOrDefault(e => e.ResourceId == resourceID);
        }
    }
}
