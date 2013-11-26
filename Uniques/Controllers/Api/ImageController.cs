using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using StructureMap;
using Uniques.Library.Data;
using Uniques.Library.Mvc;
using Uniques.Library.Users.Images;

namespace Uniques.Controllers.Api
{
	public class ImageController : ApiController
	{
		private UserImageManager UserImageManager
		{
			get { return ObjectFactory.GetInstance<UserImageManager>(); }
		}

		public async Task<HttpResponseMessage> PostFormData(int userId, string description)
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			var provider = new MultipartMemoryStreamProvider();
			await Request.Content.ReadAsMultipartAsync(provider);

			if (provider.Contents.Count > 0)
			{
				UserImageManager.Put(userId, description, await provider.Contents[0].ReadAsStreamAsync());
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[RequiresRouteValues("userId")]
		public IEnumerable<Image> Get(int userId)
		{
			return UserImageManager.GetAllForUser(userId);
		}

		[RequiresRouteValues("userId,imageId")]
		public HttpResponseMessage Get(int userId, int imageId)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

			using (var result = UserImageManager.GetImageData(userId))
			{
				if (result != null)
				{
					httpResponseMessage.Content = new StreamContent(result.Data);

					httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(result.FileContentType);
					httpResponseMessage.StatusCode = HttpStatusCode.OK;
				}
				else
				{
					httpResponseMessage.StatusCode = HttpStatusCode.NotFound;
				}

				return httpResponseMessage;
			}
		}


	}
}
