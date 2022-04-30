using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.Core
{
    public static class Constants
    {
        private static readonly int Width = 1280;
        private static readonly int Height = 720;

        private static readonly int WorldWidth = 30;
        private static readonly int WorldHeight = 20;

        public static readonly string configPath = "Config/Settings.ples";

        public static readonly Vector2 Screen = new Vector2(Width, Height);
        public static readonly Location WorldSpace = new Location(WorldWidth, WorldHeight);

        public static Vector2 PixelsPerWorldUnit()
        {
            return new Vector2(Screen.X / WorldSpace.x, Screen.Y / WorldSpace.y);
        }
    }
}
