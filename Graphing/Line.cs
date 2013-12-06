using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphing
{
    internal class Line
    {
        private List<Vector2> Points;  //Just a simple list of pixels that make up a line
        public Line(Vector2 start, Vector2 finish)
        {
            Points = new List<Vector2>();
            Trace(start, finish);
        }

        private void Trace(Vector2 start, Vector2 finish)
        {
            Points.Add(start);   //Add the start point to our list of points
            while (!(Points[Points.Count - 1].X == finish.X) && !(Points[Points.Count - 1].Y == finish.Y)) //While we haven't reached the finish point, keep adding points
            {
                Vector2 myPoint = new Vector2(Points[Points.Count - 1].X, Points[Points.Count - 1].Y); //Create a new point, referencing the last one in the list

                float Xstep = finish.X - start.X;          //Create the X_Step (direction)
                float Ystep = finish.Y - start.Y;          //Create the Y_Step (direction)

                float Y_increase = Ystep / Xstep;          //Get the Slope of the line (Rise / Run)
                myPoint.Y += Y_increase;                   //increase the our current point's Y value

                if (Y_increase > 0)
                {
                    for (float z = myPoint.Y; z > myPoint.Y - Y_increase; z--)
                        Points.Add(new Vector2(myPoint.X, z));                      //Add extra pixels to give the line some needed width
                }
                else
                {
                    for (float z = myPoint.Y; z < myPoint.Y - Y_increase; z++)      //Do the opposite if the line is going the opposite direction
                        Points.Add(new Vector2(myPoint.X, z));
                }

                myPoint.X++;                              //Step one on the X

                Points.Add(myPoint);                         //Add the point, repeat 
            }
        }

        public void Draw(SpriteBatch sbatch, Texture2D pixel, Vector2 Position)
        {
            foreach (Vector2 p in Points)
                sbatch.Draw(pixel, Position + new Vector2(p.X, -p.Y), Color.White);
        }
    }
}