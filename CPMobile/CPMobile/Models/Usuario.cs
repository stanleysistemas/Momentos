using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moments.Models
{
    public class Usuario
    {

        public int idUser { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public bool emailConfirmed { get; set; }
        public int level { get; set; }
        public string joinDate { get; set; }
        public string message { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool logado { get; set; }

    }
}
