using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_LDtk_Importer;

namespace App
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private LDtkProject project;
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
            font = Content.Load<SpriteFont>("font");
            project = Content.Load<LDtkProject>("Test_file_for_API_showing_all_features");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (GamePad.GetCapabilities(PlayerIndex.One).DisplayName != null)
            {
                _spriteBatch.DrawString(font, GamePad.GetCapabilities(PlayerIndex.One).DisplayName, new Vector2(0, 0), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(font, "LDtk Project loaded correctly :,)", new Vector2(0, 0), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
