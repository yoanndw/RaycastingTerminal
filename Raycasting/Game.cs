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
        Image wallImage;
        Image targetImage;

        int tileSize = Constants.TILE_SIZE;
        Vector2 tileSizeVec;

        int[,] map =
        {
            { 1,2,1,1,1,1,1,1,1,1 },
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

        List<Image> images = new List<Image>();

        BaseRenderer renderer;

        public Game()
        {
            this.renderer = new RaylibRenderer(this.map, this.wallImage, this.player);
        }

        void LoadAssets()
        {
            this.wallImage = Raylib.LoadImage("Images/wall16.png");
            this.targetImage = Raylib.LoadImage("Images/target16.png");
            this.images.Add(this.wallImage);
            this.images.Add(this.targetImage);
        }

        public void Run()
        {
            Raylib.InitWindow(1200, 600, "Raycasting");
            LoadAssets();
            while (!Raylib.WindowShouldClose())
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

                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Shoot();
                }

                Draw();
            }

            Raylib.CloseWindow();
        }

        void Shoot()
        {
            Ray r = Ray.OptimisedRaycast(this.map, this.player, 0);
            Debug.WriteLine($"{r.Tile}");
            if (r.Tile == 2)
            {
                int tileX = (int)r.TilePos.X;
                int tileY = (int)r.TilePos.Y;
                this.map[tileY, tileX] = 1;
            }
        }

        void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectangle(800, 0, 400, 600, Color.White);

            // Sky and ground
            Raylib.DrawRectangle(0, 0, 800, 300, Color.SkyBlue);
            Raylib.DrawRectangle(0, 300, 800, 300, Color.Brown);

            this.player.Draw(map, 800, 0);
            DrawMap(800, 0);

            for (int i = 0; i < Constants.RESOLUTION_WIDTH; i++)
            {
                var a = (i - Constants.RESOLUTION_WIDTH / 2) * Constants.STEP_ANGLE_DEG;
                //Debug.WriteLine($"--------{a}°----------");

                Ray r = Ray.OptimisedRaycast(this.map, this.player, (float)a);
                var c = r.CorrectedDistance <= Constants.FAR_PLANE_DIST ? Color.Green : Color.Red;
                r.Draw(800, 0, c);

                Render3D(r, i);
            }

            Raylib.EndDrawing();
        }

        void Render3D(Ray ray, int nearPlaneX)
        {
            if (ray.CorrectedDistance <= Constants.FAR_PLANE_DIST)
            {
                int pixelWidth = 800 / Constants.RESOLUTION_WIDTH;
                int pixelHeight = 600 / Constants.RESOLUTION_HEIGHT;
                int posScrX = nearPlaneX * pixelWidth;

                // Color
                Image image = this.images[ray.Tile - 1];
                int posTexX = ray.HitTextureX(image);

                double height = 1.5 * Constants.RESOLUTION_HEIGHT * Constants.NEAR_PLANE_DIST / ray.CorrectedDistance;
                for (int y = 0; y < height; y++)
                {
                    int posTexY = ray.HitTextureY(image, height, y);

                    int posScrY = (int)Math.Round((y - height / 2) * pixelHeight, 3) + 300;
                    Color color = Raylib.GetImageColor(image, posTexX, posTexY);
                    Raylib.DrawRectangle(posScrX, posScrY, pixelWidth, pixelHeight, color);
                }

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
                        Raylib.DrawRectangleRoundedLines(new Rectangle(off + new Vector2(j, i) * Constants.TILE_SIZE, Constants.TILE_SIZE_VEC), 0, 0, 1, Color.Black);
                    }
                }
            }
        }

    }
}
