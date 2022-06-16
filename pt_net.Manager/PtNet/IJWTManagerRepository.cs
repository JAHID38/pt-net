using pt_net.Entity.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace pt_net.Manager.PtNet
{
    public interface  IJWTManagerRepository
    {
        Tokens Authenticate(User user);
    }
}
