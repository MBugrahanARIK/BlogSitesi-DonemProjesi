using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Content
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public DateTime date { get; set; }
        public DateTime lastUpdate { get; set; }
        public DateTime releaseDate { get; set; }
        public bool isDeleted { get; set; }
        [ForeignKey(nameof(User))]
        public int userId { get; set; } // User anahtar sütunu
        public User user { get; set; }
        public Category Categories { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public ICollection<KeyWordContainer> keyWordContainer { get; set; }
    }
}
