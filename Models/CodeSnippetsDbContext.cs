using Microsoft.EntityFrameworkCore;

namespace CodeSnippetSaver.Models
{
  public class CodeSnippetsDbContext : DbContext
  {
    public DbSet<Code> CodeSnippets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlite("DataSource=Code.db");
    }
  }
}