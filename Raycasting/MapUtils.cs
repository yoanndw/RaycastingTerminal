using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    public static class MapUtils
    {
        public static int TILE_SIZE = 20;
        public static int TileToPixel(int v)
        {
            return v * TILE_SIZE;
        }
    }
}
