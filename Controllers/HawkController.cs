using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
            Task<List<MatchResult>> matchs = new SubMatchFaces().matchFaces();
            return null;
         }
        [HttpPost]
        [Route("WatchlistMember/create")]
        public IActionResult createWatchlistMember(string displayName, string fullName, string note)
        {
            WatchlistMember watchlistMember =
                new SubWatchlistMember().createWatchListMember(displayName,
                    fullName, note);
            return Json(watchlistMember);
        }
        
        [HttpPut]
        [Route("WatchlistMember/update")]
        public IActionResult updateWatchlistMember(string id, string displayName, string fullName, string note)
        {
            id = "916255be-0ca7-43e9-ab7e-727c665bbd7a";
            displayName = "t";
            fullName = "e";
            note = "dfd";
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
        
        [HttpPost]
        [Route("WatchlistMember/register")]
        public IActionResult registerWatchlistMember(string imgUrl, string id,
            string watchlistId)
        {
            string registeredWatchlistMember =
                new SubWatchlistMember().register(id, watchlistId, imgUrl);
            return Json(registeredWatchlistMember);
        }
        
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
    }
}