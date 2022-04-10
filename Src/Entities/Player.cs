using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.EntityStateMachineSystem;
using TileBasedPlatformer.Src.FileManagment;
using TileBasedPlatformer.Src.InputSystem;

namespace TileBasedPlatformer.Src.Entities
{
    public class Player : Entity
    {
        public float speed;
        private IInput input;
        private ConfigManager dataManager;

        public Player(Vector2 initialPos, Vector2 dim, AnimationManager animManager, IInput input) : base (initialPos, dim, animManager)
        {
            this.input = input;

            FileManager<ConfigData> fileManager = new FileManager<ConfigData>(Constants.configPath);
            ConfigData data = fileManager.ReadData();
            dataManager = new ConfigManager(data);

            state = new PlayerIdleState(this, animManager);
            speed = 10;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            
        }

        public void SetState(EntityState state)
        {
            this.state = state;
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
