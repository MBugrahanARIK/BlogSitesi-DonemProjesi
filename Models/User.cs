using System.Drawing;
using System.Net.Mail;

namespace WebApplication1.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string image { get; set; }
    }
}
