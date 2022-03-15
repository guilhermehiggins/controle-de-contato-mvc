using ControleDeContato.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContato.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
