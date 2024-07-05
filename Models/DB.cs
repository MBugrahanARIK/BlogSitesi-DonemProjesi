using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class DB
    {
        DbContext context;
        DB(DbContext context)
        {
            context = context;
        }
    }
}
