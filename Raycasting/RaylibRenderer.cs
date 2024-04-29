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

                //Ray ray = Ray.Raycast(this.map, this.player, -Constants.HALF_FOV, 5, 1);
                //var ray2 = Ray.Raycast(this.map, this.player, Constants.HALF_FOV, 5, 1);
                var ray3 = Ray.Raycast(this.map, this.player, 20, 5, 1);

                //Debug.WriteLine("Ray: " + ray.Project());
                //Debug.WriteLine("Ray 2: " + ray2.Project());
                Debug.WriteLine("Ray 3: " + ray3.Project());

                DrawPlayer(800, 0);
                DrawMap(800, 0);
                //ray.Draw(800, 0);
                //ray2.Draw(800, 0);
                ray3.Draw(800, 0);

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
                        Raylib.DrawRectangleRoundedLines(new Rectangle(off + new Vector2(j, i) * this.tileSize, this.tileSizeVec), 0, 0, 1, Color.Black);
                        //Raylib.DrawRectangle(offX + j * this.tileSize, offY + i * this.tileSize, this.tileSize, this.tileSize, Color.Blue);
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
            //Debug.WriteLine("Origin: " + offset);
        }
    }
}
