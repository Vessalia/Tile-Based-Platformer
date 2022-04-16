using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer
{
    public struct Location
    {
        public int x, y;
        public static Location zero = new Location(0, 0);

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Location(Location location)
        {
            x = location.x;
            y = location.y;
        }

        public static implicit operator Vector2(Location location) => new Vector2(location.x, location.y);

        public static implicit operator Location(Vector2 vec) => new Location((int)Math.Round(vec.X), (int)Math.Round(vec.Y));

        public static bool operator ==(Location a, Location b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Location a, Location b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static Location operator +(Location a, Location b)
        {
            return new Location(a.x + b.x, a.y + b.y);
        }

        public static Location operator -(Location a, Location b)
        {
            return new Location(a.x - b.x, a.y - b.y);
        }

        public static Location operator *(Location a, Location b)
        {
            return new Location(a.x * b.x, a.y * b.y);
        }

        public static Location operator /(Location a, Location b)
        {
            return new Location(a.x / b.x, a.y / b.y);
        }

        public static Location Zero()
        {
            return zero;
        }
    }
}
