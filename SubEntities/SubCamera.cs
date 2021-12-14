using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    public class SubCamera : Camera
    {
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

        public async Task<string> requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Cameras/" + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                res = response(httpWebRequest);
            });
            return res;
        }

        public async Task<string> requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            await Task.Run(async () =>
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Cameras/" + reqUrl);
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

        public async Task<Camera> getCamera(string id)
        {
            Camera camera = null;
            await Task.Run(async () =>
            {
                string result = await requestNoBody(id, "GET");
                camera = JsonSerializer.Deserialize<Camera>(result);
            });
            return camera;
        }

        public async Task<List<Camera>> getAllCameras()
        {
            List<Camera> cameras = null;
            await Task.Run(async () =>
            {
                string result = await requestNoBody("", "GET");
                //Console.WriteLine(result);
                cameras = JsonSerializer.Deserialize<List<Camera>>(result);
            });
            return cameras;
        }

        public async Task<Camera> createCamera(string rtsp, string cameraName)
        {
            Camera camera = null;
            await Task.Run(async () =>
            {
                string json = "{\"name\":\"" + cameraName + "\",\"source\":\"" + rtsp + "\",\"enabled\":\"" +
                              true + "\"}";
                string result = await requestWithBody("", "POST", json);
                camera = JsonSerializer.Deserialize<Camera>(result);
            });
            return camera;
        }

        public async Task<Camera> updateCamera(Camera cam)
        {
            Camera camera = null;
            string jsonCam = JsonSerializer.Serialize(cam);

            string result = await requestWithBody("", "PUT", jsonCam);
            camera = JsonSerializer.Deserialize<Camera>(result);

            return camera;
        }

        public async Task<string> deleteCamera(string id)
        {
            string camera = "";
            await Task.Run(async () =>
            {
                string result = await requestNoBody(id, "DELETE");
                camera = result;
            });
            return camera;
        }
    }
}