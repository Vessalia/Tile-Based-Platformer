﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachineSystem;

namespace TileBasedPlatformer.Src
{
    public abstract class Entity
    {
        protected bool facingLeft = false; 
        protected EntityState state;

        protected AnimationManager animManager;

        public Vector2 pos;
        public Vector2 dim;
        public Vector2 vel;

        public float speed;
        public float terminalVel;

        public Entity(Vector2 initialPos, Vector2 dim, AnimationManager animManager, float speed)
        {
            pos = initialPos;
            this.dim = dim;
            this.animManager = animManager;

            this.speed = speed;
            terminalVel = 15 * speed;
        }

        public Entity(float initialX, float initialY)
        {
            pos = new Vector2(initialX, initialY);
        }

        public virtual void Update(float dt)
        {
            state.Update(dt);
        }

        public void Draw(SpriteBatch sb)
        {
            state.Draw(sb);
        }

        public void SetState(EntityState state)
        {
            this.state = state;
        }

        public void SetFacingLeft(bool facingLeft)
        {
            this.facingLeft = facingLeft;
        }

        public bool IsFacingLeft()
        {
            return facingLeft;
        }
    }
}
