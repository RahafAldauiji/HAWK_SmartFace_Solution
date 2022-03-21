using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SmartfaceSolution.SubEntities
{
    /// <summary> 
    /// Entity <c>HttpResquest</c> will send the request to the smartFace platform and get the response
    /// </summary>
    public class HttpResquest
    {
        /// <summary>
        /// Method <c>response</c> will get the response from the http request 
        /// </summary>
        /// <param name="httpWebRequest">the Http Web Request</param>
        /// <returns>the response</returns>
        public string response(HttpWebRequest httpWebRequest)
        {
            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            string result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                try
                {
                    result = streamReader.ReadToEnd();
                }
                finally
                {
                    streamReader.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Method <c>requestNoBody</c> will send the http web request with no body request 
        /// </summary>
        /// <param name="reqUrl">the request url</param>
        /// <param name="methodType">the method type</param>
        /// <returns>the response of the request</returns>
        public async Task<string> requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/" + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                res = response(httpWebRequest);
            });
            return res;
        }

        /// <summary>
        /// Method <c>requestWithBody</c> will send the http web request with body request 
        /// </summary>
        /// <param name="reqUrl">the request url</param>
        /// <param name="methodType">the method type</param>
        /// <param name="json">the request body json</param>
        /// <returns>the response of the request</returns>
        public async Task<string> requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/" + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    try
                    {
                        streamWriter.Write(json);
                    }
                    finally
                    {
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }


                res = response(httpWebRequest);
            });
            return res;
        }
    }
}