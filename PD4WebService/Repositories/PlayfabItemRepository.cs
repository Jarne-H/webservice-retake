using Microsoft.EntityFrameworkCore;
using PD4ExamAPI.Models;
using System.Diagnostics;

namespace PD4ExamAPI.Repositories
{
    public class PlayfabItemRepository : RepositoryBaseClass
    {
        private readonly MazeGameContext _context;

        public PlayfabItemRepository(MazeGameContext context) : base(context)
        {
            _context = context;
        }

        ////get all items indeptedent who they are from
        //public List<PlayfabItem>? GetAllPlayfabItems()
        //{
        //    List<PlayfabItem> playfabItems = _context.PlayfabItems
        //        .ToList();
        //    //debug output the count of items found
        //    Debug.WriteLine($"Found {playfabItems.Count} total items in database");
        //    return playfabItems;
        //}

        public List<PlayfabItem>? GetPlayfabItemsByPlayfabID(string playfabid)
        {
            List<PlayfabItem> playfabItems = _context.PlayfabItems
                .Where(e => e.playfabid == playfabid)
                .ToList();

            //debug output the count of items found
            Debug.WriteLine($"Found {playfabItems.Count} items for PlayfabID {playfabid}");

            return playfabItems;
        }

        public PlayfabItem? GetPlayfabItemByName(string playfabID, string name)
        {
            //find the item where the id and name match
            PlayfabItem PlayfabItem = _context.PlayfabItems
                .FirstOrDefault(e => e.playfabid.ToString() == playfabID && e.displayname == name);

            return PlayfabItem;
        }

        //get all items
        public IQueryable<PlayfabItem>? GetAllPlayfabItems()
        {
            IQueryable<PlayfabItem> playfabItems = _context.PlayfabItems;
            //debug output the count of items found
            Debug.WriteLine($"Found {playfabItems.Count()} total items in database");
            return playfabItems;
        }

        //get item by itemid
        public PlayfabItem? GetPlayfabItemByItemID(int itemid)
        {
            PlayfabItem PlayfabItem = _context.PlayfabItems
                .FirstOrDefault(e => e.PlayfabItemId == itemid);
            return PlayfabItem;
        }

        public void SavePlayfabItem(string playfabID, string displayName)
        {
            ////find any with the same name, then delete the old one
            PlayfabItem? existingPlayfabItem = GetPlayfabItemByName(playfabID, displayName);
            if (existingPlayfabItem != null)
            {
                DeletePlayfabItem(existingPlayfabItem.playfabid, existingPlayfabItem.displayname);
            }

            PlayfabItem newPlayfabItem = new()
            {
                playfabid = playfabID,
                displayname = displayName
            };

            _context.Add<PlayfabItem>(newPlayfabItem);
            _context.SaveChanges(); // Ensure playfabid is set
        }

        //create a new PlayfabItem with originalplayfabid
        public void GeneratePlayfabItem(string playfabID, string displayName)
        {
            //find any with the same name, then delete the old one
            PlayfabItem? existingPlayfabItem = GetPlayfabItemByName(playfabID, displayName);
            if (existingPlayfabItem != null)
            {
                DeletePlayfabItem(existingPlayfabItem.playfabid, existingPlayfabItem.displayname);
            }
            PlayfabItem newPlayfabItem = new PlayfabItem()
            {
                playfabid = playfabID,
                displayname = displayName
            };

            _context.Add<PlayfabItem>(newPlayfabItem);
            _context.SaveChanges();
        }

        //delete PlayfabItem by id, and delete all tiles associated with it
        public void DeletePlayfabItem(string playfabid, string displayName)
        {
            PlayfabItem? PlayfabItemToRemove = GetPlayfabItemByName(playfabid, displayName);
            if (PlayfabItemToRemove != null)
            {
                _context.Remove(PlayfabItemToRemove);
                _context.SaveChanges();
            }
        }

        public void DeleteAllPlayfabItems(string playfabid)
        {
            var PlayfabItemsToRemove = GetPlayfabItemsByPlayfabID(playfabid);
            if (PlayfabItemsToRemove != null)
            {
                _context.RemoveRange(PlayfabItemsToRemove);
                _context.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"No items found to delete for PlayfabID {playfabid}");
            }
        }

        //delete all
        public void DeleteAll()
        {
            var allItems = _context.PlayfabItems.ToList();
            _context.RemoveRange(allItems);
            _context.SaveChanges();
        }
    }
}
