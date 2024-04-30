using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    internal class Constants
    {
        public static int TILE_SIZE = 40;
        public static int RESOLUTION_WIDTH = 100;
        public static int FOV_DEG = 70;
        public static float FOV_RAD = FOV_DEG * MathF.PI / 180;
        public static int HALF_FOV_DEG = FOV_DEG / 2;
        public static float HALF_FOV_RAD = FOV_RAD / 2f;
        public static float NEAR_PLANE_DIST = TILE_SIZE / 2f;
        public static float FAR_PLANE_DIST = 2.5f * TILE_SIZE;

        public static float NEAR_PLANE_HALF_W = NEAR_PLANE_DIST * MathF.Tan(HALF_FOV_RAD);
        public static float NEAR_PLANE_W = NEAR_PLANE_HALF_W * 2;
        public static float INITIAL_CAMERA_NEAR_PLANE_DIST = MathF.Sqrt(NEAR_PLANE_HALF_W * NEAR_PLANE_HALF_W + NEAR_PLANE_DIST * NEAR_PLANE_DIST);
    }
}
