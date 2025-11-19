using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game_prototype_1.PerlinGen;

namespace Game_prototype_1
{
 
    internal class PerlinGen
    {
        public enum TileType { Ocean, GrassLands, Forest, Desert, Mountains }

       public class TileInfo
        {
            public int Col { get; set; }
            public int Row { get; set; }
            public TileType Type { get; set; }
            public int Level { get; set; }
            public bool HasFactory { get; set; }
            public string FactoryType { get; set; }
        }
     
        public class PerlinNoise
        {
            public readonly int[] perm;
            public PerlinNoise(int seed)
            {
                perm = new int[512];
                int[] p = new int[256];
                Random rnd = new Random(seed);
                for (int i = 0; i < 256; i++)
                {
                    p[i] = i;
                }
                for (int i = 255; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    int tmp = p[i];
                    p[i] = p[j]; p[j] = tmp;
                }
                for (int i = 0; i < 512; i++)
                {
                    perm[i] = p[i & 255];
                }
            }
            public float Noise(float x, float y)
            {
                int X = FastFloor(x) & 255;
                int Y = FastFloor(y) & 255;
                float xf = x - (float)Math.Floor(x);
                float yf = y - (float)Math.Floor(y);
                float u = Fade(xf);
                float v = Fade(yf);
                int aa = perm[X + perm[Y]];
                int ab = perm[X + perm[Y + 1]];
                int ba = perm[X + 1 + perm[Y]];
                int bb = perm[X + 1 + perm[Y + 1]];
                float x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u);
                float x2 = Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u);
                return Lerp(x1, x2, v);
            }
            public static int FastFloor(float x) => x > 0 ? (int)x : (int)x - 1;
            public static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);
            public static float Lerp(float a, float b, float t) => a + t * (b - a);
            public static float Grad(int hash, float x, float y)
            {
                int h = hash & 7;
                float u = h < 4 ? x : y;
                float v = h < 4 ? y : x;
                return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
            }
        }
    }
}
