using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkitchToAzure.Controllers
{
    public class UploadsController : ApiController
    {
        #region public

        public void Delete(int id) { }

        /// <summary>
        ///     Returns all available uploads
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<string> Get()
        {
            return new []
            {
                "value1", "value2"
            };
        }

        public string Get(string id)
        {
            return "value";
        }

        /// <summary>
        /// Responds to HEAD requests looking for a file ID. This is the magic method that *ALWAYS* returns HTTP 200.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Head(string id)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("All Good!"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public void Post([FromBody] string value) { }

        public void Put(int id, [FromBody] string value) { }

        #endregion
    }
}