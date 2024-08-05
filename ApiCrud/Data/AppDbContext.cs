using ApiCrud.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskBody> ToDoList { get; set; } // Referência da tabela onde são registradas as tarefas

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string targetServer = "DESKTOP-RO7KJA3";
            string targetDatabase = "databasetest";

            optionsBuilder.UseSqlServer(
                $"Server={targetServer};Database={targetDatabase};Trusted_Connection=True;TrustServerCertificate=Yes;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
