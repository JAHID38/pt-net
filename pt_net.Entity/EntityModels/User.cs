using System;
using System.Collections.Generic;
using System.Text;

namespace pt_net.Entity.EntityModels
{

    public class User
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string statusCode { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}
