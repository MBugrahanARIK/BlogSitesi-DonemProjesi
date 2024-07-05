using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public User user { get; set; }

        public DbSet<User> users { get; set; }
        public DbSet<Settings> settings { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<KeyWords> keyWords { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<KeyWordContainer> keyWordContainers { get; set; }
    }
}
