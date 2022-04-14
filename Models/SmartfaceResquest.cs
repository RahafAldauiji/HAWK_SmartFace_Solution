using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SmartfaceSolution.Helpers;

namespace SmartfaceSolution.Models
{
    /// <summary> 
    /// Entity <c>HttpResquest</c> will send the request to the smartFace platform and get the response
    /// </summary>
    public class SmartfaceResquest
    {
        private string serverName = "http://localhost:8098/api/v1/";
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
                    (HttpWebRequest) WebRequest.Create(serverName + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                var response = (HttpWebResponse) httpWebRequest.GetResponse();
                res = response.StatusCode == (HttpStatusCode) 400
                    ? throw new AppException("Wrong Information Format")
                    : new StreamReader((response).GetResponseStream())
                        .ReadToEnd();
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
                    (HttpWebRequest) WebRequest.Create(serverName + reqUrl);
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

                var response = (HttpWebResponse) httpWebRequest.GetResponse();
                res = response.StatusCode == (HttpStatusCode) 400
                    ? throw new AppException("Wrong Information Format")
                    : new StreamReader((response).GetResponseStream())
                        .ReadToEnd();
                Console.WriteLine(res);
            });
            return res;
        }
    }
}