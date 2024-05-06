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
    public class Player
    {
        public Vector2 Pos { get; private set; }
        float speed;
        public float Angle { get; private set; }
        public float Radius { get; private set; }

        public Player(int tx, int ty, float radius, float speed, float angle)
        {
            SetTilePos(tx, ty);
            this.Radius = radius;
            this.Angle = angle;
            this.speed = speed;
        }

        public void SetTilePos(int tx, int ty)
        {
            this.Pos = new Vector2(
                MapUtils.TileToPixel(tx) + Constants.TILE_SIZE / 2,
                MapUtils.TileToPixel(ty) + Constants.TILE_SIZE / 2
            );
        }

        public void Move(int[,] map, int angleDeg, float dt)
        {
            float angleRad = MathUtils.Deg2Rad(angleDeg + this.Angle);
            float vx = MathF.Round(MathF.Cos(angleRad), 4);
            float vy = MathF.Round(MathF.Sin(angleRad), 4);
            MoveXY(map, vx, vy, dt);
        }

        public void MoveXY(int[,] map, float vx, float vy, float dt)
        {
            Vector2 newPos = this.Pos + new Vector2(vx, vy) * dt * this.speed;
            Debug.WriteLine($"velocity: {vx}, {vy}");

            int tileX = MapUtils.PixelToTile(newPos.X);
            int tileY = MapUtils.PixelToTile(newPos.Y);

            int topTileY = MapUtils.PixelToTile(newPos.Y - this.Radius);
            int leftTileX = MapUtils.PixelToTile(newPos.X - this.Radius);
            int botTileY = MapUtils.PixelToTile(newPos.Y + this.Radius);
            int rightTileX = MapUtils.PixelToTile(newPos.X + this.Radius);
            if (CollidesWall(map, newPos))
            {
                if (vx > 0)
                {
                    float wallX = MapUtils.TileToPixel(rightTileX);
                    float overlapX = newPos.X + this.Radius - wallX + 1;
                    newPos.X -= overlapX;
                }
                else if (vx < 0)
                {
                    float wallX = MapUtils.TileToPixel(tileX);
                    float overlapX = wallX - (newPos.X - this.Radius);
                    newPos.X += overlapX;
                }
            }

            if (CollidesWall(map, newPos))
            {
                if (vy > 0)
                {
                    float wallY = MapUtils.TileToPixel(botTileY);
                    float overlapY = newPos.Y + this.Radius - wallY + 1;
                    newPos.Y -= overlapY;
                }
                else if (vy < 0)
                {
                    float wallY = MapUtils.TileToPixel(tileY);
                    float overlapY = wallY - (newPos.Y - this.Radius);
                    newPos.Y += overlapY;
                }
            }

            this.Pos = newPos;
        }

        bool CollidesWall(int[,] map, Vector2 pos)
        {
            int topTileY = MapUtils.PixelToTile(pos.Y - this.Radius);
            int leftTileX = MapUtils.PixelToTile(pos.X - this.Radius);
            int botTileY = MapUtils.PixelToTile(pos.Y + this.Radius);
            int rightTileX = MapUtils.PixelToTile(pos.X + this.Radius);

            return MapUtils.GetTile(map, leftTileX, topTileY) != 0
                || MapUtils.GetTile(map, rightTileX, topTileY) != 0
                || MapUtils.GetTile(map, leftTileX, botTileY) != 0
                || MapUtils.GetTile(map, rightTileX, botTileY) != 0;
        }

        bool CollidesWall(int[,] map)
        {
            return CollidesWall(map, this.Pos);
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

        public void Draw(int[,] map, int offX, int offY)
        {
            int playerScrX = offX + (int)this.Pos.X;
            int playerScrY = offY + (int)this.Pos.Y;

            Color c = this.CollidesWall(map) ? Color.Brown : Color.Green;
            Raylib.DrawCircle(playerScrX, playerScrY, this.Radius, c);
        }
    }
}
