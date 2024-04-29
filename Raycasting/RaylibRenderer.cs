using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace Raycasting.Rendering
{
    public class RaylibRenderer : BaseRenderer
    {
        Player player;
        int playerRadius = 7;

        int[,] map;
        int tileSize = 20;
        Vector2 tileSizeVec;

        public RaylibRenderer(int[,] map, Player player)
        {
            this.player = player;
            this.map = map;

            this.tileSizeVec = new Vector2(this.tileSize, this.tileSize);
        }

        public override void Draw()
        {
            Raylib.InitWindow(1000, 600, "Raycasting");
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawRectangle(800, 0, 200, 600, Color.White);

                Vector2 ray = ShootOneRay(45, 5);

                DrawPlayer(800, 0);
                DrawMap(800, 0);
                DrawRay(ray, 800, 0);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        void DrawMap(int offX, int offY)
        {
            Vector2 off = new Vector2(offX, offY);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int tile = map[i, j];
                    if (tile == 1)
                    {
                        Raylib.DrawRectangleRoundedLines(new Rectangle(off + new Vector2(j, i) * this.tileSize, this.tileSizeVec), 0, 0, 2, Color.Black);
                    }
                }
            }
        }

        void DrawPlayer(int offX, int offY)
        {
            Vector2 playerScrPos = this.player.ScreenPos;
            int playerScrX = offX + (int)playerScrPos.X;
            int playerScrY = offY + (int)playerScrPos.Y;
            Raylib.DrawCircle(playerScrX, playerScrY, this.playerRadius, Color.Green);
        }

        void DrawRay(Vector2 ray, int offX, int offY)
        {
            Vector2 offset = new Vector2(offX, offY);
            Raylib.DrawLineV(offset + this.player.ScreenPos, offset + ray, Color.Red);
            Debug.WriteLine("Origin: " + offset);
        }

        Vector2 ShootOneRay(float angle, float distance)
        {
            float angleRad = angle * MathF.PI / 180;

            Vector2 playerScrPos = this.player.ScreenPos;
            Vector2 distanceVec = distance * new Vector2(MathF.Cos(angleRad), MathF.Sin(angleRad));
            Vector2 ray = playerScrPos + distanceVec;
            Debug.WriteLine(ray);

            return ray;
        }
    }
}
