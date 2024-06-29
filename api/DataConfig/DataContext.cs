using api.Models;
using Microsoft.EntityFrameworkCore;
namespace api.DataConfig
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
    }
}
