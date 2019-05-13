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

    public class EFDbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {

        protected override void Seed(EFDbContext context)
        {

            List<Game> games = new List<Game>
            {
                new Game{Name ="SimCity", Description = "1989 veröffentlichte EA einen Meilenstein der Spielegeschichte und Grundstein der Aufbau Simulation: SimCity. Knapp 20 Jahre später stellt der Entwickler nun die Städtebau", Category = "Simulation", Price= 15.00M},
                new Game{Name ="TITANFALL", Description = "Im Vorfeld sind schon ein paar Infos über Titanfall geleakt. Es wird mit der Source-Engine laufen, die selbst nach neun Jahren noch unglaubliche Ergebnisse liefert, die PlayStation 4 wird vom Release ausgeschlossen und Titanfall wird nur einen Multiplayermodus beinhalten. Die Mitbegründer von „Infinity Ward“ liefern uns mit Titanfall einen sehr action-lastigen Multiplayer-Shooter für die Xbox One", Category = "Ego Shooter", Price= 25.00M},
                new Game{Name ="Battlefield 4", Description = "Du bist bereit für die Schlacht? Dann ist die Battlefield 4 Premium Edition der perfekte Einstieg. Die Premium Edition gewährleistet, dass du alles hast, was du brauchst, um dich deinen Freunden anzuschließen und dich in First-Person-Schlachten für 64 Spieler zu stürzen.", Category = "Ego Shooter", Price= 10.49M},
                new Game{Name ="The Sims 4", Description = "Die Sims steht seit jeher für eine echtzeit-strategische Lebenssimulation, in der ihr die Kontrolle über das Leben virtueller Figuren übernehmt. Dabei lässt sich sowohl das Arbeitsleben, als auch das soziale Leben der Spielfiguren komplett steuern und organisieren. Mit Die Sims 4 setzt EA konsequent die Serie um das künstliche Leben im PC fort.", Category = "Simulation", Price= 15.00M},
                new Game{Name ="Dark Souls 2", Description = "Unter den zahlreichen mysteriösen Charakteren, die euch im Rollenspiel Dark Souls 2 begegnen, ist Ornifex eine der wichtigsten. Das Mischwesen halb Mensch, halb Krähe stellt einen wichtigen Händler dar, der euch zudem einige Bosswaffen schmieden kann. Wir verraten euch Standort und Funktion von Ornifex.", Category = "RPG", Price= 5.00M},
                new Game{Name ="The Elder Scrolls V: Skyrim", Description = "Mit The Elder Scrolls V: Skyrim begibt sich Bethesda Softworks in die Richtung massentauglicher Rollenspiele. Mit dem fünften Teil um die Alten Schriftrollen Tamriels ist ihnen der Schritt gelungen, Fantasy-Rollenspiele wieder salonfähig zu machen und eines der besten Spiele dieser Tage zu kreieren.", Category = "RPG", Price= 14.99M},
                new Game{Name ="FIFA 14", Description = "Nach Angaben von EA werden mehr als 3,4 Millionen Matches „Ultimate Team“ pro Tag gespielt und ist damit der populärste Spielmodus der FIFA-Reihe. Mit den neuen Features soll die Chemie zwischen den Spielern besser stimmen, die Spieltaktik individueller einstellbar sein und es wird für alle Matches mehr Rewards geben, mit denen man neue Spieler und Items freischalten kann. Außerdem kann man nun endlich seine eigenen, individuellen Trikos erstellen.", Category = "Simulation", Price= 6.00M},
                new Game{Name ="Need for Speed Rivals", Description = "Kein Jahr ohne neues Need for Speed. Das Rennfranchise von Electronic Arts hat einmal mehr das Entwicklerstudio gewechselt. EA Ghosts aus Göteborg arbeitet jetzt an Need for Speed Rivals – und möchte mit ein paar Änderungen überzeugen.", Category = "Simulation", Price= 15.00M},
                new Game{Name ="Crysis 3", Description = "Es musste ja kommen: Keine zwei Jahre nach dem Release von Crysis 2 legen Crytek und Electronic Arts mit Crysis 3 gleich nach. Was uns erwartet? Wohl wieder Action und Grafik vom Feinsten.", Category = "Ego Shooter", Price= 12.00M},
                new Game{Name ="Dead Space 3", Description = "Rund acht Stunden sind vergangen und ich schaue auf die Uhr. Wie lang denn noch? Seit etwa sechs Stunden will ich bereits nicht mehr, war zunächst geschockt, dann sauer und will jetzt einfach nur noch, dass es endlich aufhört. Es ist einer dieser Momente im Alltag eines Spielejournalisten, wo das Hobby, das zum Beruf wurde, zur nervenden Arbeit ausartet. Dass es hier um eine Spielreihe geht, dessen Erstling ich verehrt habe, macht es kaum besser.", Category = "Ego Shooter", Price= 49.00M}
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
