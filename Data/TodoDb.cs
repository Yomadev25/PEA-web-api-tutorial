using Microsoft.EntityFrameworkCore;
using PEAApi.Models;

namespace PEAApi.Data
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) :base(options){}
        public DbSet<Todo> Todos {get; set;}
    }
}