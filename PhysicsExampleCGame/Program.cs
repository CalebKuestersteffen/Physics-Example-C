using System;

namespace PhysicsExampleCGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PhysicsExampleCGame())
                game.Run();
        }
    }
}
