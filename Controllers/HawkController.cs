using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartfaceSolution.Entities;
using SmartfaceSolution.Helpers;
using SmartfaceSolution.SubEntities;
using SmartfaceSolution.Models;
using SmartfaceSolution.Services;

namespace SmartfaceSolution.Controllers
{
    /// <summary>
    /// <c>HawkController</c> is Controller defines the endpoint our system
    /// It will handles all the routes for the API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("Smartface")]
    [EnableCors("AnotherPolicy")]
    public class HawkController : Controller
    {
        private IUserService _user;
        private readonly IDBConnection _dbConnection;

        public HawkController(IUserService user, IDBConnection dbConnection)
        {
            _user = user;
            _dbConnection = dbConnection;
        }


        #region authenticate

        /// <summary>
        /// Authenticate the user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bad Request if the user is unauthorized else return a json that contain JWT token.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest user)
        {
            var response = _user.Authenticate(user);
            Console.WriteLine(response.Result);
            return response.Result == null
                ? throw new UnauthorizedAccessException("Invalid User")
                : Json(response.Result);
        }

        #endregion

        /// <summary>
        /// Camera region have all the Camera operation
        /// </summary>

        #region Camera

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the camera id to get the specific camera information.
        /// The request will be sent it to samrtface platform using method getCamera from class SubCamera then the result will return it to the user 
        /// </summary>
        /// <param name="id">camera id</param>
        /// <returns>Camera data</returns>
        [Authorize]
        [HttpGet]
        [Route("Camera/getCamera")]
        public IActionResult getCamera(string id)
        {
            return Json(new SubCamera().getCamera(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The request will be sent it to samrtface platform using method getAllCameras from class SubCamera then the result
        /// will return it to the user contain all the cameras information in the system
        /// </summary>
        /// <returns>all cameras</returns>
        [Authorize]
        [HttpGet]
        [Route("Camera/getAllCameras")]
        public IActionResult getAllCameras()
        {
            return Json(new SubCamera().getAllCameras());
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the camera RTSP and the name and by default we will enable the camera.
        /// The request will be sent it to samrtface platform using method createCamera from class SubCamera then the result will return it to the user contain the new camera
        /// </summary>
        /// <param name="cam">camera</param>
        /// <returns>new camera</returns>
        [Authorize]
        [HttpPost]
        [Route("Camera/create")]
        public IActionResult createCamera([FromBody] Camera cam)
        {
            return Json(new SubCamera().createCamera(rtsp: cam.source, cameraName: cam.name));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the camera and the new information to be updated.
        /// The request will be sent it to samrtface platform using method updateCamera from class SubCamera then the result will return it to the user contain the updated camera 
        /// </summary>
        /// <param name="camera">camera data</param>
        /// <returns>updated camera</returns>
        [Authorize]
        [HttpPost]
        [Route("Camera/update")]
        public IActionResult updateCamera(string camera)
        {
            Camera updatedCamera = new SubCamera().updateCamera(JsonConvert.DeserializeObject<Camera>(camera));
            return Json(updatedCamera);
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the camera.
        /// Then, the response will be the information of the deleted camera.
        /// The request will be sent it to samrtface platform using method deleteCamera from class SubCamera then the result will return it to the user 
        /// </summary>
        /// <param name="id">camera id</param>
        /// <returns>deleted camera</returns>
        [Authorize]
        [HttpDelete]
        [Route("Camera/delete")]
        public IActionResult deleteCamera(string id)
        {
            return Json(new SubCamera().deleteCamera(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The response will be send if the smartface platform detect and match any member on the system.
        /// We will listen to the changes in the database using ZeroMQ library that running on post 2406.
        /// If there is any match the system will send a response with the member match information 
        /// </summary>
        /// <returns>match</returns>
        [Authorize]
        [HttpGet]
        [Route("Match")]
        public IActionResult getMatch()
        {
            return Json(new SubMatchFaces().matchFaces());
        }

        #endregion

        /// <summary>
        /// Watchlist region have all the Watchlist operation
        /// </summary>

        #region Watchlist

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the watchlist display, full name, and threshold.
        /// The request will be sent it to samrtface platform using method createWatchlist from class SubWatchlist then the result will return it to the user contain the new watchlist information 
        /// </summary>
        /// <param name="watchlist">watchlist data</param>
        /// <returns>new watchlist</returns>
        [Authorize]
        [HttpPost]
        [Route("Watchlist/create")]
        public IActionResult createWatchlist([FromBody] Watchlist watchlist)
        {
            return Json(new SubWatchlist().createWatchList(watchlist.displayName, watchlist.fullName,
                watchlist.threshold));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the Id of the watchlist.
        /// The request will be sent it to samrtface platform using method getWatchlistMembers from class SubWatchlist then the result will return it to the user contain all the members in the watchlist 
        /// </summary>
        /// <param name="id">watchlist id</param>
        /// <returns>members</returns>
        [Authorize]
        [HttpGet]
        [Route("Watchlist/getMembers")]
        public IActionResult getWatchlistMembers(string id)
        {
            return Json((new SubWatchlist().retrievesWatchlistMembers(id)).items);
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the watchlist and the new information to be updated.
        /// The request will be sent it to samrtface platform using method updateWatchlist from class SubWatchlist then the result will return it to the user contain the updated watchlist 
        /// </summary>
        /// <param name="watchlist">watchlist data</param>
        /// <returns>updated watchlist</returns>
        [Authorize]
        [HttpPost]
        [Route("Watchlist/upadte")]
        public IActionResult updateWatchlist(string watchlist)
        {
            Watchlist list = JsonConvert.DeserializeObject<Watchlist>(watchlist);
            return Json(new SubWatchlist().updateWatchList(list.id, list.displayName,
                list.fullName, list.threshold));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The request will be sent it to samrtface platform using method retrievesAllWatchlist from class SubWatchlist then the result will return it to the user contain all watchlist in the system
        /// </summary>
        /// <returns>all watchlists</returns>
        [Authorize]
        [HttpGet]
        [Route("Watchlist/getAllWatchlist")]
        public IActionResult getAllWatchlist()
        {
            return Json(new SubWatchlist().retrievesAllWatchlist());
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the watchlist id to get the specific watchlist.
        /// The request will be sent it to samrtface platform using method getWatchlist from class SubWatchlist then the result will return it to the user contain all watchlist in the system 
        /// </summary>
        /// <param name="id">watchlist id</param>
        /// <returns>watchlist</returns>
        [Authorize]
        [HttpGet]
        [Route("Watchlist/getWatchlist")]
        public IActionResult getWatchlist(string id)
        {
            return Json(new SubWatchlist().getWatchlist(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the watchlist name to get the specific watchlist.
        /// The request will be sent it to samrtface platform using method retrievesAllWatchlist from class SubWatchlist to get all the watchlists then finding the watchlists by its name if it is existed it will be return to the user
        /// </summary>
        /// <param name="name">watchlist name</param>
        /// <returns>watchlist</returns>
        [Authorize]
        [HttpGet]
        [Route("Watchlist/getWatchlistByName")]
        public IActionResult getWatchlistByName(string name)
        {
            AllWatchlist watchlist = new SubWatchlist().retrievesAllWatchlist();
            int i;
            for (i = 0; i < watchlist.items.Length; i++)
            {
                if (watchlist.items[i].fullName.Trim().Equals(name.Trim())) break;
            }

            return Json(watchlist.items[i]);
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the watchlist.
        /// Then, the response will be the information of the deleted watchlist.
        /// The request will be sent it to samrtface platform using method deleteWatchlist from class SubWatchlist then the result will return it to the user 
        /// </summary>
        /// <param name="id">watchlist id</param>
        /// <returns>deleted watchlist</returns>
        [Authorize]
        [HttpDelete]
        [Route("Watchlist/delete")]
        public IActionResult deleteWatchlist(string id)
        {
            return Json(new SubWatchlist().deleteWatchList(id));
        }

        #endregion

        /// <summary>
        /// WatchlistMember region have all the WatchlistMember operation
        /// </summary>

        #region WatchlistMember

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the member id to get the specific member information.
        /// The request will be sent it to samrtface platform using method getWatchlistMember from class SubWatchlistMember then the result will return it to the user contain the member information
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>watchlist member</returns>
        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMember")]
        public IActionResult getWatchlistMember(string id)
        {
            return Json(new SubWatchlistMember().getWatchlistMember(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the member after that the watchlistmemberId will be retrieve from the SmartfaceLink table using getMemberId method from DBConnection class.
        /// The request will be sent it to samrtface platform using method updateWatchlistMember from class SubWatchlistMember then the result will return it to the user contain the updated member information
        /// </summary>
        /// <param name="member">member data</param>
        /// <returns>updated watchlist member</returns>
        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/update")]
        public IActionResult updateWatchlistMember(string member)
        {
            WatchlistMember watchlistMember = JsonConvert.DeserializeObject<WatchlistMember>(member);
            watchlistMember.id = _dbConnection.getMemberId(int.Parse(watchlistMember.note.Split(',')[2])).Trim();
            return Json(new SubWatchlistMember().updateWatchListMember(watchlistMember.id,
                watchlistMember.displayName, watchlistMember.fullName, watchlistMember.note));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the id of the member after that the watchlistmemberId will be retrieve from the SmartfaceLink table using getMemberId method from DBConnection class.
        /// Then, the member will be deleted from SmartfaceLink table using deleteMemberById method from DBConnection class.
        /// Finally, the request will be sent it to samrtface platform using method deleteWatchlistMember from class SubWatchlistMember then the result will return it to the user
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>deleted member</returns>
        [Authorize]
        [HttpDelete]
        [Route("WatchlistMember/delete")]
        public IActionResult deleteWatchlistMember(int id)
        {
            string watchlistMemberId = _dbConnection.getMemberId(id).Trim();
            _dbConnection.deleteMemberById(id);
            return Json(new SubWatchlistMember().deleteWatchListMember(watchlistMemberId));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The request will be sent it to samrtface platform using method retrieveAllWatchlistMembers from class SubWatchlistMember then the result will return it to the user contain all members information
        /// </summary>
        /// <returns>all watchlist members</returns>
        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/GetAllWatchlistMembers")]
        public IActionResult getAllWatchlistMembers()
        {
            Members watchlistMember = new SubWatchlistMember().retrievesAllWatchlistMembers();
            return Json(watchlistMember.items);
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the member id after that, the request will be sent it to samrtface platform using method
        /// getMemberFace from class SubWatchlistMember then the response will contain the face information and the image data will be retrieved and converted
        /// to base64 string using retrieveImage method from SubWatchlistMember class and the result will be returned to the user contain the image data 
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>member face data</returns>
        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMemberFace")]
        public IActionResult getMemberFace(string id)
        {
            return Json(new SubWatchlistMember().getMemberFace(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the member id after that, the request will be sent it to samrtface platform using method
        /// getMemberFace from class SubWatchlistMember then the response will contain the all the member faces information
        /// and the image data will be retrieved and converted to base64 string using retrieveImage method from SubWatchlistMember
        /// class for each image and the result will be returned to the user contain array of images data
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>member face</returns>
        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getFaces")]
        public IActionResult getWatchlistMemberFaces(string id)
        {
            return Json(new SubWatchlistMember().getFaces(id));
        }

        /// <summary>
        /// The user should be authorized to access this API request.
        /// The user should provide the member display name, full name, notes, watchlist id, and image.
        /// First, we send a request to smartface platform to create a new watchlistMember using createWatchlistMember method from SubWatchlistMember.
        /// Then we link and set the id of the employee with the watchlistMember id in SmartfaceLink table using setMemberId method from DBConnection class.
        /// Finally, send the request to smartface platform to register the member in the watchlist and send the image data.
        /// </summary>
        /// <param name="memberRegistration"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/CreateAndResgister")]
        public IActionResult createWatchlistMember([FromBody] MemberRegistration memberRegistration)
        {
            WatchlistMember watchlistMember =
                new SubWatchlistMember().createWatchListMember(memberRegistration.watchlistMember.displayName,
                    memberRegistration.watchlistMember.fullName, memberRegistration.watchlistMember.note);
            _dbConnection.setMemberId(int.Parse(memberRegistration.watchlistMember.note.Split(',')[2]),
                watchlistMember.id);
            return Json(new SubWatchlistMember().registerNewMember(watchlistMember.id, memberRegistration.watchlistId,
                memberRegistration.img));
        }

        #endregion
    }
}