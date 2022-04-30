using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    internal class PlayerStunnedState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private float timer = 0;

        public PlayerStunnedState(Entity entity, AnimationManager manager) : base(entity, manager)
        {
            manager.LoadContent("hit");
            Player.zIdx = 0;
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            timer += dt;
            if (manager.GetAnimationDuration() <= timer)
            {
                Player.SetState(new PlayerFreeFallState(Player, manager));
                return;
            }
            manager.Update(dt, "hit");
        }
    }
}
