using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Domain;

namespace WebApplication1.Data
{
    public class ContactlyDBContext : DbContext
    {
        public ContactlyDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
