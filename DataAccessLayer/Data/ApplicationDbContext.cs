using DataAccessLayer.Enitites;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options) { }
    public DbSet<TodoItem> TodoItems { get; set; }
}
