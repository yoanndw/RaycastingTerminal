using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    internal class Constants
    {
        public static int TILE_SIZE = 20;
        public static int PROJ_PLANE_WIDTH = 2 * TILE_SIZE;
        public static int RESOLUTION_WIDTH = 100;
        public static int FOV_DEG = 70;
        public static int HALF_FOV_DEG = FOV_DEG / 2;
        public static float FOV_RAD = FOV_DEG * MathF.PI / 180;
        public static float HALF_FOV_RAD = HALF_FOV_DEG * MathF.PI / 180;
    }
}
