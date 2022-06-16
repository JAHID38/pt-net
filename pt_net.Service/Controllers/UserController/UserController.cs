using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pt_net.Entity.EntityModels;
using pt_net.Entity.Utility;
using pt_net.Manager.PtNet;
using System.Collections.Generic;

namespace pt_net.Service.Controllers.UserController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IJWTManagerRepository _jWTManager;


        public UserController(IWebHostEnvironment hostEnvironment, IUserService _userService, IJWTManagerRepository jWTManager)
        {
            this._hostEnvironment = hostEnvironment;
            userService = _userService;
            this._jWTManager = jWTManager;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(User usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [Route("/api/user/list")]
        [HttpGet]
        public List<User> userList()
        {
            return userService.userList();
        }
        
        [Route("/api/user/list/active")]
        [HttpGet]
        public List<User> activeUserList()
        {
            return userService.userList().FindAll(u => u.status.Equals(1));
        }
        
        [Route("/api/user")]
        [HttpGet]
        public User user(int id)
        {
            return userService.user(id);
        }

        [Route("/api/save/user")]
        [HttpPost]
        public int save (User user)
        {
            //user status =  1: ACTIVE
            user.status = 1;
            return (string.IsNullOrEmpty(user.name)) ? -1 : userService.save(user);
        }

        [Route("/api/add/user")]
        [HttpPost]
        [AllowAnonymous]
        public User add (string name)
        {
            User user = null;
            if (!string.IsNullOrEmpty(name))
            {
                user = new User()
                {
                    name = name,
                    status = 1,
                    password = MD5.CreateMD5("2022")
                };
            }
            
            //user status =  1: ACTIVE
            return userService.addUser(user);
        }
    }

}
