using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Cube cube, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Cube.CubeId == cube.CubeId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Cube = cube,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Cube cube)
        {
            lineCollection.RemoveAll(l => l.Cube.CubeId == cube.CubeId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Cube.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Cube Cube { get; set; }
        public int Quantity { get; set; }
    }
}
