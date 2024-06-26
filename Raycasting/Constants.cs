﻿using Raylib_cs;

using System.Numerics;

using static Raycasting.MathUtils;

namespace Raycasting
{
    internal class Constants
    {
        public static int FPS_VIEW_W = 800;
        public static int FPS_VIEW_H = 600;
        public static int HUD_W = 800;
        public static int HUD_OFF_X = HUD_W / 2;
        public static int WIN_W = FPS_VIEW_W + HUD_W;
        public static int WIN_H = FPS_VIEW_H;

        public static int TILE_SIZE = 40;
        public static Vector2 TILE_SIZE_VEC = new Vector2(TILE_SIZE);
        public static int RESOLUTION_WIDTH = 400;
        public static int RESOLUTION_HEIGHT = 300;
        public static int FOV_DEG = 70;
        public static double FOV_RAD = Deg2Rad(FOV_DEG);
        public static int HALF_FOV_DEG = FOV_DEG / 2;
        public static double HALF_FOV_RAD = FOV_RAD / 2f;
        public static double NEAR_PLANE_DIST = TILE_SIZE / 2f;
        public static double FAR_PLANE_DIST = 6 * TILE_SIZE;

        public static double NEAR_PLANE_HALF_W = NEAR_PLANE_DIST * Math.Tan(HALF_FOV_RAD);
        public static double NEAR_PLANE_W = NEAR_PLANE_HALF_W * 2;
        public static double INITIAL_CAMERA_NEAR_PLANE_DIST = Math.Sqrt(NEAR_PLANE_HALF_W * NEAR_PLANE_HALF_W + NEAR_PLANE_DIST * NEAR_PLANE_DIST);

        public static double STEP_ANGLE_RAD = FOV_RAD / RESOLUTION_WIDTH;
        public static double STEP_ANGLE_DEG = FOV_DEG / ((double)RESOLUTION_WIDTH);
        public static float MOUSE_SENSIBILITY = 25f;

        public static double MAX_FOG_RADIUS = RESOLUTION_WIDTH / 2;
        public static double MIN_FOG_RADIUS = MAX_FOG_RADIUS * 0.1;

        #region Sides
        public static int NORTH = 1;
        public static int EAST = 2;
        public static int SOUTH = 3;
        public static int WEST = 4;

        public static Color[] COLORS =
        {
            Color.SkyBlue,
            Color.Blue,
            Color.Yellow,
            Color.Green,
            Color.Red,
        };
        #endregion
    }
}
