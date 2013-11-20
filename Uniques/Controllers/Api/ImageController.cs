using System;
using System.Collections.Generic;
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

namespace Uniques.Controllers.Api
{
    public class ImageController : ApiController
    {
        private string GetFilePath(string filename)
        {
            return string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/App_Data"), filename);
        }

        public async Task<HttpResponseMessage> PostFormData(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentDisposition.FileName != null)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();

                    File.WriteAllBytes(GetFilePath(filename), buffer);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get(int? id, string name)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            httpResponseMessage.Content = new ByteArrayContent(File.ReadAllBytes(GetFilePath(name)));

            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            httpResponseMessage.StatusCode = HttpStatusCode.OK;

            return httpResponseMessage;
        }
    }
}
