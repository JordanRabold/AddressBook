using AddressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Data
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions<AddressContext> options) : base(options)
        {
        }

        public DbSet<AddressClass> AddressClasses { get; set; }

    }
}
