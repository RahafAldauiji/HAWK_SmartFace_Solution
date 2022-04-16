using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using SmartfaceSolution.Entities;

namespace SmartfaceSolution.SubEntities
{
    /// <summary>
    /// <c>SubMatchFaces</c> provide all the operation that are necessary to operate in the MatchFaces entity 
    /// </summary>
    public class SubMatchFaces
    {
        /// <summary>
        /// Method <c>matchFaces</c> check if there is a any match members
        /// it listen to smartface notification API using ZeroMQ messaging library
        /// The ZeroMQ open a socket on TCP port 2406 for the communication 
        /// </summary>
        ///<returns>The WatchlistMember member</returns>
        public async Task<MemberMatch> matchFaces()
        {
            MemberMatch memberMatch;
            using (var subscriber = new SubscriberSocket())
            {
                //  listen to smartface notification API using ZeroMQ messaging library 
                subscriber.Connect("tcp://127.0.0.1:2406");
                subscriber.Subscribe("matchResults.match");
                var topic = subscriber.ReceiveFrameString(); // matchResults
                var matchHit = subscriber.ReceiveFrameString(); // This notification contains watchlist member
                memberMatch = JsonSerializer.Deserialize<MemberMatch>(matchHit);
                if (memberMatch.Type.Equals("Match")) return memberMatch;
            }

            return null;
        }
    }
}