using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenTokSDK;
using VideoChat.Dtos;
using VideoChat.Models;

namespace VideoChat.Controllers
{
    public class SessionController : Controller
    {
        private readonly IConfiguration _config;

        public SessionController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult GetSession([FromBody] RoomForm roomForm)
        {
            var apiKey = int.Parse(_config["ApiKey"]);
            var apiSecret = _config["ApiSecret"];
            var opentok = new OpenTok(apiKey, apiSecret);

            var roomName = roomForm.RoomName;
            string sessionId;
            string token;

            using (var db = new OpentokContext())
            {
                var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
                if (room != null)
                {
                    sessionId = room.SessionId;
                    token = opentok.GenerateToken(sessionId);
                    room.Token = token;
                    db.SaveChanges();
                }
                else
                {
                    var session = opentok.CreateSession();
                    sessionId = session.Id;
                    token = opentok.GenerateToken(sessionId);
                    var roomInsert = new Room
                    {
                        SessionId = sessionId,
                        Token = token,
                        RoomName = roomName
                    };
                    db.Add(roomInsert);
                    db.SaveChanges();
                }
            }
            return Json(new { sessionId = sessionId, token = token, apiKey = _config["ApiKey"] });
        }
    }
}