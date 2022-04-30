using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CombatSystem;
using TileBasedPlatformer.Src.Core;
using TileBasedPlatformer.Src.Entities;

namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
{
    internal class PlayerFreeFallState : EntityState
    {
        private Player Player { get { return (Player)entity; } }
        private string anim;
        
        private float timer = 0;
        private bool finishedPeak = false;

        public PlayerFreeFallState(Player entity, AnimationManager manager) : base(entity, manager)
        {
            if (Player.vel.Y > 0) anim = "rising";
            else anim = "falling";
            manager.LoadContent(anim);

            attacks.Clear();
            bodies.Clear();

            attacks.Add(new AttackBox(new RectangleF(Player.pos, Player.dim - new Vector2(0, 0.4f)), Player, 1, 4));
            bodies.Add(new BodyBox(new RectangleF(Player.pos - new Vector2(0, 1), Player.dim - new Vector2(0, 0.6f)), Player));
        }

        public override void Update(float dt, Vector2 pos)
        {
            base.Update(dt, pos);
            HandleInput();

            if (anim.Equals("jump_peak") && timer < manager.GetAnimationDuration())
            {
                timer += dt;
                finishedPeak = true;
            }

            if (Player.vel.Y >= -2.0f && Player.vel.Y <= 2.0f && !anim.Equals("jump_peak") && !finishedPeak)
            {
                anim = "jump_peak";
                manager.LoadContent(anim);
            }
            else if (Player.vel.Y < 0.0f && !anim.Equals("falling"))
            {
                anim = "falling";
                manager.LoadContent(anim);
            }

            float speed = 0;
            if ((Player.IsInputDown("left") || Player.IsInputDown("right")) && !(Player.IsInputDown("left") && Player.IsInputDown("right")))
            {
                speed = Player.speed;
                if (Player.IsFacingLeft())
                {
                    speed *= -1;
                }
            }

            Player.vel.X = speed;

            float epsilon = 0.001f;
            Player.pos.Y += epsilon;
            if (CollisionResolver.Resolve(entity, false) == CollisionSide.Top)
            {
                attacks.Clear();
                bodies.Clear();
                bodies.Add(new BodyBox(new RectangleF(Player.pos, Player.dim), Player));

                Player.SetState(new PlayerLandingState(Player, manager));
                return;
            }
            else Player.pos.Y -= epsilon;

            manager.Update(dt, anim);
        }

        private void HandleInput()
        {
            int dir = Player.GetXDir();
            Player.SetDir(dir);
        }
    }
}
