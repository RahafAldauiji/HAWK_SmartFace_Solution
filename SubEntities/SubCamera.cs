using System.Collections.Generic;
using SmartfaceSolution.Models;
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
        public Camera getCamera(string id)
        {
            string result = new SmartfaceResquest().requestNoBody("Cameras/" + id, "GET");
            Camera camera = JsonSerializer.Deserialize<Camera>(result);
            return camera;
        }

        /// <summary>
        /// Method <c>getAllCameras</c> gets all cameras that are stored in the database
        /// </summary>
        /// <returns>list of cameras</returns>
        public List<Camera> getAllCameras()
        {
            string result = new SmartfaceResquest().requestNoBody("Cameras/", "GET");
            List<Camera> cameras = JsonSerializer.Deserialize<List<Camera>>(result);
            return cameras;
        }

        /// <summary>
        /// Method <c>createCamera</c> connect a new camera to the system
        /// </summary>
        /// <param name="rtsp">the real time streaming protocol of the camera</param>
        /// <param name="cameraName">the name of the camera</param>
        /// <returns>the new camera</returns>
        public Camera createCamera(string rtsp, string cameraName)
        {
            string json = "{\"name\":\"" + cameraName + "\",\"source\":\"" + rtsp + "\",\"enabled\":\"" +
                          true + "\"}";
            string result = new SmartfaceResquest().requestWithBody("Cameras/", "POST", json);
            Camera camera = JsonSerializer.Deserialize<Camera>(result);

            return camera;
        }

        /// <summary>
        /// Method <c>updateCamera</c> update a specific camera from the system
        /// </summary>
        /// <param name="cam">the camera that will be updated</param>
        /// <returns>the camera after the updating</returns>
        public Camera updateCamera(Camera cam)
        {
            string jsonCam = JsonSerializer.Serialize(cam);
            string result = new SmartfaceResquest().requestWithBody("Cameras/", "PUT", jsonCam);
            Camera camera = JsonSerializer.Deserialize<Camera>(result);
            return camera;
        }

        /// <summary>
        /// Method <c>deleteCamera</c> delete a specific camera from the system 
        /// </summary>
        /// <param name="id">the id of the camera to be deleted</param>
        /// <returns>the deleted camera</returns>
        public string deleteCamera(string id)
        {
            string camera  = new SmartfaceResquest().requestNoBody("Cameras/" + id, "DELETE");
            return camera;
        }
    }
}