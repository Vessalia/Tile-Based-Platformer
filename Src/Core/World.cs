using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TileBasedPlatformer.Src.Core
{
    public class World
    {
        private readonly Location dim;
        private readonly Tile[,] world;

        public World(string worldString)
        {
            string[] worldArray = worldString.Split("\n");

            dim = new Location(worldArray[0].Length, worldArray.Length);
            world = new Tile[dim.x, dim.y];

            BuildWorld(worldArray);
        }

        private void BuildWorld(string[] worldArray)
        {
            for (int i = 0; i < dim.y; i++)
            {
                string row = worldArray[i];
                for (int j = 0; j < row.Length; j++)
                {
                    world[j, i] = ParseTile(new Location(j, i), row[j]);
                }
            }
        }

        private Tile ParseTile(Location pos, char tileChar)
        {
            TileType type = TileType.empty;

            switch (tileChar)
            {
                case '#':
                    type = TileType.collider;
                    break;
                case 'H':
                    type = TileType.ladder;
                    break;
                case 'S':
                    type = TileType.spawn;
                    break;
                case 'W':
                    type = TileType.win;
                    break;
                case '1':
                    type = TileType.slimeSpawn;
                    break;
            }

            return new Tile(pos, type);
        }

        public Tile GetTile(Location pos)
        {
            pos.x = Math.Clamp(pos.x, 0, dim.x - 1);
            pos.y = Math.Clamp(pos.y, 0, dim.y - 1);

            return world[pos.x, pos.y];
        }


        public Location GetDim()
        {
            return dim;
        }
    }
}
