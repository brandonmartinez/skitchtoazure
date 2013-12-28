using System.Collections.Generic;
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

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody] string value) { }

        public void Put(int id, [FromBody] string value) { }

        #endregion
    }
}