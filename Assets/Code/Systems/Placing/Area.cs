using System.Collections.Generic;
using Province.Vector;

namespace Code.Systems.Placing
{
    public class Area
    {
        public static readonly Area Current = new Area();
        
        
        
        public List<PositionComponent>[,] Grid { get; set; }

        public List<PositionComponent> this[Vector position]
        {
            get { return Grid[position.X, position.Y]; }
            set { Grid[position.X, position.Y] = value; }
        }



        
        public void Initialize(Vector size)
        {
            Grid = new List<PositionComponent>[size.X, size.Y];
            for (var x = 0; x < size.X; x++)
            {
                for (var y = 0; y < size.Y; y++)
                {
                    Grid[x, y] = new List<PositionComponent>();
                }
            }
        }
    }
}

