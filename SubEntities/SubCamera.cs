using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    /// <summary>
    /// <c>SubCamera</c> provide all the operation that are necessary to operate in the Camera entity 
    /// </summary>
    public class SubCamera
    {
        
        /// <summary>
        /// Method <c>getCamera</c> gets a specific camera
        /// </summary>
        /// <param name="id">the id of the camera</param>
        /// <returns>the camera</returns>
        public async Task<Camera> getCamera(string id)
        {
            Camera camera = null;
            await Task.Run(async () =>
            {
                string result = await new HttpResquest().requestNoBody("Cameras/"+id, "GET");
                camera = JsonSerializer.Deserialize<Camera>(result);
            });
            return camera;
        }

        /// <summary>
        /// Method <c>getAllCameras</c> gets all cameras that are stored in the database
        /// </summary>
        /// <returns>list of cameras</returns>
        public async Task<List<Camera>> getAllCameras()
        {
            List<Camera> cameras = null;
            await Task.Run(async () =>
            {
                string result = await new HttpResquest().requestNoBody("Cameras/", "GET");
                cameras = JsonSerializer.Deserialize<List<Camera>>(result);
            });
            return cameras;
        }

        /// <summary>
        /// Method <c>createCamera</c> connect a new camera to the system
        /// </summary>
        /// <param name="rtsp">the real time streaming protocol of the camera</param>
        /// <param name="cameraName">the name of the camera</param>
        /// <returns>the new camera</returns>
        public async Task<Camera> createCamera(string rtsp, string cameraName)
        {
            Camera camera = null;
            await Task.Run(async () =>
            {
                string json = "{\"name\":\"" + cameraName + "\",\"source\":\"" + rtsp + "\",\"enabled\":\"" +
                              true + "\"}";
                string result = await new HttpResquest().requestWithBody("Cameras/", "POST", json);
                camera = JsonSerializer.Deserialize<Camera>(result);
            });
            return camera;
        }

        /// <summary>
        /// Method <c>updateCamera</c> update a specific camera from the system
        /// </summary>
        /// <param name="cam">the camera that will be updated</param>
        /// <returns>the camera after the updating</returns>
        public async Task<Camera> updateCamera(Camera cam)
        {
            Camera camera = null;
            string jsonCam = JsonSerializer.Serialize(cam);
            string result = await new HttpResquest().requestWithBody("Cameras/", "PUT", jsonCam);
            camera = JsonSerializer.Deserialize<Camera>(result);

            return camera;
        }

        /// <summary>
        /// Method <c>deleteCamera</c> delete a specific camera from the system 
        /// </summary>
        /// <param name="id">the id of the camera to be deleted</param>
        /// <returns>the deleted camera</returns>
        public async Task<string> deleteCamera(string id)
        {
            string camera = "";
            await Task.Run(async () =>
            {
                string result = await new HttpResquest().requestNoBody("Cameras/"+id, "DELETE");
                camera = result;
            });
            return camera;
        }
    }
}