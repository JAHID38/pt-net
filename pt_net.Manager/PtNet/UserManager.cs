using pt_net.Entity.EntityModels;
using pt_net.Gateway.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pt_net.Manager.PtNet
{
    public class UserManager : IUserService
    {
        TestGateway gateway = new TestGateway();

        public User addUser(User user)
        {
            return gateway.addUser(user);
        }

        public User authenticate(User user)
        {
            return gateway.authenticate(user);
        }

        public int save(User user)
        {
            return gateway.saveUser(user);
        }

        public User user(int id)
        {
            return userList().Where(x=>x.id == id).FirstOrDefault();
        }

        public User user(string username)
        {
            return userList().Where(u => u.username.ToLower().Trim().Equals(username)).FirstOrDefault();
        }

        public  List<User> userList()
        {
            return gateway.getUserList();
        }
    }
}
