using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachineSystem.PlayerState;
using TileBasedPlatformer.Src.FileManagment;
using TileBasedPlatformer.Src.InputSystem;
using TileBasedPlatformer.Src.PhysicsSystems;

namespace TileBasedPlatformer.Src.Entities
{
    public class Player : Entity
    {
        private IInput input;
        private ConfigManager dataManager;

        public Player(Vector2 initialPos, Vector2 dim, AnimationManager animManager, IInput input, float speed) : base (initialPos, dim, animManager, speed)
        {
            this.input = input;

            FileManager<ConfigData> fileManager = new FileManager<ConfigData>(Constants.configPath);
            ConfigData data = fileManager.ReadData();
            dataManager = new ConfigManager(data);

            state = new PlayerRunningState(this, animManager);
        }

        public override void Draw(SpriteBatch sb)
        {
            var sprite = animManager.GetCurrentSprite();
            float scale = 2f / sprite.TextureRegion.Height;
            if (facingLeft)
            {
                sprite.Effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                sprite.Effect = SpriteEffects.None;
            }
            sb.Draw(sprite, pos + new Vector2(1, 0) / 2, 0, new Vector2(scale, scale));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            GravitySystem.Update(this, dt);
        }

        public int GetXDir()
        {
            int xDir = 0;
            if (IsInputDown("right"))
            {
                xDir += 1;
            }
            if (IsInputDown("left"))
            {
                xDir -= 1;
            }

            return xDir;
        }

        public bool IsInputJustPressed(string key)
        {
            return input.IsKeyJustPressed(dataManager.GetKeyBinding(key));
        }

        public bool IsInputDown(string key)
        {
            return input.IsKeyDown(dataManager.GetKeyBinding(key));
        }

        public bool IsInputJustReleased(string key)
        {
            return input.IsKeyJustReleased(dataManager.GetKeyBinding(key));
        }

        public bool IsInputUp(string key)
        {
            return input.IsKeyUp(dataManager.GetKeyBinding(key));
        }
    }
}
