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

        public IEnumerable<Game> Games => context.Games;

        public void SaveProdukt(Game game)
        {
            if (game.GameId == 0)
                context.Games.Add(game);
            else
            {
                Game dbEntry = context.Games.Find(game.GameId);
                if(dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
