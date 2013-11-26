using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Users.Images
{
	public class UserImageDataManager
	{
		private readonly UserImageTransformer _transformer;

		private readonly string _basePath;

		public const string ImageSuffix = "png";

		public const string ImageContentType = "image/png";

		public const string ThumbnailFolder = "Thumbnails";

		public const string ImageFolder = "Images";

		public const string ImagePathFormat = "{0}/{1}.{2}";

		public const string ImageFolderPathFormat = "{0}/{1}/{2}";

		public static readonly ImageFormat ImageFormat = ImageFormat.Png;

		public UserImageDataManager(string basePath, UserImageTransformer transformer)
		{
			_basePath = basePath;
			_transformer = transformer;
		}

		private string GetUserImageFolder(Data.Image image, bool isThumbnail = false)
		{
			return string.Format(
				CultureInfo.CurrentCulture,
				ImageFolderPathFormat,
				_basePath,
				isThumbnail ? ThumbnailFolder : ImageFolder,
				image.Owner.Id);
		}

		private string GetImagePath(Data.Image image, bool isThumbnail = false)
		{
			return string.Format(
				CultureInfo.CurrentCulture,
				ImagePathFormat,
				GetUserImageFolder(image, isThumbnail),
				image.FileId,
				ImageSuffix);
		}

		private string GetUserThumbnailFolder(Data.Image image)
		{
			return GetUserImageFolder(image);
		}

		private string GetThumbnailPath(Data.Image image)
		{
			return GetImagePath(image, true);
		}

		private Image FromStream(Stream stream)
		{
			return Image.FromStream(stream);
		}

		private void EnsureImageFolders(Data.Image image)
		{
			Directory.CreateDirectory(GetUserThumbnailFolder(image));
			Directory.CreateDirectory(GetUserImageFolder(image));
		}

		public Data.Image Put(Data.Image image, Stream data)
		{
			EnsureImageFolders(image);

			var bitmap = FromStream(data);

			_transformer.TransformToThumbnail(bitmap).Save(GetThumbnailPath(image), ImageFormat);
			_transformer.TranformToImage(bitmap).Save(GetImagePath(image), ImageFormat);

			return image;
		}

		private ImageData Get(Data.Image image, bool isThumbnail = false)
		{
			return new ImageData
					{
						FileContentType = ImageContentType,
						Data = File.OpenRead(isThumbnail ? GetThumbnailPath(image) : GetImagePath(image))
					};
		}

		public ImageData GetAsThumbnail(Data.Image image)
		{
			return Get(image, true);
		}

		public ImageData GetAsImage(Data.Image image)
		{
			return Get(image);
		}

		public void Delete(Data.Image image)
		{
			var imagePaths = new [] {GetThumbnailPath(image), GetThumbnailPath(image)};

			foreach (var imagePath in imagePaths)
			{
				if (File.Exists(imagePath))
				{
					File.Delete(imagePath);
				}
			}
		}
	}
}
