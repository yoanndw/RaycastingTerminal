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

        public static int GetTile(int[,] map, int x, int y)
        {
            if (y < 0 || y >= map.GetLength(0) || x < 0 || x >= map.GetLength(1))
            {
                return 1;
            }

            return map[y, x];
        }

        public static int GetPreviousTileDelta(float pixelPos)
        {
            if (pixelPos % Constants.TILE_SIZE == 0)
            {
                return - 1;
            }

            return 0;
        }
    }
}
