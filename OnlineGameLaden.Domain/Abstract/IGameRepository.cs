using RubiksCubeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeStore.Domain.Abstract
{
    public interface ICubeRepository
    {
        IEnumerable<Cube> Cubes { get; }
        void SaveProdukt(Cube cube);
        Cube DeleteProdukt(int cubeId);
    }
}
