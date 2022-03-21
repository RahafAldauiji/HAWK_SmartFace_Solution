using System;
using System.Collections.Generic;
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
    [Produces("application/json")]
    [Route("HAWK")]
    [ApiController]
    [EnableCors("AnotherPolicy")]
    public class HawkController : Controller
    {
        private IUserService _userService;

        public HawkController(IUserService userService)
        {
            _userService = userService;
        }

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
            var response = _userService.Authenticate(user);
            return response == null ? throw new AppException("Invalid User") : Json(response);
        }

        /// <summary>
        /// Camera region have all the Camera operation
        /// </summary>

        #region Camera

        [Authorize]
        [HttpGet]
        [Route("Camera/getCamera")]
        public async Task<IActionResult> getCamera(string id)
        {
            return Json(await new SubCamera().getCamera(id));
        }

        [Authorize]
        [HttpGet]
        [Route("Camera/getAllCameras")]
        public async Task<IActionResult> getAllCameras()
        {
            return Json(await new SubCamera().getAllCameras());
        }

        [Authorize]
        [HttpPost]
        [Route("Camera/create")]
        public async Task<IActionResult> createCamera([FromBody] Camera cam)
        {
            return Json(await new SubCamera().createCamera(rtsp: cam.source, cameraName: cam.name));
        }

        [Authorize]
        [HttpPost]
        [Route("Camera/update")]
        public async Task<IActionResult> updateCamera(string camera)
        {
            Camera cam = JsonConvert.DeserializeObject<Camera>(camera);
            Camera updatedCamera = await new SubCamera().updateCamera(cam);
            return Json(updatedCamera);
        }

        [Authorize]
        [HttpDelete]
        [Route("Camera/delete")]
        public async Task<IActionResult> deleteCamera(string id)
        {
            return Json(await new SubCamera().deleteCamera(id));
        }

        [Authorize]
        [HttpGet]
        [Route("Camera/Match")]
        public async Task<IActionResult> getMatch()
        {
            // TestMember matchs =await new MatchService().matchFaces();
            MemberMatch match = await new SubMatchFaces().matchFaces();
            Console.WriteLine(match.Id);
            return Json(match);
        }

        #endregion

        /// <summary>
        /// Watchlist region have all the Watchlist operation
        /// </summary>

        #region Watchlist

        [Authorize]
        [HttpPost]
        [Route("Watchlist/create")]
        public async Task<IActionResult> createWatchlist(string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            return Json(await new SubWatchlist().createWatchList(watchlistDisplayName, watchlistFullName,
                watchlistThreshold));
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getMembers")]
        public async Task<IActionResult> getWatchlistMembers(string id)
        {
            return Json(await new SubWatchlist().retrievesWatchlistMembers(id));
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getAllWatchlist")]
        public async Task<IActionResult> getAllWatchlist()
        {
            return Json(await new SubWatchlist().retrievesAllWatchlist());
        }

        [Authorize]
        [HttpPut]
        [Route("Watchlist/upadte")]
        public async Task<IActionResult> updateWatchlist(string watchlistId, string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            return Json(await new SubWatchlist().updateWatchList(watchlistId, watchlistDisplayName,
                watchlistFullName, watchlistThreshold));
        }

        [Authorize]
        [HttpDelete]
        [Route("Watchlist/delete")]
        public async Task<IActionResult> deleteWatchlist(string watchlistId)
        {
            return Json(await new SubWatchlist().deleteWatchList(watchlistId));
        }

        #endregion

        /// <summary>
        /// WatchlistMember region have all the WatchlistMember operation
        /// </summary>

        #region WatchlistMember

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMember")]
        public async Task<IActionResult> getWatchlistMember(string id)
        {
            return Json(await new SubWatchlistMember().getWatchlistMember(id));
        }

        [Authorize]
        [HttpPut]
        [Route("WatchlistMember/update")]
        public async Task<IActionResult> updateWatchlistMember(string id, string displayName, string fullName,
            string note)
        {
            return Json(await new SubWatchlistMember().updateWatchListMember(id,
                displayName, fullName, note));
        }

        // [Authorize]
        // [HttpPost]
        // [Route("WatchlistMember/link")]
        // public async Task<IActionResult> linkWatchlistMember(string watchlistMember,
        //     string watchlistId)
        // {
        //     string linkedwatchlistMember =
        //         await new SubWatchlistMember().linkWatchListMember(watchlistId, watchlistMember);
        //     return Json(linkedwatchlistMember);
        // }

        // [Authorize]
        // [HttpPost]
        // [Route("WatchlistMember/unlink")]
        // public async Task<IActionResult> unlinkWatchlistMember(string watchlistMember,
        //     string watchlistId)
        // {
        //     string linkedwatchlistMember =
        //         await new SubWatchlistMember().unlinkWatchListMember(watchlistId, watchlistMember);
        //     return Json(linkedwatchlistMember);
        // }

        [Authorize]
        [HttpDelete]
        [Route("WatchlistMember/delete")]
        public async Task<IActionResult> deleteWatchlistMember(string watchlistMemberId)
        {
            return Json(await new SubWatchlistMember().deleteWatchListMember(watchlistMemberId));
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/GetAllWatchlistMembers")]
        public async Task<IActionResult> getAllWatchlistMembers()
        {
            Members watchlistMember = await new SubWatchlistMember().retrievesAllWatchlistMembers();

            return Json(watchlistMember.items);
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMemberFace")]
        public async Task<IActionResult> getMemberFace(string id)
        {
            string img = await new SubWatchlistMember().getMemberFace(id);
            return Json(await new SubWatchlistMember().getMemberFace(id));
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getFaces")]
        public async Task<IActionResult> getWatchlistMemberFaces(string id)
        {
            List<string> faces = await new SubWatchlistMember().getFaces(id);
            return Json(await new SubWatchlistMember().getFaces(id));
        }

        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/CreateAndResgister")]
        public async Task<IActionResult> createWatchlistMember(string displayName, string fullName, string note,
            string watchlistId,
            string imgUrl)
        {
            WatchlistMember watchlistMember =
                await new SubWatchlistMember().createWatchListMember(displayName,
                    fullName, note);
            return Json(await new SubWatchlistMember().registerNewMember(watchlistMember.id, watchlistId, imgUrl));
        }


        // [Authorize]
        // [HttpPost]
        // [Route("WatchlistMember/addFace")]
        // public async Task<IActionResult> addFace(string watchlistMemberId, string imgUrl)
        // {
        //     Face face = await new SubWatchlistMember().addNewFace(watchlistMemberId, imgUrl);
        //     return Json(face);
        // }

        // [Authorize]
        // [HttpPost]
        // [Route("WatchlistMember/removeFace")]
        // public async Task<IActionResult> removeFace(string id, string faceId)
        // {
        //     string removedFace = await new SubWatchlistMember().removeFace(id, faceId);
        //     return Json(removedFace);
        // }

        #endregion
    }
}