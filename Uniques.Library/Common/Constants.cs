using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Common
{
	public class Constants
	{
		public const string EmailRegExp = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";

		public const string AdminRole = "Administrator";

		public static readonly Size ThumbnailSize = new Size(180, 240); 

		public static readonly Size ImageSize = new Size(240, 320);
	}
}
