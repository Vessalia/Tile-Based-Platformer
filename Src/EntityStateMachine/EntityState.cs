using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;

namespace TileBasedPlatformer.Src.EntityStateMachine
{
    public abstract class EntityState
    {
        protected Entity entity;
        protected AnimationManager manager;

        public EntityState(Entity entity, AnimationManager manager)
        {
            this.entity = entity;
            this.manager = manager;
        }

        public abstract void Update(float dt);
    }
}
