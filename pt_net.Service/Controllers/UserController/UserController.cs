using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pt_net.Entity.EntityModels;
using pt_net.Manager.PtNet;
using System.Collections.Generic;

namespace pt_net.Service.Controllers.UserController
{
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(IWebHostEnvironment hostEnvironment, IUserService _userService)
        {
            this._hostEnvironment = hostEnvironment;
            userService = _userService;

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
        public User add (string name)
        {
            User user = new User()
            {
                name = name,
                status = 1
            };
            //user status =  1: ACTIVE
            return (string.IsNullOrEmpty(user.name)) ? null : userService.addUser(user);
        }
    }

}
