using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pt_net.Entity.EntityModels;
using pt_net.Entity.Utility;
using pt_net.Manager.PtNet;
using System.Collections.Generic;
using System.Linq;
using CoreApiResponse;
using System.Net;
using System;

namespace pt_net.Service.Controllers.UserController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IJWTManagerRepository _jWTManager;


        public UserController(IWebHostEnvironment hostEnvironment, IUserService _userService, IJWTManagerRepository jWTManager)
        {
            this._hostEnvironment = hostEnvironment;
            this.userService = _userService;
            this._jWTManager = jWTManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(User usersdata)
        {
            Tokens token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                //return Unauthorized();
                return CustomResult("Incorrect Credentials!", HttpStatusCode.Unauthorized);
            }
            else
            {
                if (token.Token.Equals("not_found"))
                {
                    return CustomResult("No user found with: " +usersdata.username, HttpStatusCode.NotFound);
                }
                else if (token.Token.Equals("inactive"))
                {
                    return CustomResult("Inactive user: " + usersdata.username, HttpStatusCode.BadRequest);
                }
            }
            return CustomResult("Authentication Successful", token, HttpStatusCode.OK);
        }

        [Route("user/list")]
        [HttpGet]
        public  IActionResult userList()
        {
            try
            {
                return CustomResult(userService.userList(), HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Unauthorized);
            }
        }
        
        [Route("user/list/active")]
        [HttpGet]
        public IActionResult activeUserList()
        {
            return CustomResult(userService.userList().Where(u => u.status.Equals(1)), HttpStatusCode.OK);
        }

        [Route("user")]
        [HttpGet]
        public IActionResult user(int id)
        {
            User user = userService.user(id);
            if (user == null)
            {
                return CustomResult("No user found with ID: " + id, HttpStatusCode.NotFound);
                //return NotFound("No User Found with ID: "+ id);
            }
            return CustomResult(user, HttpStatusCode.OK);
        }


        [Route("add/user")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult add (User payload)
        {
            if (userService.user(payload.name) != null)
            {
                return CustomResult(payload.name +" already exists!", HttpStatusCode.Conflict);
            }
            User user = null;
            if (!string.IsNullOrEmpty(payload.name))
            {
                user = new User()
                {
                    name = payload.name,
                    status = 1,
                    password = MD5.CreateMD5(payload.password.Trim())
                };
            }
            
            //user status =  1: ACTIVE
            return Created("created", userService.addUser(user));
        }
    }

}
