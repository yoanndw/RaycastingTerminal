using System;
using System.Collections.Generic;
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

                DrawPlayer(800, 0);
                DrawMap(800, 0);

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
            int playerScrX = offX + (int)playerScrPos.X + this.tileSize / 2;
            int playerScrY = offY + (int)playerScrPos.Y + this.tileSize / 2;
            Raylib.DrawCircle(playerScrX, playerScrY, this.playerRadius, Color.Green);
        }
    }
}
