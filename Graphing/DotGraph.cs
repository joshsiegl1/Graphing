using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Graphing
{
    public class DotGraph : Graph
    {
        private List<Line> Lines;

        public DotGraph(int x_increment, int y_increment, int x_max, int y_max)
            : base(x_increment, y_increment, x_max, y_max)
        {
            Lines = new List<Line>();
        }

        public override void Plot(Vector2 point)
        {
            base.Plot(point);
            if (Points.Count >= 2)
                Lines.Add(new Line(Points[Points.Count - 2], Points[Points.Count - 1]));
        }

        public override void Plot(List<Vector2> points)
        {
            base.Plot(points);
            if (points.Count >= 2)
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Lines.Add(new Line(points[i], points[i + 1])); 
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sbatch)
        {
            base.Draw(sbatch);

            foreach (Line line in Lines)
                line.Draw(sbatch, PixelTexture, position);

            foreach (Vector2 p in Points)
                sbatch.Draw(PixelTexture, position + new Vector2(p.X, -p.Y) - new Vector2(2.5f, 2.5f), null, Color.Red, 0f, Vector2.Zero, 5f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1f);
        }
    }
}
