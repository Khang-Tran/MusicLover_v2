﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MusicLover.WebApp.Server.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockUser(this Controller controller, string userId, string userName)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            }));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() {User = user}
            };
        }
    }
}
