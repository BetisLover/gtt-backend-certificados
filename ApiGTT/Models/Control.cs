using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGTT.Models
{
    public class Control
    {
        public long status { get; set; }
        public string comment { get; set; }
        public string jwt { get; set; }
        public long user_id { get; set; }
        public Role role { get; set; }

        public Control(long status, string comment, string jwt, long user_id, Role role) {
            this.status = status;
            this.comment = comment;
            this.jwt = jwt;
            this.user_id = user_id;
            this.role = role;
        }
    }
}
