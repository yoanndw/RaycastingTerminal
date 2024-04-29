using Raylib_cs;

using Raycasting;

public class Program
{
    static int playerX = 400, playerY = 300;
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Run();
    }

    static void DrawPlayer()
    {
        Raylib.DrawCircle(playerX, playerY, 10, Color.Green);
    }
}