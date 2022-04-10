using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using System.IO;
using TileBasedPlatformer.AnimationSystem;
using TileBasedPlatformer.Src.Entities;
using TileBasedPlatformer.Src.FileManagment;
using TileBasedPlatformer.Src.InputSystem;

namespace TileBasedPlatformer.Src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        ScalingViewportAdapter viewportAdapter;
        OrthographicCamera camera;

        private World world;
        private Player player;

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
            "#.....................####...#\n" +
            "#.......H#########...........#\n" +
            "#.......H....................#\n" +
            "#.......H....................#\n" +
            "#.......H....................#\n" +
            "#....####....####....####....#\n" +
            "#.S..........................#\n" +
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
            camera = new OrthographicCamera(viewportAdapter);

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

            Location spawnPos = Location.zero;
            for (int i = 0; i < world.GetDim().x; i++)
            {
                for (int j = 0; j < world.GetDim().y; j++)
                {
                    Tile tile = world.GetTile(new Location(i, j));
                    if (tile.Type == TileType.spawn)
                    {
                        spawnPos = tile.Pos;
                        break;
                    }
                }
            }
            player = new Player(spawnPos, new Vector2(1, 1), playerAnimManager, input);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            input.Update();

            float dt = gameTime.GetElapsedSeconds();

            float speed = 4;
            Vector2 dir = Vector2.Zero;
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                dir.Y -= 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                dir.Y += 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                dir.X -= 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dir.X += 1;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                camera.ZoomIn(dt * 1.1f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                camera.ZoomOut(dt * 1.1f);
            }

            dir.Normalize();
            dir *= speed * gameTime.GetElapsedSeconds();

            if (dir.LengthSquared() > 0)
            {
                camera.Move(dir);
            }

            player.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            for(int i = 0; i < world.GetDim().x; i++)
            {
                for(int j = 0; j < world.GetDim().y; j++)
                {
                    world.GetTile(new Location(i, j)).Draw(_spriteBatch);
                }
            }

            player.Draw(_spriteBatch);

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
