using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CombatSystem;

namespace TileBasedPlatformer.Src.EntityStateMachine
{
    public abstract class EntityState
    {
        protected Entity entity;
        protected AnimationManager manager;

        protected List<AttackBox> attacks = new List<AttackBox>();
        protected List<BodyBox> bodies = new List<BodyBox>();

        public EntityState(Entity entity, AnimationManager manager)
        {
            this.entity = entity;
            this.manager = manager;

            bodies.Add(new BodyBox(new RectangleF(entity.pos, entity.dim), entity, Vector2.Zero));
        }

        public virtual void Update(float dt, Vector2 pos)
        {
            foreach(var attack in attacks)
            {
                attack.box.Position = pos + attack.offset;
            }
            foreach(var body in bodies)
            {
                body.box.Position = pos + body.offset;
            }
        }

        public List<AttackBox> GetAttackBoxes()
        {
            return attacks;
        }

        public List<BodyBox> GetBodyBoxes()
        {
            return bodies;
        }
    }
}
