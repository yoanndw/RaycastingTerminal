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
        Player player = new Player(7, 7, -90);

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
            this.renderer = new RaylibRenderer(this.map, this.player);
        }

        public void Run()
        {
            this.renderer.Draw();
        }
    }
}
