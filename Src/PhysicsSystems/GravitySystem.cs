using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.PhysicsSystems
{
    public static class GravitySystem
    {
        private static float gravity = 1.0f;

        public static void Update(Entity entity, float dt)
        {
            entity.vel.Y += gravity * dt;
            entity.pos.Y += entity.vel.Y * dt;
            CollisionResolver.Resolve(entity, false);
        }
    }
}
