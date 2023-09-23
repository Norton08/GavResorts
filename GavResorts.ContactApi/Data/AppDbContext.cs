using GavResorts.ContactApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GavResorts.ContactApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Contacts> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    => optionsBuilder.UseSqlite("DataSource=gavresortscontactsdb.db;Cache=Shared");
}
