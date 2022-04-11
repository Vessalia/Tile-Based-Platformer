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

        public static void Resolve(Entity entity, bool xCheck)
        {
            List<Tile> tiles = GetNeighbours(entity.pos);
            foreach (var tile in tiles)
            {
                TileResolve(entity, tile, xCheck);
            }
        }

        private static List<Tile> GetNeighbours(Location pos)
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

        public static void TileResolve(Entity entity, Tile collisionTile, bool xCheck)
        {
            if (collisionTile.Type != TileType.collider) return;

            RectangleF entityRect = new RectangleF(entity.pos.X, entity.pos.Y, entity.dim.X, entity.dim.Y);
            RectangleF worldRect = new RectangleF(collisionTile.Pos.x, collisionTile.Pos.y, 1, 1);

            if (!entityRect.Intersects(worldRect)) return;

            CollisionSide side = GetCollisionSide(entityRect, worldRect);
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
                    entity.pos.Y = collisionTile.Pos.y - 1;
                }
                else if(side == CollisionSide.Bottom)
                {
                    entity.pos.Y = collisionTile.Pos.y + entity.dim.Y;
                }
            }
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
