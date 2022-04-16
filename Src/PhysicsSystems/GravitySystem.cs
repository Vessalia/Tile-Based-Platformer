using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.PhysicsSystems
{
    public static class GravitySystem
    {
        private static float gravity = 5.0f;

        public static void Update(Entity entity, float dt)
        {
            entity.vel.Y -= gravity * dt;
            entity.vel.Y = Math.Clamp(entity.vel.Y, -entity.terminalVel, entity.terminalVel);
        }
    }
}
