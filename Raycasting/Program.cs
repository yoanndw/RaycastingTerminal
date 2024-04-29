using Raylib_cs;

Raylib.InitWindow(800, 600, "Raycasting");
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);

    Raylib.DrawText("Texte", 200, 200, 30, Color.Black);
    Raylib.DrawLine(100, 100, 200, 200, Color.Red);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
