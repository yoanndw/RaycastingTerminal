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
        float angle;

        public Player(int x, int y, float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public Vector2 ScreenPos
        {
            get => new Vector2(MapUtils.TileToPixel(x), MapUtils.TileToPixel(y)) + new Vector2(MapUtils.TILE_SIZE) / 2;
        }
    }
}
