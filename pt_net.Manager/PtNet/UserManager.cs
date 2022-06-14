using pt_net.Entity.EntityModels;
using pt_net.Gateway.Connection;
using System;
using System.Collections.Generic;
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

        public int save(User user)
        {
            return gateway.saveUser(user);
        }

        public User user(int id)
        {
            return userList().Find(x=>x.id == id);
        }

        public  List<User> userList()
        {
            return gateway.getUserList();
        }
    }
}
