using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Uniques.Library.Data;

namespace Uniques.Library.Users.Images
{
	public class UserImageManager
	{
		private readonly Func<UniquesDataContext> _dbContextGetter;
		private readonly UserImageDataManager _dataManager;

		public UserImageManager(Func<UniquesDataContext> dbContextGetter, UserImageDataManager dataManager)
		{
			_dbContextGetter = dbContextGetter;
			_dataManager = dataManager;
		}

		public IEnumerable<Image> GetAllForUser(int userId)
		{
			return _dbContextGetter().Images.Where(image => image.Owner.Id == userId);
		}

		public Image Get(int imageId)
		{
			return Get(imageId, _dbContextGetter());
		}

		public Image Get(Guid imageId)
		{
			return Get(imageId, _dbContextGetter());
		}

		private Image Get(Guid imageId, UniquesDataContext dbContext)
		{
			return dbContext.Images.Include("Owner").FirstOrDefault(image => image.FileId == imageId);
		}

		private Image Get(int imageId, UniquesDataContext dbContext)
		{
            return dbContext.Images.Include("Owner").FirstOrDefault(image => image.Id == imageId);
		}

		public Image Patch(int imageId, string description)
		{
			var dbContext = _dbContextGetter();
			return Patch(Get(imageId, dbContext), description, dbContext);
		}

		public Image Patch(Guid imageId, string description)
		{
			var dbContext = _dbContextGetter();
			return Patch(Get(imageId, dbContext), description, dbContext);
		}

		private Image Patch(Image image, string description, UniquesDataContext dbContext)
		{
			image.Description = description;
			dbContext.SaveChanges();
			return image;
		}

		public Image GetProfilePicture(int userId)
		{
			return GetProfilePicture(userId, _dbContextGetter());
		}

		private Image GetProfilePicture(int userId, UniquesDataContext dbContext)
		{
            return dbContext.Images.Include("Owner").FirstOrDefault(image => image.Owner.Id == userId && image.IsPortrait);
		}

		public Image PutProfilePicture(int userId, int newProfileImageId)
		{
			var dbContext = _dbContextGetter();
			return PutProfilePicture(userId, Get(newProfileImageId, dbContext), dbContext);
		}

		public Image PutProfilePicture(int userId, Guid newProfileImageId)
		{
			var dbContext = _dbContextGetter();
			return PutProfilePicture(userId, Get(newProfileImageId, dbContext), dbContext);
		}

		private Image PutProfilePicture(int userId, Image image, UniquesDataContext dbContext)
		{
			if (image != null)
			{
				var oldImage = GetProfilePicture(userId, dbContext);

				if (oldImage != null)
				{
					oldImage.IsPortrait = false;
				}

				image.IsPortrait = true;
				dbContext.SaveChanges();
			}
			
			return image;
		}

		public void DeleteImage(Guid imageId)
		{
			var dbContext = _dbContextGetter();
			DeleteImage(Get(imageId, dbContext), dbContext);
		}

		public void DeleteImage(int imageId)
		{
			var dbContext = _dbContextGetter();
			DeleteImage(Get(imageId, dbContext), dbContext);
		}

		private void DeleteImage(Image image, UniquesDataContext dbContext)
		{
			dbContext.Images.Remove(image);
			dbContext.SaveChanges();

			_dataManager.Delete(image);
		}

		public Image Put(int userId, string description, Stream imageData)
		{
			var dbContext = _dbContextGetter();

			var image = dbContext.Images.Create();
			image.Owner = dbContext.Users.FirstOrDefault(u => u.Id == userId);
			image.FileId = Guid.NewGuid();
			image.Description = description;

			dbContext.Images.Add(image);
		    dbContext.SaveChanges();

			_dataManager.Put(image, imageData);
			return image;
		}

		public ImageData GetImageData(Guid imageId)
		{
			return _dataManager.GetAsImage(Get(imageId));
		}

		public ImageData GetThumbnailData(Guid imageId)
		{
			return _dataManager.GetAsThumbnail(Get(imageId));
		}

		public ImageData GetImageData(int imageId)
		{
			return _dataManager.GetAsImage(Get(imageId));
		}

		public ImageData GetThumbnailData(int imageId)
		{
            return _dataManager.GetAsThumbnail(Get(imageId));
		}

		public ImageData GetImageData(Image image)
		{
			return _dataManager.GetAsImage(image);
		}

		public ImageData GetThumbnailData(Image image)
		{
            return _dataManager.GetAsThumbnail(image);
		}
	}
}
