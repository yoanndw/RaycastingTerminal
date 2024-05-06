using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    using Rendering;
    using System.Diagnostics;
    using System.Numerics;

    public class Game
    {
        Player player = new Player(2, 7, -105);
        protected Image wallImage;
        int playerRadius = 7;

        int tileSize = Constants.TILE_SIZE;
        Vector2 tileSizeVec;

        int[,] map =
        {
            { 1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,1,0,0,0,0,0,1 },
            { 1,0,0,1,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1 },
        };

        BaseRenderer renderer;

        public Game()
        {
            this.renderer = new RaylibRenderer(this.map, this.wallImage, this.player, 7);
        }

        void LoadAssets()
        {
            this.wallImage = Raylib.LoadImage("Images/wall16.png");
            this.renderer.UpdateWallImage(this.wallImage);
        }

        public void Run()
        {
            this.renderer.InitFrame();
            LoadAssets();
            while (this.renderer.IsRunning())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Right))
                {
                    this.player.MoveNoCheck(1, 0);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Left))
                {
                    this.player.MoveNoCheck(-1, 0);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    this.player.MoveNoCheck(0, -1);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Down))
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

                this.renderer.Draw();
            }
        }
    }
}
