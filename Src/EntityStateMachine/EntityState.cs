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

            attacks.Add(new AttackBox(new RectangleF(entity.pos, entity.dim), entity, 0.000001f, 4));
            bodies.Add(new BodyBox(new RectangleF(entity.pos, entity.dim), entity));
        }

        public virtual void Update(float dt, Vector2 pos)
        {
            foreach(var attack in attacks)
            {
                attack.box.Position = pos;
            }
            foreach(var body in bodies)
            {
                body.box.Position = pos;
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
