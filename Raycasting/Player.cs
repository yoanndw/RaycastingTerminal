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
        public float Angle { get; private set; }

        public Player(int x, int y, float angle)
        {
            this.x = x;
            this.y = y;
            this.Angle = angle;
        }

        public Vector2 ScreenPos
        {
            get => new Vector2(MapUtils.TileToPixel(x), MapUtils.TileToPixel(y)) + new Vector2(Constants.TILE_SIZE) / 2;
        }
    }
}
