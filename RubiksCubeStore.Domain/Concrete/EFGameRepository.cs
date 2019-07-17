using RubiksCubeStore.Domain.Abstract;
using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeStore.Domain.Concrete
{
    public class EFCubeRepository : ICubeRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Cube> Cubes => context.Cubes;

        public void SaveProdukt(Cube cube)
        {
            if (cube.CubeId == 0)
                context.Cubes.Add(cube);
            else
            {
                Cube dbEntry = context.Cubes.Find(cube.CubeId);
                if(dbEntry != null)
                {
                    dbEntry.Name = cube.Name;
                    dbEntry.ImageData = cube.ImageData;
                    dbEntry.ImageMimeType = cube.ImageMimeType;
                    dbEntry.Description = cube.Description;
                    dbEntry.Price = cube.Price;
                    dbEntry.Category = cube.Category;
                }
            }
            context.SaveChanges();
        }

        public Cube DeleteProdukt(int cubeId)
        {
            Cube dbEntry = context.Cubes.Find(cubeId);
            if(dbEntry != null)
            {
                context.Cubes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
