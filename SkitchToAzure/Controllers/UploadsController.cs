using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using SkitchToAzure.Azure;

namespace SkitchToAzure.Controllers
{
    public class UploadsController : ApiController
    {
        #region Fields

        private readonly BlobClient _blobClient;

        private readonly string _path;

        #endregion

        #region Constructors

        internal UploadsController(BlobClient blobClient, string serverPath)
        {
            _blobClient = blobClient;
            _path = serverPath;

            // if path doesn't exist, create it
            if(!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public UploadsController(string blobStorageConnectionString, string blobStorageContainerName, string serverPath)
            : this(new BlobClient(blobStorageConnectionString, blobStorageContainerName), serverPath) { }

        public UploadsController()
            : this(
                ConfigurationManager.AppSettings["BlobStorageConnectionString"],
                ConfigurationManager.AppSettings["BlobStorageContainerName"],
                HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadFolderVirtualPath"])) { }

        #endregion

        #region public

        public void Delete(string id)
        {
            _blobClient.Delete(id);
        }

        /// <summary>
        ///     Returns all available uploads
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<string> Get()
        {
            var blob = _blobClient.Get();
            return blob;
        }

        public HttpResponseMessage Get(string id)
        {
            var blob = _blobClient.Get(id);
            var content = new ByteArrayContent(blob);
            var contentType = "application/octet-stream";
            switch(Path.GetExtension(id))
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".mp4":
                case ".m4v":
                    contentType = "video/mp4";
                    break;
                case ".mpg":
                case ".mpeg":
                    contentType = "video/mpeg";
                    break;
                case ".mov":
                    contentType = "video/quicktime";
                    break;
                default:
                    // I realize this is a side-effect, cleanest way to set it though
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = id
                    };
                    break;
            }
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return new HttpResponseMessage
            {
                Content = content,
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        ///     Responds to HEAD requests looking for a file ID. This is the magic method that *ALWAYS* returns HTTP 200.
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public HttpResponseMessage Head(string id)
        {
            Task.Run(() =>
            {
                var filepath = Path.Combine(_path, id);

                if(File.Exists(filepath))
                {
                    using(var file = File.OpenRead(filepath))
                    {
                        _blobClient.Upload(id, file);
                    }

                    File.Delete(filepath);
                }
            });

            return new HttpResponseMessage
            {
                Content = new StringContent("All Good!"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public void Post(string id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        public void Put(string id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}