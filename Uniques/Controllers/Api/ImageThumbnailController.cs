using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using StructureMap;
using Uniques.Library.Mvc;
using Uniques.Library.Users.Images;

namespace Uniques.Controllers.Api
{
    public class ImageThumbnailController : ApiController
    {
        private UserImageManager UserImageManager
        {
            get { return ObjectFactory.GetInstance<UserImageManager>(); }
        }

        [RequiresRouteValues("userId,imageId")]
        public HttpResponseMessage Get(int userId, int imageId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            var result = UserImageManager.GetThumbnailData(imageId);

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
