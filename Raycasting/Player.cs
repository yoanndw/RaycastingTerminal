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
            this.x = MapUtils.TileToPixel(x);
            this.y = MapUtils.TileToPixel(y);
            this.Angle = angle;
        }

        public Vector2 ScreenPos
        {
            get => new Vector2(x, y);
        }

        public void MoveNoCheck(int dx, int dy)
        {
            this.x += dx * Constants.TILE_SIZE;
            this.y += dy * Constants.TILE_SIZE;
        }

        public void Rotate(float angleDeg)
        {
            this.Angle += angleDeg;
            if (this.Angle > 360)
            {
                this.Angle -= 360;
            }
            else if (this.Angle < -360)
            {
                this.Angle += 360;
            }
        }
    }
}
