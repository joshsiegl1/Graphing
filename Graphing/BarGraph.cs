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

        }
    }
}
