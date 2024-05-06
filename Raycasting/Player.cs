using Raylib_cs;

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

        public void MoveNoCheck(int dx, int dy)
        {
            this.Pos += new Vector2(dx, dy) * Constants.TILE_SIZE;
        }

        public void Move(int[,] map, float dx, float dy, float dt)
        {
            Vector2 newPos = this.Pos + new Vector2(dx, dy) * dt * this.speed;
            if (CollidesWall(map, newPos))
            {
                int leftTileX = MapUtils.PixelToTile(newPos.X - this.Radius);
                int rightTileX = MapUtils.PixelToTile(newPos.X + this.Radius);

                float deltaX = 0;
                if (dx < 0)
                {
                    float wallX = MapUtils.TileToPixel(leftTileX + 1);
                    deltaX = wallX - newPos.X + this.Radius;
                }
                else if (dx > 0)
                {
                    float wallX = MapUtils.TileToPixel(rightTileX);
                    deltaX = wallX - newPos.X - this.Radius;
                }

                newPos.X += deltaX;
            }

            if (CollidesWall(map, newPos))
            {
                int topTileY = MapUtils.PixelToTile(newPos.Y - this.Radius);
                int botTileY = MapUtils.PixelToTile(newPos.Y + this.Radius);

                float deltaY = 0;
                if (dy < 0)
                {
                    float wallY = MapUtils.TileToPixel(topTileY + 1);
                    deltaY = wallY - newPos.Y + this.Radius;
                }
                else if (dy > 0)
                {
                    float wallY = MapUtils.TileToPixel(botTileY);
                    deltaY = wallY - newPos.Y - this.Radius;
                }

                newPos.Y += deltaY;
            }

            this.Pos = newPos;
        }

        bool CollidesWall(int[,] map, Vector2 pos)
        {
            int topTileY = MapUtils.PixelToTile(pos.Y - this.Radius);
            int leftTileX = MapUtils.PixelToTile(pos.X - this.Radius);
            int botTileY = MapUtils.PixelToTile(pos.Y + this.Radius - 1);
            int rightTileX = MapUtils.PixelToTile(pos.X + this.Radius - 1);

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
