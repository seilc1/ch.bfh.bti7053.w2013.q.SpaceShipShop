using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Users.Images
{
	public class UserImageTransformer
	{
		private static Image ScaleToSize(Image src, Size size)
		{
			Image newImage = new Bitmap(size.Width, size.Height);

			using (Graphics gr = Graphics.FromImage(newImage))
			{
				gr.SmoothingMode = SmoothingMode.HighQuality;
				gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
				gr.DrawImage(src, new Rectangle(0, 0, size.Width, size.Height));
			}

			return newImage;
		}

		private static Image CropImage(Image img, Rectangle cropArea)
		{
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
			return bmpCrop;
		}

		private static ImageTransition GetProportionForImage(Image srcImage, Size targetSize)
		{
			double xRatio = srcImage.Width / targetSize.Width;
			double yRatio = srcImage.Height / targetSize.Height;

			if (Math.Abs(xRatio - yRatio) < 0.001)
			{
				return ImageTransition.Leave;
			}
			else if (yRatio > xRatio)
			{
				return ImageTransition.MakeShorter;
			}
			else
			{
				return ImageTransition.MakeThiner;
			}
		}

		private enum ImageTransition
		{
			MakeThiner,
			MakeShorter,
			Leave
		}

		private Size ThumbnailSize { get { return Common.Constants.ThumbnailSize; } }

		private Size ImageSize { get { return Common.Constants.ImageSize; } }

		public Image TransformToThumbnail(Image image)
		{
			return Transform(image, ThumbnailSize);
		}

		public Image TranformToImage(Image image)
		{
			return Transform(image, ImageSize);
		}

		public Image Transform(Image image, Size size)
		{
			ImageTransition imgTransition = GetProportionForImage(image, size);
			Image resultImage = image;

			if (imgTransition != ImageTransition.Leave)
			{
				int leftOffset = 0;
				int topOffset = 0;

				if (imgTransition == ImageTransition.MakeThiner)
				{
					var cropSizeRatio = image.Height / size.Height;

					leftOffset = (image.Width - (cropSizeRatio * size.Width)) / 2;
				}
				else if (imgTransition == ImageTransition.MakeShorter)
				{
					var cropSizeRatio = image.Width / size.Width;
					topOffset = (image.Height - (cropSizeRatio * size.Height)) / 2;
				}

				resultImage = CropImage(resultImage, new Rectangle(leftOffset, topOffset, (image.Width - (2 * leftOffset)), (image.Height - (2 * topOffset))));
			}

			resultImage = ScaleToSize(resultImage, size);

			return resultImage;
		}
	}
}
