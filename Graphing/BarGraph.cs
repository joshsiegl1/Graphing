using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphing
{
    public class BarGraph : Graph
    {
        public BarGraph(int x_increment, int y_increment, int x_max, int y_max)
            : base(x_increment, y_increment, x_max, y_max)
        {
        }

        public override void Draw(SpriteBatch sbatch)
        {
            base.Draw(sbatch);
            foreach (Vector2 v in Points)
                Draw_Bar(sbatch, v);
        }

        private void Draw_Bar(SpriteBatch sbatch, Vector2 location)
        {
            Rectangle bar = new Rectangle((int)position.X + (int)location.X - (X_Increment / 2), (int)position.Y - (int)location.Y, (int)X_Increment, (int)location.Y);
            sbatch.Draw(PixelTexture, bar, Color.White);
            OutLine_Bar(sbatch, bar);
        }

        private void OutLine_Bar(SpriteBatch sbatch, Rectangle rect)
        {
            List<Vector2> pixels = new List<Vector2>();
            for (int x = rect.X; x < rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    if (y == rect.Y || y == rect.Y + rect.Height - 1)
                        pixels.Add(new Vector2(x, y));
                    if (x == rect.X || x == rect.X + rect.Width - 1)
                        pixels.Add(new Vector2(x, y));
                }
            }

            foreach (Vector2 v in pixels)
                sbatch.Draw(PixelTexture, v, Color.LimeGreen);
        }
    }
}
