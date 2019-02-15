using OnlineGameLaden.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameLaden.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}
