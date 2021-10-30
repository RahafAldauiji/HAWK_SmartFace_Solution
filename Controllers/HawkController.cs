using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartfaceSolution.Classes;
using SmartfaceSolution.SubClasses;

namespace SmartfaceSolution.Controllers
{
    [Produces("application/json")]
    [Route("HAWK")]
    [ApiController]
    public class HawkController : Controller
    {
        [HttpGet]
        [Route("Camera/Match")]
        public IActionResult getMatch()
        {
            List<MatchFaces> matchs = new SubMatchFaces().matchFaces();
            return Json(matchs);
        }

        [HttpPut]
        [Route("WatchlistMember/update")]
        public IActionResult updateWatchlistMember(string id, string displayName, string fullName, string note)
        {
            // id = "916255be-0ca7-43e9-ab7e-727c665bbd7a";
            // displayName = "t";
            // fullName = "e";
            // note = "dfd";
            WatchlistMember updatedwatchlistMember = new SubWatchlistMember().updateWatchListMember(id,
                displayName, fullName, note);
            return Json(updatedwatchlistMember);
        }

        [HttpPost]
        [Route("WatchlistMember/link")]
        public IActionResult linkWatchlistMember(string watchlistMember,
            string watchlistId)
        {
            string linkedwatchlistMember = new SubWatchlistMember().linkWatchListMember(watchlistId, watchlistMember);
            return Json(linkedwatchlistMember);
        }

        [HttpPost]
        [Route("WatchlistMember/unlink")]
        public IActionResult unlinkWatchlistMember(string watchlistMember,
            string watchlistId)
        {
            string linkedwatchlistMember =
                new SubWatchlistMember().unlinkWatchListMember(watchlistId, watchlistMember);
            return Json(linkedwatchlistMember);
        }

        [HttpDelete]
        [Route("WatchlistMember/delete")]
        public IActionResult deleteWatchlistMember(string watchlistMemberId)
        {
            string deletedwatchlistMember = new SubWatchlistMember().deleteWatchListMember(watchlistMemberId);
            return Json(deletedwatchlistMember);
        }

        // [HttpPost]
        // [Route("WatchlistMember/register")]
        // public IActionResult registerWatchlistMember(string imgUrl, string id,
        //     string watchlistId)
        // {
        //     string registeredWatchlistMember =
        //         new SubWatchlistMember().register(id, watchlistId, imgUrl);
        //     return Json(registeredWatchlistMember);
        // }
        [HttpPost]
        [Route("WatchlistMember/CreateAndResgister")]
        public IActionResult createWatchlistMember(string displayName, string fullName, string note, string watchlistId,
            string imgUrl)
        {
            WatchlistMember watchlistMember =
                new SubWatchlistMember().createWatchListMember(displayName,
                    fullName, note);
            string registeredWatchlistMember =
                new SubWatchlistMember().register(watchlistMember.Id, watchlistId, imgUrl);
            return Json(registeredWatchlistMember);
        }

        //cc9c8016-3489-49f1-8e2d-842c7dae3431 :S
        //90ca71c3-2247-47a2-a78d-6a97ac5a1540 :E
        [HttpPost]
        [Route("WatchlistMember/addFace")]
        public IActionResult addFace(string watchlistMemberId, string imgUrl)
        {
            Face face = new SubWatchlistMember().addNewFace(watchlistMemberId, imgUrl);
            return Json(face);
        }

        [HttpPost]
        [Route("WatchlistMember/removeFace")]
        public IActionResult removeFace(string id, string faceId)
        {
            string removedFace = new SubWatchlistMember().removeFace(id, faceId);
            return Json(removedFace);
        }

        [HttpPost]
        [Route("Watchlist/create")]
        public IActionResult createWatchlist(string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            Watchlist watchlist = new SubWatchlist().createWatchList(watchlistDisplayName, watchlistFullName,
                watchlistThreshold);
            return Json(watchlist);
        }

        [HttpPut]
        [Route("Watchlist/upadte")]
        public IActionResult updateWatchlist(string watchlistId, string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            Watchlist updatedwatchlist = new SubWatchlist().updateWatchList(watchlistId, watchlistDisplayName,
                watchlistFullName, watchlistThreshold);
            return Json(updatedwatchlist);
        }

        [HttpDelete]
        [Route("Watchlist/delete")]
        public IActionResult deleteWatchlist(string watchlistId)
        {
            string deletedwatchlist = new SubWatchlist().deleteWatchList(watchlistId);
            return Json(deletedwatchlist);
        }

        [HttpGet]
        [Route("Camera/getCamera")]
        public IActionResult getCamera(string id)
        {
            Camera camera = new SubCamera().getCamera(id);
            return Json(camera);
        }

        [HttpGet]
        [Route("Camera/getAllCameras")]
        public IActionResult getAllCameras()
        {
            List<Camera> cameras = new SubCamera().getAllCameras();
            return Json(cameras);
        }

        [HttpPost]
        [Route("Camera/createCamera")]
        public IActionResult createCamera(string rtsp, string cameraName)
        {
            Camera camera = new SubCamera().createCamera(rtsp: rtsp, cameraName: cameraName);
            return Json(camera);
        }

        // [HttpPut] NOT WORKING
        // [Route("Camera/updateCamera")]
        // public IActionResult updateCamera(string cam)
        // {
        //     Camera camera = new SubCamera().updateCamera(cam);
        //     return Json(camera);
        // }

        [HttpDelete]
        [Route("Camera/deleteCamera")]
        public IActionResult deleteCamera(string id)
        {
            Camera camera = new SubCamera().deleteCamera(id);
            return Json(camera);
        }
    }
}