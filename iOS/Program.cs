using Foundation;
using System;
using UIKit;

using GameLogic;

namespace iOS
{
    [Register("AppDelegate")]
    internal class Program : UIApplicationDelegate
    {
        private static Game1 game;

        internal static void RunGame()
        {
            game = new Game1();
            game.IsMouseVisible = true; // show mouse
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, typeof(Program));
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
