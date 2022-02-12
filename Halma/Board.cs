using Raylib_cs;
using System.Numerics;
using System.Text.Json;

namespace Halma
{
    class Board //Not used
    {
        public static void TriDraw(int xStart, int yStart, int width, int height, Color col, bool up, int num)
        {
            if (up)
            {
                for (int y = 0; y < num; y++)
                {
                    int xAdd = xStart - y * (width / 2) - width / 2;
                    for (int x = 0; x < y + 1; x++)
                    {
                        SimpTri(x * 2, y * 2, xAdd, yStart, width, height, col, true);
                    }
                }
            }
            else
            {
                yStart -= height;
                for (int y = 0; y < num; y++)
                {
                    int xAdd = xStart - (y) * (width / 2) - width / 2;
                    for (int x = 0; x < y + 1; x++)
                    {
                        SimpTri(x * 2, y * 2, xAdd, yStart, width, height, col, false);
                    }
                }
            }
        }
        static void SimpTri(int x, int y, int xAdd, int yStart, int width, int height, Color col, bool up)
        {
            int d = 3;
            Vector2[] vec = new Vector2[3];
            if (up)
            {
                vec[0] = new Vector2(xAdd + x * (width / 2), yStart + height * y / 2 + height); vec[1] = new Vector2(xAdd + x * (width / 2) + width, yStart + height * y / 2 + height); vec[2] = new Vector2(xAdd + x * (width / 2) + width / 2, yStart + height * y / 2);
                Raylib.DrawTriangleLines(
                    new Vector2(xAdd + x * (width / 2), yStart + height * y / 2 + height),
                    new Vector2(xAdd + x * (width / 2) + width, yStart + height * y / 2 + height),
                    new Vector2(xAdd + x * (width / 2) + width / 2, yStart + height * y / 2),
                    col);
            }
            else
            {
                vec[0] = new Vector2(xAdd + x * (width / 2) + width / 2, yStart - height * y / 2 + height); vec[1] = new Vector2(xAdd + x * (width / 2) + width, yStart - height * y / 2); vec[2] = new Vector2(xAdd + x * (width / 2), yStart - height * y / 2);
                Raylib.DrawTriangleLines(
                    new Vector2(xAdd + x * (width / 2) + width / 2, yStart - height * y / 2 + height),
                    new Vector2(xAdd + x * (width / 2) + width, yStart - height * y / 2),
                    new Vector2(xAdd + x * (width / 2), yStart - height * y / 2),
                    col);
            }
            /*Raylib.DrawCircleV(vec[0], d, Color.DARKBROWN);
            Raylib.DrawCircleV(vec[1], d, Color.DARKBROWN);
            Raylib.DrawCircleV(vec[2], d, Color.DARKBROWN);*/
        }
    }
}
