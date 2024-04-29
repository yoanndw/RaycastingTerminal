using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    public static class MapUtils
    {
        public static int TileToPixel(int v)
        {
            return v * Constants.TILE_SIZE;
        }

        public static int PixelToTile(float v)
        {
            return (int)MathF.Floor(v / Constants.TILE_SIZE);
        }
    }
}
