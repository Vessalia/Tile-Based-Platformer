using Microsoft.Xna.Framework;
using TileBasedPlatformer.Src.Entities;
using TileBasedPlatformer.Src.FileManagment;
using TileBasedPlatformer.Src.InputSystem;

namespace TileBasedPlatformer.Src.Controllers
{
    public class PlayerController : Controller
    {
        private IInput input;

        private ConfigManager dataManager;
        private Player Player { get { return (Player)owner; } }

        public PlayerController(Player owner, IInput input) : base(owner) 
        {
            this.input = input;

            FileManager<ConfigData> fileManager = new FileManager<ConfigData>(Constants.configPath);
            ConfigData data = fileManager.ReadData();
            dataManager = new ConfigManager(data);
        }

        public override void HandleInput()
        {
            int dir = 0;
            if (input.IsKeyDown(dataManager.GetKeyBinding("left"))) { dir += -1; }
            if (input.IsKeyDown(dataManager.GetKeyBinding("right"))) { dir += 1; }

            Player.SetXDir(dir);
        }
    }
}
