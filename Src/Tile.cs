using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TileBasedPlatformer.Src
{
    public enum TileType
    {
        collider, ladder, spawn, win, empty
    }

    public class Tile
    {
        public Location Pos { get; private set; }
        public TileType Type { get; private set; } 

        public Tile(Location Pos, TileType Type)
        {
            this.Pos = Pos;
            this.Type = Type;
        }

        public void Draw(SpriteBatch sb)
        {
            Color colour = Color.Transparent; 
            if (Type == TileType.collider)
            {
                colour = Color.Black;
            }
            else if (Type == TileType.ladder)
            {
                colour = Color.Green;
            }
            else if (Type == TileType.spawn)
            {
                colour = Color.Red;
            }
            else if (Type == TileType.win)
            {
                colour = Color.Yellow;
            }

            sb.FillRectangle(Pos.x, Pos.y, 1, 1, colour);
        }
    }
}
