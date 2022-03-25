using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace pt_net.Gateway
{
  public  class ConnectionGateway
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        private readonly string _connectionString;
        public ConnectionGateway()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(projectPath).AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetSection("DataBase").GetSection("NStar").Value;


            Connection = new SqlConnection(_connectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
    }
}
