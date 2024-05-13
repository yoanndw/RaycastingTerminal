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
    enum CollisionCorner
    {
        TL = 0,
        TR = 1,
        BL = 2,
        BR = 3,
    }

    public class Player
    {
        public Vector2 Pos { get; private set; }
        float speed;
        public float Angle { get; private set; }
        public float Radius { get; private set; }

        float LeftX(Vector2 pos) => pos.X - Radius;
        float RightX(Vector2 pos) => pos.X + Radius;
        float TopY(Vector2 pos) => pos.Y - Radius;
        float BotY(Vector2 pos) => pos.Y + Radius;

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
            //Debug.WriteLine($"velocity: {vx}, {vy}");

            int topTileY = MapUtils.PixelToTile(TopY(newPos));
            int leftTileX = MapUtils.PixelToTile(LeftX(newPos));
            int botTileY = MapUtils.PixelToTile(BotY(newPos));
            int rightTileX = MapUtils.PixelToTile(RightX(newPos));

            bool[] coll = CheckCollisions(map, newPos);
            if (coll.All(c => c))
            {
                newPos = Pos;
            }

            // Two or more corner collide
            if (coll[(int)CollisionCorner.TL] && coll[(int)CollisionCorner.BL])
            {
                float wallX = MapUtils.TileToPixel(leftTileX + 1);
                float overlapX = newPos.X - this.Radius - wallX;
                newPos.X -= overlapX;
            }
            else if (coll[(int)CollisionCorner.TR] && coll[(int)CollisionCorner.BR])
            {
                float wallX = MapUtils.TileToPixel(rightTileX);
                float overlapX = newPos.X + this.Radius - wallX;
                newPos.X -= overlapX;
            }
            
            if (coll[(int)CollisionCorner.TL] && coll[(int)CollisionCorner.TR])
            {
                float wallY = MapUtils.TileToPixel(topTileY + 1);
                float overlapY = newPos.Y - this.Radius - wallY;
                newPos.Y -= overlapY;
            }
            else if (coll[(int)CollisionCorner.BL] && coll[(int)CollisionCorner.BR])
            {
                float wallY = MapUtils.TileToPixel(botTileY);
                float overlapY = newPos.Y + this.Radius - wallY;
                newPos.Y -= overlapY;
            }

            // Only one corner collides
            if (coll.Where(c => c).Count() <= 1)
            {
                if (coll[(int)CollisionCorner.TL])
                {
                    float wallX = MapUtils.TileToPixel(leftTileX + 1);
                    float wallY = MapUtils.TileToPixel(topTileY + 1);
                    float overlapX = newPos.X - this.Radius - wallX;
                    float overlapY = newPos.Y - this.Radius - wallY;
                    if (Math.Abs(overlapX) > Math.Abs(overlapY))
                    {
                        newPos.Y -= overlapY;
                    }
                    else if (Math.Abs(overlapX) < Math.Abs(overlapY))
                    {
                        newPos.X -= overlapX;
                    }
                    else
                    {
                        newPos.X -= overlapX;
                        newPos.Y -= overlapY;
                    }
                }
                else if (coll[(int)CollisionCorner.TR])
                {
                    float wallX = MapUtils.TileToPixel(rightTileX);
                    float wallY = MapUtils.TileToPixel(topTileY + 1);
                    float overlapX = newPos.X + this.Radius - wallX;
                    float overlapY = newPos.Y - this.Radius - wallY;
                    if (Math.Abs(overlapX) > Math.Abs(overlapY))
                    {
                        newPos.Y -= overlapY;
                    }
                    else if (Math.Abs(overlapX) < Math.Abs(overlapY))
                    {
                        newPos.X -= overlapX;
                    }
                    else
                    {
                        newPos.X -= overlapX;
                        newPos.Y -= overlapY;
                    }
                }
                else if (coll[(int)CollisionCorner.BL])
                {
                    float wallX = MapUtils.TileToPixel(leftTileX + 1);
                    float wallY = MapUtils.TileToPixel(botTileY);
                    float overlapX = newPos.X - this.Radius - wallX;
                    float overlapY = newPos.Y + this.Radius - wallY;
                    if (Math.Abs(overlapX) > Math.Abs(overlapY))
                    {
                        newPos.Y -= overlapY;
                    }
                    else if (Math.Abs(overlapX) < Math.Abs(overlapY))
                    {
                        newPos.X -= overlapX;
                    }
                    else
                    {
                        newPos.X -= overlapX;
                        newPos.Y -= overlapY;
                    }
                }
                else if (coll[(int)CollisionCorner.BR])
                {
                    float wallX = MapUtils.TileToPixel(rightTileX);
                    float wallY = MapUtils.TileToPixel(botTileY);
                    float overlapX = newPos.X + this.Radius - wallX;
                    float overlapY = newPos.Y + this.Radius - wallY;
                    if (Math.Abs(overlapX) > Math.Abs(overlapY))
                    {
                        newPos.Y -= overlapY;
                    }
                    else if (Math.Abs(overlapX) < Math.Abs(overlapY))
                    {
                        newPos.X -= overlapX;
                    }
                    else
                    {
                        newPos.X -= overlapX;
                        newPos.Y -= overlapY;
                    }
                }
            }

            this.Pos = newPos;
        }

        bool CollidesWall(int[,] map, Vector2 pos)
        {
            int topTileY = MapUtils.PixelToTile(TopY(pos));
            int leftTileX = MapUtils.PixelToTile(LeftX(pos));
            int botTileY = MapUtils.PixelToTile(BotY(pos));
            int rightTileX = MapUtils.PixelToTile(RightX(pos));

            return MapUtils.GetTile(map, leftTileX, topTileY) != 0
                || MapUtils.GetTile(map, rightTileX, topTileY) != 0
                || MapUtils.GetTile(map, leftTileX, botTileY) != 0
                || MapUtils.GetTile(map, rightTileX, botTileY) != 0;
        }

        bool[] CheckCollisions(int[,] map, Vector2 pos)
        {
            int topTileY = MapUtils.PixelToTile(TopY(pos));
            int leftTileX = MapUtils.PixelToTile(LeftX(pos));
            int botTileY = MapUtils.PixelToTile(BotY(pos));
            int rightTileX = MapUtils.PixelToTile(RightX(pos));

            bool[] res = { false, false, false, false };
            if (MapUtils.GetTile(map, leftTileX, topTileY) != 0)
            {
                res[(int)CollisionCorner.TL] = true;
            }
            if (MapUtils.GetTile(map, rightTileX, topTileY) != 0)
            {
                res[(int)CollisionCorner.TR] = true;
            }
            if (MapUtils.GetTile(map, leftTileX, botTileY) != 0)
            {
                res[(int)CollisionCorner.BL] = true;
            }
            if (MapUtils.GetTile(map, rightTileX, botTileY) != 0)
            {
                res[(int)CollisionCorner.BR] = true;
            }

            return res;
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
