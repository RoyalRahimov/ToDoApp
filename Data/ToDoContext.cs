using Microsoft.EntityFrameworkCore;
using ToDoTasksApi.Models;

namespace ToDoTasksApi.Data;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    public DbSet<ToDoTask> ToDoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>()
            .OwnsOne(t => t.TaskPriorityLevel); // Mark Priority as an owned type
    }
}
