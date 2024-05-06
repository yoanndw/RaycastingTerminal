using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting.Rendering
{
    public abstract class BaseRenderer
    {
        protected Image wallImage;

        public BaseRenderer(Image wallImage)
        {
            this.wallImage = wallImage;
        }

        public void Run()
        {
            
        }

        public abstract void InitFrame();
        public void UpdateWallImage(Image wallImage)
        {
            this.wallImage = wallImage;
        }
        public abstract void CloseFrame();
        public abstract bool IsRunning();
        public abstract void Draw();
        public abstract void RenderPixel(int posScrX, int posScrY, Color color);
    }
}
