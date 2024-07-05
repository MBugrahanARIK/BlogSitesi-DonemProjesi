using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Settings
    {
        public int id { get; set; }
        public string siteMainTitle { get; set; }
        public string mainKeyWords { get; set; }
    }
}
