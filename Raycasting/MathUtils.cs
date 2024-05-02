using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    internal static class MathUtils
    {
        public static float Deg2Rad(float deg)
        {
            return deg / 180 * MathF.PI;
        }

        public static float Rad2Deg(float rad)
        {
            return rad / MathF.PI * 180;
        }
    }
}
