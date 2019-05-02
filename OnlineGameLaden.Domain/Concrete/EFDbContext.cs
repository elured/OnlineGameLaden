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
        public EFDbContext()// : base("GameStore")
        {
            
            //var initialister = new EFDbInitializer();
            Database.SetInitializer<EFDbContext>(new EFDbInitializer());
        }

        public DbSet<Game> Games { get; set; }
    }

    public class EFDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {

        protected override void Seed(EFDbContext context)
        {

            List<Game> games = new List<Game>
            {
                new Game{Name ="SimCity", Description = "Градостроительный симулятор снова с вами! Создайте город своей мечты", Category = "Симулятор", Price= 1499.00M},
                new Game{Name ="TITANFALL", Description = "Эта игра перенесет вас во вселенную, где малое противопоставляется большому, природа – индустрии, а человек – машине", Category = "Шутер", Price= 2299.00M},
                new Game{Name ="Battlefield 4", Description = "Battlefield 4 – это определяющий для жанра, полный экшена боевик, известный своей разрушаемостью, равных которой нет", Category = "Шутер", Price= 899.40M},
                new Game{Name ="The Sims 4", Description = "В реальности каждому человеку дано прожить лишь одну жизнь. Но с помощью The Sims 4 это ограничение можно снять!  Вам решать — где, как и с кем жить, чем заниматься, чем украшать и обустраивать свой дом", Category = "Симулятор", Price= 15.00M},
                new Game{Name ="Dark Souls 2", Description = "Продолжение знаменитого ролевого экшена вновь заставит игроков пройти через сложнейшие испытания. Dark Souls II предложит  нового героя, новую историю и новый мир. Лишь одно неизменно – выжить в мрачной вселенной Dark Souls очень непросто.", Category = "RPG", Price= 949.00M},
                new Game{Name ="The Elder Scrolls V: Skyrim", Description = "После убийства короля Скайрима империя оказалась на грани катастрофы. Вокруг претендентов на престол сплотились новые союзы, и разгорелся конфликт. К тому же, как предсказывали древние свитки, в мир вернулись жестокие и беспощадные драконы. Теперь будущее Скайрима и всей империи зависит от драконорожденного — человека, в жилах которого течет кровь легендарных существ.", Category = "RPG", Price= 1399.00M},
                new Game{Name ="FIFA 14", Description = "Достоверный, красивый, эмоциональный футбол! Проверенный временем геймплей FIFA стал ещё лучше благодаря инновациям, поощряющим творческую игру в центре поля и позволяющим задавать её темп.", Category = "Симулятор", Price= 699.00M},
                new Game{Name ="Need for Speed Rivals", Description = "Забудьте про стандартные режимы игры. Сотрите грань между одиночным и многопользовательским режимом в постоянном соперничестве между гонщиками и полицией. Свободно войдите в мир, в котором ваши друзья уже участвуют в гонках и погонях.", Category = "Симулятор", Price= 15.00M},
                new Game{Name ="Crysis 3", Description = "Действие игры разворачивается в 2047 году, а вам предстоит выступить в роли Пророка.", Category = "Шутер", Price= 1299.00M},
                new Game{Name ="Dead Space 3", Description = "В Dead Space 3 Айзек Кларк и суровый солдат Джон Карвер отправляются в космическое путешествие, чтобы узнать о происхождении некроморфов.", Category = "Шутер", Price= 499.00M}
                //new Game{Name ="", Description = "", Category = "", Price= 1M},
            };

            context.Games.AddRange(games);
            //foreach (Game game in games)
            //    context.Games.Add(game);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
