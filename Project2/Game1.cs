using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera camera;
        private Player player;
        private World world;
        private GameContent gameContent;
        private Texture2D pixel;
        private int screenWidth = 0;
        private int screenHeight = 0;
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            if (screenWidth >= 1024)
            {
                screenWidth = 1024;
            }
            if (screenHeight >= 1024)
            {
                screenHeight = 1024;
            }
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();
            player = new Player(GraphicsDevice, _spriteBatch);
            camera = new Camera(GraphicsDevice.Viewport, player, _spriteBatch);
            gameContent = new GameContent(Content);
            world = new World(9959565, player, camera, gameContent, GraphicsDevice, _spriteBatch);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.Red });
            //tile = new Tile(new Vector2(0, 0), new Vector2(0, 0), Color.White, pixel, camera, GraphicsDevice, _spriteBatch);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                player.MoveLeft();
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                player.MoveRight();
            }
            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                player.MoveUp();
            }
            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                player.MoveDown();
            }

            oldMouseState = newMouseState; // this saves the old state
            oldKeyboardState = newKeyboardState;
            // TODO: Add your update logic here
            camera.UpdateCamera(GraphicsDevice.Viewport);
            world.generateChunk();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.Transform);
            world.Draw();

            player.Draw();
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
