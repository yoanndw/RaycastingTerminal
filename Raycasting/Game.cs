using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    using Rendering;

    public class Game
    {
        int playerX = 200, playerY = 300;

        BaseRenderer renderer;

        public Game()
        {
            this.renderer = new RaylibRenderer(playerX, playerY);
        }

        public void Run()
        {
            this.renderer.Draw();
        }
    }
}
