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
        int[,] map;
        Player player;

        public RaylibRenderer(int[,] map, Image wallImage, Player player) : base(wallImage)
        {
            this.map = map;
            this.player = player;
        }


        public override void InitFrame()
        {
            Raylib.InitWindow(1200, 600, "Raycasting");
        }

        public override bool IsRunning()
        {
            return !Raylib.WindowShouldClose();
        }

        public override void CloseFrame()
        {
            Raylib.CloseWindow();
        }

        public override void Draw()
        {
            
        }
    }
}
