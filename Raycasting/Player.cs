using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    public class Player
    {
        int x, y;

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ScreenPos
        {
            get => new Vector2(MapUtils.TileToPixel(x), MapUtils.TileToPixel(y));
        }
    }
}
