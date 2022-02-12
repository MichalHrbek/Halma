using Raylib_cs;
using System.Numerics;
using System;

namespace Halma
{
    static class Program
    {
        public static void Main()
        {
            //Possible moves
            Vector2[] moves = new Vector2[6]; moves[0] = new Vector2(1, 0); moves[1] = new Vector2(0, 1); moves[2] = new Vector2(1, -1); moves[3] = new Vector2(-1, 0); moves[4] = new Vector2(0, -1); moves[5] = new Vector2(-1, 1);
            const int screenWidth = 900, screenHeight = 900;
            int xOffset = -150, yOffset = 50, pixelSize = 50; // Board render options
            int sx = 0, sy = 0; // selected cell
            bool selected = false, jumped = false;
            int players = 2, turn = 2;

            // Player Colors
            Color[] colors = new Color[7]; colors[1] = Color.BLACK; colors[2] = Color.BLUE; colors[3] = Color.DARKGREEN;
            
            // Game Grid
            int[,] grid = new int[17, 17] {
                { 0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0 },
                 { 0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0 },
                  { 0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0 },
                   { 0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0 },
                    { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                     { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                      { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0 },
                       { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0 },
                        { 0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0 },
                         { 0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
                          { 0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
                           { 0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
                            { 1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
                             { 0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0 },
                              { 0,0,0,0,3,3,3,0,0,0,0,0,0,0,0,0,0 },
                               { 0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 }};

            Raylib.InitWindow(screenWidth, screenHeight, "Halma");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BEIGE);

                if (selected) // Draw black border around cell when selected
                {
                    Raylib.DrawCircle(xOffset + sx * pixelSize + (sy * pixelSize) / 2, yOffset + sy * pixelSize, 12, Color.BLACK);
                }

                Raylib.DrawCircle(pixelSize, pixelSize, 12, colors[turn]); // Indicates whos turn it is

                for (int y = 0; y < grid.GetLength(0); y++) // Draw the board
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        if (grid[y, x] !=0) // Ignores empty cells
                            Raylib.DrawCircle(xOffset + x * pixelSize + (y * pixelSize) /2, yOffset + y * pixelSize, 10, colors[grid[y, x]]);
                    }
                }
                
                if (Raylib.IsMouseButtonPressed(0))
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        for (int y = 0; y < grid.GetLength(0); y++)
                        {
                            if (grid[y, x] != 0)
                            {
                                if (Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new Vector2(xOffset + x * pixelSize + (y * pixelSize) / 2, yOffset + y * pixelSize), 20))
                                {
                                    if (selected)
                                    {
                                        if (grid[y,x] == 1)
                                        {
                                            if (!jumped)
                                            {
                                                if(Check(x,y,1))
                                                {
                                                    grid[y, x] = grid[sy, sx];
                                                    grid[sy, sx] = 1;
                                                    sx = x;
                                                    sy = y;
                                                    selected = false;
                                                    Play();
                                                }
                                            }
                                            if(Check(x,y,2))
                                            {
                                                grid[y, x] = grid[sy, sx];
                                                grid[sy, sx] = 1;
                                                sx = x;
                                                sy = y;
                                                jumped = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (grid[y, x] == turn)
                                        {
                                            sx = x;
                                            sy = y;
                                            selected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON)) // Finsish the move
                {
                    if (jumped)
                    {
                        jumped = false;
                        Play();
                    }
                    selected = false;
                }

                if(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) // Doesnt work
                {
                    AI();
                }

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();

            bool Check(int x, int y, int jumpLenght) // Checks if a move is possible
            {
                for (int i = 0; i < moves.Length; i++)
                {
                    if (new Vector2(sx - x, sy - y) == moves[i] * jumpLenght)
                    {
                        if (jumpLenght == 1)
                        {
                            return true;
                        }
                        else if (grid[y + (int)moves[i].Y, x + (int)moves[i].X] != 1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            void Play() // Switches turns
            {
                if (turn > players)
                {
                    turn = 2;
                }
                else
                {
                    turn++;
                }
            }

            void AI()
            {
                /*bool[,] posibleJump = new bool[17, 17];
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    for (int y = 0; y < grid.GetLength(0); y++)
                    {
                        if (grid[y,x] == turn)
                        {
                            for (int i = 0; i < moves.Length; i++)
                            {
                                try
                                {
                                    if (grid[y + (int)moves[i].Y, x + (int)moves[i].X] == 1)
                                    {
                                        Console.WriteLine(y);
                                        Raylib.DrawCircle(
                                            xOffset + (x + (int)moves[i].X) * pixelSize + ((y + (int)moves[i].Y) * pixelSize) /2,
                                            yOffset + (y + (int)moves[i].Y) * pixelSize,
                                            10, Color.WHITE);
                                    }
                                    else if (grid[y + (int)moves[i].Y*2, x + (int)moves[i].X*2] == 1)
                                    {
                                        int ay = x;
                                        int ax = y;
                                        while (true)
                                        {
                                            Raylib.DrawCircle(
                                                xOffset + (x + (int)moves[i].X*2) * pixelSize + ((y + (int)moves[i].Y*2) * pixelSize) / 2,
                                                yOffset + (y + (int)moves[i].Y*2) * pixelSize,
                                                10, Color.WHITE);
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }*/
            }
            int CheckMove(int x, int y, int goalX, int goalY) // Returns move score for AI
            {
                return 0;
            }
        }

    }
}