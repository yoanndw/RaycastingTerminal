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
        int tileSize = Constants.TILE_SIZE;
        Vector2 tileSizeVec;

        public RaylibRenderer(int[,] map, Player player)
        {
            this.player = player;
            this.map = map;

            this.tileSizeVec = new Vector2(this.tileSize, this.tileSize);
        }

        public override void Draw()
        {
            Raylib.InitWindow(1200, 600, "Raycasting");
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Right))
                {
                    this.player.MoveNoCheck(1, 0);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Left))
                {
                    this.player.MoveNoCheck(-1, 0);
                } else if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    this.player.MoveNoCheck(0, -1);
                } else if (Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    this.player.MoveNoCheck(0, 1);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.J))
                {
                    this.player.Rotate(-15);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.L))
                {
                    this.player.Rotate(15);
                }


                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawRectangle(800, 0, 400, 600, Color.White);

                // Sky and ground
                Raylib.DrawRectangle(0, 0, 800, 300, Color.SkyBlue);
                Raylib.DrawRectangle(0, 300, 800, 300, Color.Brown);

                DrawPlayer(800, 0);
                DrawMap(800, 0);

                for (int i = 0; i < Constants.RESOLUTION_WIDTH; i++)
                {
                    var a = (i - Constants.RESOLUTION_WIDTH / 2) * Constants.STEP_ANGLE_DEG;

                    Ray r = Ray.Raycast(this.map, this.player, (float)a, 1);
                    var c = r.Distance <= Constants.FAR_PLANE_DIST ? Color.Green : Color.Red;
                    r.Draw(800, 0, c);
                    Render3D(r, i);
                }


                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        void Render3D(Ray ray, int projScrX)
        {
            if (ray.Distance <= Constants.FAR_PLANE_DIST)
            {
                int pixelSize = 800 / Constants.RESOLUTION_WIDTH;
                int x = projScrX * pixelSize;

                // Compute height
                double height = Constants.RESOLUTION_HEIGHT * Constants.NEAR_PLANE_DIST / ray.Distance;
                int renderedHeight = (int)Math.Round(height) * pixelSize;
                int y = 300 - renderedHeight/ 2;
                Raylib.DrawRectangle(x, y, pixelSize, renderedHeight, Color.Green);
            }
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
    }
}
