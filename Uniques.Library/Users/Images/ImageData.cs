using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Users.Images
{
	public class ImageData : IDisposable
	{
		public string FileContentType { get; set; }

		public Stream Data { get; set; }

		public void Dispose()
		{
			Data.Dispose();
		}
	}
}
