using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raylib_cs;

namespace Raycasting.Rendering
{
    public class RaylibRenderer : BaseRenderer
    {
        int playerX, playerY;

        public RaylibRenderer(int playerX, int playerY)
        {
            this.playerX = playerX;
            this.playerY = playerY;
        }

        public override void Draw()
        {
            Raylib.InitWindow(800, 600, "Raycasting");
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                DrawPlayer();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        void DrawPlayer()
        {
            Raylib.DrawCircle(playerX, playerY, 10, Color.Green);
        }
    }
}
