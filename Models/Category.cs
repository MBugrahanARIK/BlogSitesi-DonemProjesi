namespace WebApplication1.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Content> contents { get; set; }
    }
}
