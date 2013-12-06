using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphing
{
    public abstract class Graph
    {
        protected static Texture2D PixelTexture; //Simple Pixel Texture, holds a white dot
        private static Texture2D Background; //Background Texture for the graph

        protected Vector2 position; //position of the graph from the bottom-left corner

        private float total_scale = 2f; //total scale of the graph
        protected float Scale { get { return total_scale; } }

        private int x_increment, y_increment; //Spacing on the X and Y axis

        private int x_max, y_max;

        protected List<Vector2> Points; //The List of points to plot on the graph

        public bool DisplayNumbers { get; set; }

        /// <summary>
        /// Title of the graph
        /// </summary>
        public string Title { get; set; }

        public string X_Title { get; set; }

        public string Y_Title { get; set; }

        public Graph(int x_increment = 1, int y_increment = 1, int x_max = 10, int y_max = 10)
        {
            Points = new List<Vector2>();

            this.x_increment = x_increment;
            this.y_increment = y_increment;

            this.x_max = x_max;
            this.y_max = y_max;

            DisplayNumbers = true;

            position = new Vector2(300, 300);
        }

        public static void Set_Textures(Texture2D pixel, Texture2D background)
        {
            PixelTexture = pixel;
            Background = background;
        }

        public virtual void Plot(Vector2 point)
        {
            if (point.X > Points[Points.Count - 1].X) //Check to make sure the point we are trying to add is greater on the X than the previous point
            {
                if (point.X <= x_max && point.Y <= y_max && point.X >= 0 && point.Y >= 0)
                    Points.Add(point * total_scale);
                else throw new PointSequenceInvalidException();
            }
            else throw new PointSequenceInvalidException(); 
        }

        public void Set_Scale(float scale)
        {
            total_scale = scale;

            x_max *= (int)scale;
            y_max *= (int)scale;
            y_increment *= (int)scale;
            x_increment *= (int)scale;

            for (int i = 0; i < Points.Count; i++)
                Points[i] *= scale;
        }

        public virtual void Plot(List<Vector2> points)
        {
            int index = 0; 
            foreach (Vector2 p in points)
            {
                if (p.X <= x_max && p.Y <= y_max && p.X >= 0 && p.Y >= 0)
                {
                    if (index > 0)         //check to make sure this is not the first element in the list
                    {
                        if (p.X > points[index - 1].X)      //check to make sure current element is greater than previous element on the X axis
                            Points.Add(p * total_scale);
                        else throw new PointSequenceInvalidException();
                    }
                    else if (Points.Count > 0)       //if theres already points in the list then check the previous point
                    {
                        if (p.X > Points[Points.Count - 1].X)
                            Points.Add(p * total_scale);
                        else throw new PointSequenceInvalidException();
                    }
                    else Points.Add(p * total_scale); //if first element just add it
                }
                else throw new PointSequenceInvalidException(); 
                index++; 
            }
        }

        public virtual void Draw(SpriteBatch sbatch)
        {
            if (Background != null)
                sbatch.Draw(Background, new Rectangle((int)position.X, (int)position.Y - y_max, x_max, y_max), Color.White);

            int x_dash_count = 1;
            for (int x = 0; x < x_max; x++)
            {
                sbatch.Draw(PixelTexture, position + new Vector2(x, 0), null, Color.White, 0f, Vector2.Zero, total_scale, SpriteEffects.None, 1f);
                if (x == (x_increment * x_dash_count))
                {
                    Draw_Dash_X(sbatch, position + new Vector2(x, -3 * total_scale));
                    x_dash_count++;
                }
            }

            int y_dash_count = 1;
            for (int y = 0; y < y_max; y++)
            {
                sbatch.Draw(PixelTexture, position + new Vector2(0, -y), null, Color.White, 0f, Vector2.Zero, total_scale, SpriteEffects.None, 1f);
                if (y == (y_increment * y_dash_count))
                {
                    Draw_Dash_Y(sbatch, position + new Vector2(-3 * total_scale, -y));
                    y_dash_count++;
                }
            }
        }

        private void Draw_Dash_X(SpriteBatch sbatch, Vector2 location)
        {
            sbatch.Draw(PixelTexture, location, new Rectangle((int)location.X, (int)location.Y, 1, 6), Color.White, 0f, Vector2.Zero, total_scale, SpriteEffects.None, 1f);
        }

        private void Draw_Dash_Y(SpriteBatch sbatch, Vector2 location)
        {
            sbatch.Draw(PixelTexture, location, new Rectangle((int)location.X, (int)location.Y, 6, 1), Color.White, 0f, Vector2.Zero, total_scale, SpriteEffects.None, 1f);
        }
    }
}
