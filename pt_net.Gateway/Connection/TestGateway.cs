using pt_net.Entity.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace pt_net.Gateway.Connection
{
    public class TestGateway : ConnectionGateway
    {
        public int saveUser(User user)
        {
            try
            {
                int userId = -1;
                Query = @"INSERT INTO tbl_user(name, status, create_date, update_date) VALUES (N'" +user.name +"', " +user.status +", getdate(), getdate() ); SELECT SCOPE_IDENTITY() as userId";

                Command.CommandText = Query;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    Reader.Read();
                    userId = Convert.ToInt32(Reader["userId"]);
                }
                Reader.Close();
                Connection.Close();

                return userId;
            }
            finally
            {
                if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }
        }

        public User addUser(User user)
        {
            try
            {
                int userId = -1;
                Query = @"INSERT INTO tbl_user(name, status, create_date, update_date) VALUES (N'" +user.name +"', " +user.status +", getdate(), getdate() ); SELECT SCOPE_IDENTITY() as userId";

                Command.CommandText = Query;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    Reader.Read();
                    userId = Convert.ToInt32(Reader["userId"]);
                }
                Reader.Close();
                Connection.Close();

                User hi =  getUserList().Find(x=> x.id == userId);
                return hi;
            }
            finally
            {
                if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }
        }

        public List<User> getUserList()
        {
            try
            {
                Query = @"select * from tbl_user";
                Command.CommandText = Query;
                Command.Parameters.Clear();
                Connection.Open();
                Reader = Command.ExecuteReader();

                User user = null;
                List<User> users = new List<User>();

                while(Reader.Read())
                {
                    user = new User()
                    {
                        id = (int)Reader["id"],
                        name = Reader["name"].ToString(),
                        status = (int)Reader["status"],
                        createDate = Convert.ToDateTime(Reader["create_date"]),
                        updateDate = Convert.ToDateTime(Reader["update_date"])
                    };
                    user.statusCode = (user.status == 1) ? "ACTIVE" : "INACTIVE";
                    users.Add(user);
                }
                Reader.Close();
                Connection.Close();

                return users;
            }
            finally
            {
                if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }
        }


    }
}
