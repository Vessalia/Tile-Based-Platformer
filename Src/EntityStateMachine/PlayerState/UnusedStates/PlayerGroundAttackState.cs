//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TileBasedPlatformer.AnimationSystem;
//using TileBasedPlatformer.Src.Entities;

//namespace TileBasedPlatformer.Src.EntityStateMachine.PlayerState
//{
//    internal class PlayerGroundAttackState : EntityState
//    {
//        private Player Player { get { return (Player)entity; } }
//        private string anim;

//        private bool enterNextAttack = false;
//        private float timer = 0;

//        public PlayerGroundAttackState(Player entity, AnimationManager manager) : base(entity, manager)
//        {
//            anim = "attack1";
//            manager.LoadContent(anim);
//            Player.vel = Vector2.Zero;
//            Player.zIdx = 2;
//        }

//        public override void Update(float dt, Vector2 pos)
//        {
//            base.Update(dt, pos);
//            timer += dt;
//            if(Player.IsInputJustPressed("attack") && !enterNextAttack && !(anim.Equals("attack3") || anim.Equals("attack1_return_to_idle")))
//            {
//                enterNextAttack = true;
//            }

//            if(timer >= manager.GetAnimationDuration() && !enterNextAttack)
//            {
//                if(anim.Equals("attack1"))
//                {
//                    anim = "attack1_return_to_idle";
//                    manager.LoadContent(anim);
//                    timer = 0;
//                }
//                else
//                {
//                    Player.SetState(new PlayerIdleState(Player, manager));
//                    return;
//                }
//            }
//            else if(timer >= manager.GetAnimationDuration() && enterNextAttack)
//            {
//                timer = 0;
//                enterNextAttack = false;
//                anim = anim.Equals("attack1") ? "attack2" : "attack3";
//                manager.LoadContent(anim);
//            }

//            manager.Update(dt, anim);
//        }
//    }
//}
