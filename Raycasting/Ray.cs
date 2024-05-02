﻿using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using static Raycasting.MathUtils;

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
            var playerAngleRad = Deg2Rad(player.Angle);

            var angleRad = playerAngleRad + Deg2Rad(angle);

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

        public static Ray OptimisedRaycast(int[,] map, Player player, float angle)
        {
            var playerAngleRad = Deg2Rad(player.Angle);

            var angleRad = playerAngleRad + Deg2Rad(angle);

            Vector2 playerScrPos = player.ScreenPos;
            var initialDistance = (float)Constants.INITIAL_CAMERA_NEAR_PLANE_DIST;
            Vector2 distanceVec = initialDistance * new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));
            Vector2 rayVec = playerScrPos + distanceVec;
            Ray ray = new Ray(playerScrPos, rayVec, player.Angle + angle, angle);

            Vector2 newDest = ray.Origin;
            while (!ray.Hit(map))
            {
                newDest = ray.ComputeExtension(newDest);
                Debug.WriteLine($"New dest: {newDest}");
                ray.UpdateDest(newDest);
            }

            ray.CorrectDistortion();

            return ray;
        }

        Vector2 ComputeExtension(Vector2 origin)
        {
            float angleRad = Deg2Rad(this.AbsoluteAngleDeg);
            float referenceAngle = 0;
            int deltaTileX = 0, deltaTileY = 0;
            float cos = MathF.Cos(angleRad);
            float sin = MathF.Sin(angleRad);
            bool testAxisX = true, testAxisY = true;
            if (cos < 0)
            {
                deltaTileX = MapUtils.GetPreviousTileDelta(origin.X);
                if (sin < 0)
                {
                    deltaTileY = MapUtils.GetPreviousTileDelta(origin.Y);
                    referenceAngle = MathF.PI + angleRad;
                }
                else if (sin > 0)
                {
                    deltaTileY = 1;
                    referenceAngle = angleRad - MathF.PI;
                }
                else
                {
                    testAxisY = false;
                }
            }
            else if (cos > 0)
            {
                referenceAngle = angleRad;
                deltaTileX = 1;
                if (sin < 0)
                {
                    deltaTileY = MapUtils.GetPreviousTileDelta(origin.Y);
                }
                else if (sin > 0)
                {
                    deltaTileY = 1;
                }
                else
                {
                    testAxisY = false;
                }
            }
            else
            {
                testAxisX = false;
            }

            int originTileX = MapUtils.PixelToTile(origin.X);
            int originTileY = MapUtils.PixelToTile(origin.Y);

            Vector2 delta1 = Vector2.Zero, delta2 = Vector2.Zero;
            if (testAxisX)
            {
                float deltaGridX = MapUtils.TileToPixel(originTileX + deltaTileX) - origin.X;
                float deltaGridY = MathF.Tan(referenceAngle) * deltaGridX;
                delta1 = new Vector2(deltaGridX, deltaGridY);
            }
            
            if (testAxisY)
            {
                float deltaGridY = MapUtils.TileToPixel(originTileY + deltaTileY) - origin.Y;
                float deltaGridX = deltaGridY / MathF.Tan(referenceAngle);
                delta2 = new Vector2(deltaGridX, deltaGridY);
            }

            if (!testAxisX)
            {
                return origin + delta2;
            }

            if (!testAxisY)
            {
                return origin + delta1;
            }

            return origin + (delta1.Length() < delta2.Length() ? delta1 : delta2);
        }

        void UpdateDest(Vector2 dest)
        {
            this.Dest = dest;
            this.Distance = (this.Dest - this.Origin).Length();
        }

        void CorrectDistortion()
        {
            this.Distance *= MathF.Cos(Deg2Rad(this.AngleFromPlayerDeg));
        }

        bool Hit(int[,] map)
        {
            int tileX = MapUtils.PixelToTile(this.Dest.X);
            int tileY = MapUtils.PixelToTile(this.Dest.Y);
            if (this.Dest.X % Constants.TILE_SIZE == 0 && this.Dest.Y % Constants.TILE_SIZE == 0)
            {
                return MapUtils.GetTile(map, tileX - 1, tileY) == 1
                    || MapUtils.GetTile(map, tileX, tileY) == 1
                    || MapUtils.GetTile(map, tileX, tileY - 1) == 1
                    || MapUtils.GetTile(map, tileX - 1, tileY - 1) == 1;
            }

            if (this.Dest.X % Constants.TILE_SIZE == 0)
            {
                return MapUtils.GetTile(map, tileX - 1, tileY) == 1
                    || MapUtils.GetTile(map, tileX, tileY) == 1;
            }

            if (this.Dest.Y % Constants.TILE_SIZE == 0)
            {
                return MapUtils.GetTile(map, tileX, tileY - 1) == 1
                    || MapUtils.GetTile(map, tileX, tileY) == 1;
            }

            return MapUtils.GetTile(map, tileX, tileY) == 1;
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
