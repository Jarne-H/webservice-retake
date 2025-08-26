using Microsoft.EntityFrameworkCore;
using PD4ExamAPI.Models;

namespace PD4ExamAPI.Repositories
{
    public class ImageRepository : RepositoryBaseClass
    {

        // ImageID, ImageName, link
        public ImageRepository(MazeGameContext context) : base(context)
        {

        }

        //public Player GetById(int playerID)
        //{
        //    Player player = _context.Players
        //    .FirstOrDefault(e => e.PlayerId == playerID);

        //    return player;
        //}

        //ImageId
        public Image GetByID(int imageID)
        {
            Image? image = _context.Images
                .FirstOrDefault(e => e.ImageId == imageID);
            return image;
        }
        // MazeId
        public List<Image> GetByName(string name)
        {
            List<Image> images = _context.Images
                .Where(e => e.Name == name)
                .ToList();
            return images;
        }

        // Create a new image
        public void AddNewImage(string imageName, string imageLink)
        {
            Image newImage = new Image
            {
                //imageID must be unique, so we can set it to the max ID + 1 or 1 if no images exist
                ImageId = _context.Images.Any() ? _context.Images.Max(i => i.ImageId) + 1 : 1,
                //image name
                Name = imageName,
                Link = imageLink
            };
            _context.Add(newImage);
            _context.SaveChanges();
        }

        //updateimage by ID 
        public void UpdateByID(int imageID, string imageName, string imageLink)
        {
            Image? imageToUpdate = GetByID(imageID);
            if (imageToUpdate != null)
            {
                imageToUpdate.Name = imageName;
                imageToUpdate.Link = imageLink;
                _context.Update(imageToUpdate);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Image not found");
            }
        }
        //update image by name
        public void UpdateByName(string imageName, string imageLink)
        {
            Image? imageToUpdate = _context.Images.FirstOrDefault(e => e.Name == imageName);
            if (imageToUpdate != null)
            {
                imageToUpdate.Link = imageLink;
                _context.Update(imageToUpdate);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Image not found");
            }
        }

        //delete image by id
        public void DeleteByID(int imageID)
        {
            Image? imageToRemove = GetByID(imageID);
        }
        //delete image by name
        public void DeleteByName(string imageName)
        {
            Image? imageToRemove = _context.Images.FirstOrDefault(e => e.Name == imageName);
            if (imageToRemove != null)
            {
                _context.Remove(imageToRemove);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Image not found");
            }
        }
    }
}
