using System;

namespace StealthGame
{
    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (var game = new StealthGame(args))
            {
                game.Run();
            }
        }
    }
}