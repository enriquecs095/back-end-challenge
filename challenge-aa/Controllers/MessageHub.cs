﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using challenge_aa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using challenge_aa.Controllers;

namespace challenge_aa.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController:Hub {
        public Models.postgresContext db;

        public async Task SendMessage(int userId, string message, int rol) {
            await Clients.Group("G" + userId).SendAsync("Send", message, rol);
        }
        public async Task userToGroup(int userId) {
            await Groups.AddToGroupAsync(Context.ConnectionId, "G" + userId);
        }
    }
}
