using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using SmartfaceSolution.Classes;

namespace SmartfaceSolution.SubClasses
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

        public string requestNoBody(string reqUrl, string methodType)
        {
            string res = null;
            try
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Cameras/" + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                res = response(httpWebRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return res;
        }

        public string requestWithBody(string reqUrl, string methodType, string json)
        {
            string res = null;
            try
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:8098/api/v1/Cameras/" + reqUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = methodType;
                if (!methodType.Equals("DELETE"))
                {
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
                }

                res = response(httpWebRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return res;
        }

        public Camera getCamera(string id)
        {
            Camera camera = null;
            try
            {
                string result = requestNoBody(id, "GET");
                camera = JsonSerializer.Deserialize<Camera>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            return camera;
        }

        public List<Camera> getAllCameras()
        {
            List<Camera> cameras = null;
            try
            {
                string result = requestNoBody("", "GET");
                //Console.WriteLine(result);
                cameras = JsonSerializer.Deserialize<List<Camera>>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return cameras;
        }

        public Camera createCamera(string rtsp, string cameraName)
        {
            Camera camera = null;
            try
            {
                string json = "{\"name\":\"" + cameraName + "\",\"source\":\"" + rtsp + "\",\"enabled\":\"" +
                              true + "\"}";
                string result = requestWithBody("", "POST", json);
                camera = JsonSerializer.Deserialize<Camera>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return camera;
        }

        public Camera updateCamera(string jsonCam)
        {
            Camera camera = null;
            try
            {
                string result = requestWithBody("", "PUT", jsonCam);
                camera = JsonSerializer.Deserialize<Camera>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return camera;
        }

        public Camera deleteCamera(string id)
        {
            Camera camera = null;
            try
            {
                string result = requestNoBody(id, "DELETE");
                camera = JsonSerializer.Deserialize<Camera>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }

            return camera;
        }
    }
}