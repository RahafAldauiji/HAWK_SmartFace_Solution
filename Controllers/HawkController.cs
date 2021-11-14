﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartfaceSolution.Classes;
using SmartfaceSolution.Repositories;
using SmartfaceSolution.SubClasses;
using WebApi.Models;
using WebApi.Services;

namespace SmartfaceSolution.Controllers
{
    [Produces("application/json")]
    [Route("HAWK")]
    [ApiController]
    public class HawkController : Controller
    {
        private IUserService _userService;

        public HawkController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(response);
        }

        /////////////////////////////////
        ///

        #region Camera

        [Authorize]
        [HttpGet]
        [Route("Camera/getCamera")]
        public async Task<IActionResult> getCamera(string id)
        {
            Camera camera = await new SubCamera().getCamera(id);
            return Json(camera);
        }

        [Authorize]
        [HttpGet]
        [Route("Camera/getAllCameras")]
        public async Task<IActionResult> getAllCameras()
        {
            List<Camera> cameras = await new SubCamera().getAllCameras();
            return Json(cameras);
        }

        [Authorize]
        [HttpPost]
        [Route("Camera/create")]
        public async Task<IActionResult> createCamera(string rtsp, string cameraName)
        {
            Camera camera = await new SubCamera().createCamera(rtsp: rtsp, cameraName: cameraName);
            return Json(camera);
        }

        // [Authorize]
        // [HttpPut] NOT WORKING
        // [Route("Camera/update")]
        // public IActionResult updateCamera(string cam)
        // {
        //     Camera camera = new SubCamera().updateCamera(cam);
        //     return Json(camera);
        // }
        [Authorize]
        [HttpDelete]
        [Route("Camera/delete")]
        public async Task<IActionResult> deleteCamera(string id)
        {
            Camera camera = await new SubCamera().deleteCamera(id);
            return Json(camera);
        }

        [Authorize]
        [HttpGet]
        [Route("Camera/Match")]
        public async Task<IActionResult> getMatch()
        {
            List<MatchFaces> matchs = await new SubMatchFaces().matchFaces();
            return Json(matchs);
        }

        #endregion

        #region Watchlist

        [Authorize]
        [HttpPost]
        [Route("Watchlist/create")]
        public async Task<IActionResult> createWatchlist(string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            Watchlist watchlist = await new SubWatchlist().createWatchList(watchlistDisplayName, watchlistFullName,
                watchlistThreshold);
            return Json(watchlist);
        }

        [Authorize]
        [HttpGet]
        [Route("Watchlist/getMembers")]
        public async Task<IActionResult> getWatchlistMembers(string id)
        {
            Members watchlistMembers = await new SubWatchlist().retrievesWatchlistMembers(id);
            return Json(watchlistMembers);
        }

        [Authorize]
        [HttpPut]
        [Route("Watchlist/upadte")]
        public async Task<IActionResult> updateWatchlist(string watchlistId, string watchlistDisplayName,
            string watchlistFullName, int watchlistThreshold)
        {
            Watchlist updatedwatchlist = await new SubWatchlist().updateWatchList(watchlistId, watchlistDisplayName,
                watchlistFullName, watchlistThreshold);
            return Json(updatedwatchlist);
        }

        [Authorize]
        [HttpDelete]
        [Route("Watchlist/delete")]
        public async Task<IActionResult> deleteWatchlist(string watchlistId)
        {
            string deletedwatchlist = await new SubWatchlist().deleteWatchList(watchlistId);
            return Json(deletedwatchlist);
        }

        #endregion

        #region WatchlistMember

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMember")]
        public async Task<IActionResult> getWatchlistMember(string id)
        {
            WatchlistMember watchlistMember = await new SubWatchlistMember().getWatchlistMember(id);
            return Json(watchlistMember);
        }

        [Authorize]
        [HttpPut]
        [Route("WatchlistMember/update")]
        public async Task<IActionResult> updateWatchlistMember(string id, string displayName, string fullName,
            string note)
        {
            // id = "916255be-0ca7-43e9-ab7e-727c665bbd7a";
            // displayName = "t";
            // fullName = "e";
            // note = "dfd";
            WatchlistMember updatedwatchlistMember = await new SubWatchlistMember().updateWatchListMember(id,
                displayName, fullName, note);
            return Json(updatedwatchlistMember);
        }

        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/link")]
        public async Task<IActionResult> linkWatchlistMember(string watchlistMember,
            string watchlistId)
        {
            string linkedwatchlistMember =
                await new SubWatchlistMember().linkWatchListMember(watchlistId, watchlistMember);
            return Json(linkedwatchlistMember);
        }

        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/unlink")]
        public async Task<IActionResult> unlinkWatchlistMember(string watchlistMember,
            string watchlistId)
        {
            string linkedwatchlistMember =
                await new SubWatchlistMember().unlinkWatchListMember(watchlistId, watchlistMember);
            return Json(linkedwatchlistMember);
        }

        [Authorize]
        [HttpDelete]
        [Route("WatchlistMember/delete")]
        public async Task<IActionResult> deleteWatchlistMember(string watchlistMemberId)
        {
            string deletedwatchlistMember = await new SubWatchlistMember().deleteWatchListMember(watchlistMemberId);
            return Json(deletedwatchlistMember);
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/GetAllWatchlistMembers")]
        public async Task<IActionResult> getAllWatchlistMembers(string watchlistMemberId)
        {
            Members watchlistMember = await new SubWatchlistMember().retrievesAllWatchlistMembers();

            return Json(watchlistMember.items);
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getMemberFace")]
        public async Task<IActionResult> getMemberFaces(string id)
        {
            string img = await new SubWatchlistMember().getMemberFace(id);
            return Json(img);
        }

        [Authorize]
        [HttpGet]
        [Route("WatchlistMember/getFaces")]
        public async Task<IActionResult> getWatchlistMemberFaces(string id)
        {
            List<string> faces = await new SubWatchlistMember().getFaces(id);
            return Json(faces);
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
            string registeredWatchlistMember =
                await new SubWatchlistMember().registerNewMember(watchlistMember.id, watchlistId, imgUrl);
            return Json(registeredWatchlistMember);
        }

        //cc9c8016-3489-49f1-8e2d-842c7dae3431 :S
        //90ca71c3-2247-47a2-a78d-6a97ac5a1540 :E
        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/addFace")]
        public async Task<IActionResult> addFace(string watchlistMemberId, string imgUrl)
        {
            Face face = await new SubWatchlistMember().addNewFace(watchlistMemberId, imgUrl);
            return Json(face);
        }

        [Authorize]
        [HttpPost]
        [Route("WatchlistMember/removeFace")]
        public async Task<IActionResult> removeFace(string id, string faceId)
        {
            string removedFace = await new SubWatchlistMember().removeFace(id, faceId);
            return Json(removedFace);
        }

        #endregion
    }
}