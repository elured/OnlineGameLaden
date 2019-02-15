using OnlineGameLaden.Domain.Abstract;
using OnlineGameLaden.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGameLaden.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
    }
}
