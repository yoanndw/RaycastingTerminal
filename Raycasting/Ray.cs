using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    internal class Ray
    {
        public Vector2 Origin { get; private set; }
        public Vector2 Dest { get; private set; }
        public float Distance { get; private set; }
        public float AbsoluteAngleDeg { get; private set; }
        public float AngleFromPlayerDeg { get; private set; }

        public Ray(Vector2 origin, Vector2 dest, float absoluteAngleDeg, float angleFromPlayerDeg) 
        {
            this.Origin = origin;
            this.Dest = dest;
            this.AbsoluteAngleDeg = absoluteAngleDeg;
            this.AngleFromPlayerDeg = angleFromPlayerDeg;
            this.Distance = Vector2.Distance(origin, dest);
        }

        public static Ray Raycast(int[,] map, Player player, float angle, float distanceIncrement)
        {
            var playerAngleRad = player.Angle * Math.PI / 180;

            var angleRad = playerAngleRad + angle * Math.PI / 180;

            Vector2 playerScrPos = player.ScreenPos;
            var initialDistance = (float)Constants.INITIAL_CAMERA_NEAR_PLANE_DIST;
            Vector2 distanceVec =  initialDistance * new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));
            Vector2 rayVec = playerScrPos + distanceVec;
            Ray ray = new Ray(playerScrPos, rayVec, player.Angle + angle, angle);
            ray.Draw(800, 0);

            while (!ray.Hit(map))
            {
                distanceVec += distanceIncrement * new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));
                ray.Dest = playerScrPos + distanceVec;
                ray.Distance += distanceIncrement;
            }

            ray.CorrectDistortion();

            return ray;
        }

        void CorrectDistortion()
        {
            this.Distance *= MathF.Cos(this.AngleFromPlayerDeg / 180 * MathF.PI);
        }

        bool Hit(int[,] map)
        {
            int tileX = MapUtils.PixelToTile(this.Dest.X);
            int tileY = MapUtils.PixelToTile(this.Dest.Y);
            return map[tileY, tileX] == 1;
        }

        public void Draw(int offX, int offY, Color color)
        {
            Vector2 offset = new Vector2(offX, offY);
            Raylib.DrawLineV(offset + this.Origin, offset + this.Dest, color);
        }

        public void Draw(int offX, int offY)
        {
            this.Draw(offX, offY, Color.Green);
        }
    }
}
