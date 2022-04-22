using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src
{
    public enum CollisionSide
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    public static class CollisionResolver
    {
        private static World world;

        public static CollisionSide? Resolve(Entity entity, bool xCheck)
        {
            List<Tile> tiles = GetEntityNeighbours(entity);
            foreach (var tile in tiles)
            {
                var side = TileResolve(entity, tile, xCheck);
                if(side != null)
                {
                    return side;
                }
            }

            return null;
        }

        private static List<Tile> GetEntityNeighbours(Entity entity)
        {
            List<Tile> neighbours = new List<Tile>();

            for (int x = 0; x < (int)Math.Ceiling(entity.dim.X); x++)
            {
                for(int y = 0; y < (int)Math.Ceiling(entity.dim.Y); y++)
                {
                    List<Tile> tileNeighbours = GetTileNeighbours((Location)entity.pos + new Location(x, y));
                    foreach(var tile in tileNeighbours)
                    {
                        if(!neighbours.Contains(tile))
                        {
                            neighbours.Add(tile);
                        }
                    }
                }
            }

            return neighbours;
        }

        private static List<Tile> GetTileNeighbours(Location pos)
        {
            List<Tile> neighbours = new List<Tile>();

            for (int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    neighbours.Add(world.GetTile(new Location(pos.x + x, pos.y + y)));
                }
            }

            return neighbours;
        }

        private static CollisionSide? CheckTileSide(Entity entity, Tile collisionTile)
        {
            if (collisionTile.Type != TileType.collider) return null;

            RectangleF entityRect = new RectangleF(entity.pos.X, entity.pos.Y, entity.dim.X, entity.dim.Y);
            RectangleF worldRect = new RectangleF(collisionTile.Pos.x, collisionTile.Pos.y, 1, 1);

            if (!entityRect.Intersects(worldRect)) return null;
            return GetCollisionSide(entityRect, worldRect);
        }

        private static CollisionSide? TileResolve(Entity entity, Tile collisionTile, bool xCheck)
        {
            var side = CheckTileSide(entity, collisionTile);

            if (xCheck)
            {
                if (side == CollisionSide.Left)
                {
                    entity.pos.X = collisionTile.Pos.x - entity.dim.X;
                }
                else if(side == CollisionSide.Right)
                {
                    entity.pos.X = collisionTile.Pos.x + 1;
                }
            }
            else
            {
                if (side == CollisionSide.Top)
                {
                    entity.pos.Y = collisionTile.Pos.y - entity.dim.Y;
                    entity.vel.Y = 0;
                }
                else if(side == CollisionSide.Bottom)
                {
                    entity.pos.Y = collisionTile.Pos.y + entity.dim.Y;
                    entity.vel.Y = 0;
                }
            }

            return side;
        }

        static CollisionSide GetCollisionSide(RectangleF r0, RectangleF r1)
        {
            CollisionSide result;

            bool isLeft = r0.X + r0.Width / 2 < r1.X + r1.Width / 2;
            bool isAbove = r0.Y + r0.Height / 2 < r1.Y + r1.Height / 2;

            //holds how deep the r1ect is inside the tile on each axis
            float horizontalDif;
            float verticalDif;

            //determine the differences for depth
            if (isLeft)
            {
                horizontalDif = r0.X + r0.Width - r1.X;
            }
            else
            {
                horizontalDif = r1.X + r1.Width - r0.X;
            }

            if (!isAbove)
            {
                verticalDif = r1.Y + r1.Height - r0.Y;
            }
            else
            {
                verticalDif = r0.Y + r0.Height - r1.Y;
            }

            if (horizontalDif < verticalDif)
            {
                if (isLeft)
                {
                    result = CollisionSide.Left;
                }
                else
                {
                    result = CollisionSide.Right;
                }
            }
            else if (isAbove)
            {
                result = CollisionSide.Top;
            }
            else
            {
                result = CollisionSide.Bottom;
            }

            return result;
        }

        public static void SetWorld(World world)
        {
             CollisionResolver.world = world;
        }
    }
}
