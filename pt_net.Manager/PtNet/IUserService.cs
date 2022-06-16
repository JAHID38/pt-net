using pt_net.Entity.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace pt_net.Manager.PtNet
{
    public interface  IUserService
    {
        public int save(User user);
        public User addUser(User user);
        public User authenticate(User user);
        public List<User> userList();
        public User user(int id);
    }
}
