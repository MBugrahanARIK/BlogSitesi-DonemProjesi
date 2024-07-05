namespace WebApplication1.Models
{
    public class KeyWords
    {
        public int id { get; set; }
        public string keyWord { get; set; }
        public ICollection<KeyWordContainer> ContentKeywords { get; set; }
    }
}
