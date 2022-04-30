using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.IO;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.CameraSystem;
using TileBasedPlatformer.Src.CombatSystem;
using TileBasedPlatformer.Src.Entities;
using TileBasedPlatformer.Src.FileManagment;
using TileBasedPlatformer.Src.InputSystem;

namespace TileBasedPlatformer.Src.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ScalingViewportAdapter viewportAdapter;
        private CameraController cameraController;

        private World world;
        private Entity player;

        private List<Entity> enemies = new List<Entity>();

        private List<Entity> entities = new List<Entity>();

        private List<ICombat> combatables = new List<ICombat>();

        public static Input input;

        private string worldString =
            "##############################\n" +
            "#..........................WW#\n" +
            "#..........................WW#\n" +
            "#....................#########\n" +
            "#..###################.......#\n" +
            "#........................#...#\n" +
            "#........................#...#\n" +
            "#........................#...#\n" +
            "#........................#...#\n" +
            "#...##############.......#...#\n" +
            "#........................#...#\n" +
            "#........................#...#\n" +
            "#...........1.........####...#\n" +
            "#.......H#########...........#\n" +
            "#..S....H....................#\n" +
            "#.......H....................#\n" +
            "#.......H......1......1......#\n" +
            "#....####....####....####....#\n" +
            "#..........................1.#\n" +
            "##############################"   ;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            viewportAdapter = new ScalingViewportAdapter(GraphicsDevice, Constants.WorldSpace.x, Constants.WorldSpace.y);
            cameraController = new CameraController(new OrthographicCamera(viewportAdapter));

            _graphics.PreferredBackBufferWidth = (int)Constants.Screen.X;
            _graphics.PreferredBackBufferHeight = (int)Constants.Screen.Y;
            _graphics.ApplyChanges();

            InitializeFiles();

            input = new Input();

            world = new World(worldString);
            CollisionResolver.SetWorld(world);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("fireknight.sf", new JsonContentLoader());
            AnimationManager playerAnimManager = new AnimationManager(spriteSheet);

            SpriteSheet slimeSpriteSheet = Content.Load<SpriteSheet>("slime-Sheet.sf", new JsonContentLoader());
            List<AnimationManager> slimeAnimManagers = new List<AnimationManager>();
            int numSlimes = 3;
            for(int i = 0; i < numSlimes; i++)
            {
                slimeAnimManagers.Add(new AnimationManager(slimeSpriteSheet));
            }

            Location spawnPos = Location.zero;
            List<Location> slimeSpawnPos = new List<Location>();
            for (int i = 0; i < world.GetDim().x; i++)
            {
                for (int j = 0; j < world.GetDim().y; j++)
                {
                    Tile tile = world.GetTile(new Location(i, j));
                    if (tile.Type == TileType.spawn)
                    {
                        spawnPos = tile.Pos;
                    }
                    else if(tile.Type == TileType.slimeSpawn)
                    {
                        slimeSpawnPos.Add(tile.Pos);
                    }
                }
            }

            player = new Player(spawnPos, new Vector2(1, 1), playerAnimManager, input, 10, 4);
            cameraController.AddTargets(player);

            Random slimeSpawnIdx = new Random();
            foreach (var slimeAnimManager in slimeAnimManagers)
            {
                int idx = slimeSpawnIdx.Next(slimeSpawnPos.Count);
                enemies.Add(new Enemy(slimeSpawnPos[idx], new Vector2(1, 1), slimeAnimManager, 5));
                slimeSpawnPos.RemoveAt(idx);
            }

            foreach (var enemy in enemies)
            {
                //cameraController.AddTargets(enemy);
            }

            entities.Add(player);
            entities.AddRange(enemies);

            combatables.AddRange(entities);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            input.Update();

            float dt = gameTime.GetElapsedSeconds();

            cameraController.Update(dt);

            if (input.IsKeyDown(Keys.LeftShift))
            {
                cameraController.ZoomIn(dt);
            }
            else if (input.IsKeyDown(Keys.LeftControl))
            {
                cameraController.ZoomOut(dt);
            }

            if(!player.IsDead()) player.Update(dt);

            foreach(var enemy in enemies)
            {
                enemy.Update(dt);
            }

            foreach(ICombat combatable in combatables)
            {
                foreach(ICombat combatant in combatables)
                {
                    if(combatable == combatant) continue;
                    combatable.DealDamage(combatant);
                }
            }

            List<Entity> deadEntities = new List<Entity>();
            foreach(var entity in entities)
            {
                if(entity.IsDead()) deadEntities.Add(entity);
            }

            entities.RemoveAll(e => deadEntities.Contains(e));
            enemies.RemoveAll(e => !entities.Contains(e));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            entities.Sort((entityA, entityB) => entityA.zIdx - entityB.zIdx);

            _spriteBatch.Begin(transformMatrix: cameraController.Camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            for(int i = 0; i < world.GetDim().x; i++)
            {
                for(int j = 0; j < world.GetDim().y; j++)
                {
                    world.GetTile(new Location(i, j)).Draw(_spriteBatch);
                }
            }

            foreach(var entity in entities)
            {
                entity.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void InitializeFiles()
        {
            if (!File.Exists(Path.GetFullPath(Constants.configPath)))
            {
                ConfigData confData = new ConfigData();

                confData.Volume = 100;

                confData.KeyNames.Add("left");
                confData.KeyBindings.Add(Keys.Left);

                confData.KeyNames.Add("right");
                confData.KeyBindings.Add(Keys.Right);

                confData.KeyNames.Add("up");
                confData.KeyBindings.Add(Keys.Up);

                confData.KeyNames.Add("down");
                confData.KeyBindings.Add(Keys.Down);

                string configFullPath = Path.GetFullPath(Constants.configPath);
                FileManager<ConfigData> confFileManager = new FileManager<ConfigData>(configFullPath);
                confFileManager.WriteData(confData);
            }
        }
    }
}
