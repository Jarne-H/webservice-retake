using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PD4ExamAPI.Models;
using PD4ExamAPI.Repositories;

namespace PD4ExamAPI.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/PlayfabItem")]
    [ApiController]
    public class PlayfabItemController : Controller
    {
        private readonly MazeGameContext _context;

        private PlayfabItemRepository _PlayfabItemRepository;

        public PlayfabItemController(MazeGameContext context)
        {
            _context = context;
            _PlayfabItemRepository = new PlayfabItemRepository(context);
        }

        //get all items
        [HttpGet("get/all")]
        [EnableCors("AllowAll")]
        public List<PlayfabItem>? GetAll()
        {
            return _PlayfabItemRepository.GetAllPlayfabItems().ToList();
        }

        [HttpGet("get/all/by-playfabid/{playfabID}")]
        [EnableCors("AllowAll")]
        public List<PlayfabItem>? GetByPlayfabID([FromRoute] string playfabID)
        {
            return _PlayfabItemRepository.GetPlayfabItemsByPlayfabID(playfabID);
        }

        [HttpGet("get/by-id-and-name/{playfabID},{displayName}")]
        [EnableCors("AllowAll")]
        public PlayfabItem? GetByIDAndName([FromRoute] string playfabID, [FromRoute] string displayName)
        {
            return _PlayfabItemRepository.GetPlayfabItemByName(playfabID, displayName);
        }

        //get by itemid
        [HttpGet("get/by-itemid/{itemID}")]
        [EnableCors("AllowAll")]
        public PlayfabItem? GetByItemID([FromRoute] int itemID)
        {
            return _PlayfabItemRepository.GetPlayfabItemByItemID(itemID);
        }

        [HttpPost("post/{playfabID},{displayname}")]
        [EnableCors("AllowAll")]
        public void Post([FromRoute] string playfabID, [FromRoute] string displayname)
        {
            _PlayfabItemRepository.SavePlayfabItem(playfabID, displayname);
        }

        [HttpPut("put/{playfabID},{displayname}")]
        [EnableCors("AllowAll")]
        public void Put([FromRoute] string playfabID, [FromRoute] string displayname)
        {
            _PlayfabItemRepository.GeneratePlayfabItem(playfabID, displayname);
        }

        [HttpDelete("delete/all/by-id/{playfabitemid}")]
        [EnableCors("AllowAll")]
        public void Delete([FromRoute] string playfabitemid)
        {
            _PlayfabItemRepository.DeleteAllPlayfabItems(playfabitemid);
        }

        //delete all
        [HttpDelete("delete/all")]
        [EnableCors("AllowAll")]
        public void DeleteAll()
        {
            _PlayfabItemRepository.DeleteAll();
        }
    }
}
