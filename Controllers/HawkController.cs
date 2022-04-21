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
    [ApiController]
    [Produces("application/json")]
    [Route("Smartface")]
    [EnableCors("AnotherPolicy")]
    public class HawkController : Controller
    {
        private IUserService _user;
        private readonly ISearchDB _searchDb;

        public HawkController(IUserService user,ISearchDB searchDb)
        {
            _user = user;
            _searchDb = searchDb;
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
            return response == null ? throw new AppException("Invalid User") : Json(response.Result);
        }

        #endregion

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
        [Route("Match")]
        public async Task<IActionResult> getMatch()
        {
            MemberMatch match = await new SubMatchFaces().matchFaces();
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
        public async Task<IActionResult> createWatchlist([FromBody] Watchlist watchlist)
        {
            return Json(await new SubWatchlist().createWatchList(watchlist.displayName, watchlist.fullName,
                watchlist.threshold));
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getMembers")]
        public async Task<IActionResult> getWatchlistMembers(string id)
        {
            return Json((await new SubWatchlist().retrievesWatchlistMembers(id)).items);
        }

        [Authorize]
        [HttpPost]
        [Route("Watchlist/upadte")]
        public async Task<IActionResult> updateWatchlist(string watchlist)
        {
            Watchlist list = JsonConvert.DeserializeObject<Watchlist>(watchlist);
            return Json(await new SubWatchlist().updateWatchList(list.id, list.displayName,
                list.fullName, list.threshold));
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getAllWatchlist")]
        public async Task<IActionResult> getAllWatchlist()
        {
            return Json(await new SubWatchlist().retrievesAllWatchlist());
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getWatchlist")]
        public async Task<IActionResult> getWatchlist(string id)
        {
            return Json(await new SubWatchlist().getWatchlist(id));
        }


        [Authorize]
        [HttpDelete]
        [Route("Watchlist/delete")]
        public async Task<IActionResult> deleteWatchlist(string id)
        {
            return Json(await new SubWatchlist().deleteWatchList(id));
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
        [HttpPost]
        [Route("WatchlistMember/update")]
        public async Task<IActionResult> updateWatchlistMember(string member)
        {
            WatchlistMember watchlistMember = JsonConvert.DeserializeObject<WatchlistMember>(member);
            
            return Json(await new SubWatchlistMember().updateWatchListMember(watchlistMember.id,
                watchlistMember.displayName, watchlistMember.fullName, watchlistMember.note));
        }

        [Authorize]
        [HttpDelete]
        [Route("WatchlistMember/delete")]
        public async Task<IActionResult> deleteWatchlistMember(int id)
        {
            string watchlistMemberId=_searchDb.getMemberId(id).Trim();
            Console.WriteLine("============================"+watchlistMemberId);
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
            return Json(await new SubWatchlistMember().getMemberFace(id));
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getFaces")]
        public async Task<IActionResult> getWatchlistMemberFaces(string id)
        {
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
            _searchDb.setMemberId(int.Parse(note.Split(',')[2]),watchlistMember.id);
            return Json(await new SubWatchlistMember().registerNewMember(watchlistMember.id, watchlistId, imgUrl));
        }
       
        #endregion
    }
}