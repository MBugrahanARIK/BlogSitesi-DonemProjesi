using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Comment
    {
        public int id { get; set; }
        [ForeignKey(nameof(Content))]
        public int contentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string title { get; set; }
        public string mail { get; set; }
        public string comment { get; set; }
        public Content Content { get; set; }
        public DateTime date { get; set; }
        public byte status { get; set; }
    }
}
