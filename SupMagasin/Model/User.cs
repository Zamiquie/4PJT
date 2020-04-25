using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Model
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool RealyUser { get; set; }
        public string ID { get; set; }
    }
}
