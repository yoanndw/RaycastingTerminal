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
        protected Texture2D monsterImage;

        public BaseRenderer(Image wallImage)
        {
            this.wallImage = wallImage;
        }

        public void Run()
        {
            
        }

        public abstract void InitFrame();
        public void UpdateImages(Image wallImage, Texture2D monsterImage)
        {
            this.wallImage = wallImage;
            this.monsterImage = monsterImage;
        }
        public abstract void CloseFrame();
        public abstract bool IsRunning();
        public abstract void Draw();
    }
}
