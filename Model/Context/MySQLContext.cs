using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Todolist> Todolist { get; set; }
    }
}
