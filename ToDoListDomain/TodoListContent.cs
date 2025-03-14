using Microsoft.EntityFrameworkCore;
using ToDoListDomain.Enities;
using ToDoListDomain.Entities;

namespace ToDoListDomain
{
    public class TodoListContent : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskApp> Tasks { get; set; }

        public TodoListContent(DbContextOptions<TodoListContent> options) : base(options)
        {
        }

        public TodoListContent() { } // Конструктор без параметров для EF
    }
}
