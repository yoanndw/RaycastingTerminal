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
        Player player = new Player(1, 1, 7, 90, -90);
        protected Image wallImage;

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
            this.renderer = new RaylibRenderer(this.map, this.wallImage, this.player);
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
                float dt = Raylib.GetFrameTime();
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    this.player.Move(this.map, 0, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.A))
                {
                    this.player.Move(this.map, -90, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    this.player.Move(this.map, 180, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.D))
                {
                    this.player.Move(this.map, 90, dt);
                }

                //////////////////////
                if (Raylib.IsKeyDown(KeyboardKey.Left))
                {
                    this.player.MoveXY(this.map, -1, 0, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.Right))
                {
                    this.player.MoveXY(this.map, 1, 0, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    this.player.MoveXY(this.map, 0, -1, dt);
                }
                if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    this.player.MoveXY(this.map, 0, 1, dt);
                }

                if (Raylib.IsKeyPressed(KeyboardKey.J))
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
