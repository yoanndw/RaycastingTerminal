using Raylib_cs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting.Rendering
{
    public abstract class BaseRenderer
    {
        public BaseRenderer() { }

        public abstract void Draw();
    }
}
