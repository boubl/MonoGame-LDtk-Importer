using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_LDtk_Importer;
using System.Collections.Generic;
using System.IO;

namespace Example
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<string, Texture2D> tilesets = new Dictionary<string, Texture2D>();
        private LDtkProject project;
        private LDtkProject project2;
        private LDtkProject project3;
        private SpriteFont font;

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
            project = Content.Load<LDtkProject>("Test_file_for_API_showing_all_features");
            project2 = Content.Load<LDtkProject>("SeparateLevelFiles");
            project3 = Content.Load<LDtkProject>("FieldsTypes");
            font = Content.Load<SpriteFont>("font");
            tilesets.Add("_Soul_s_RPG_Graphics_Icons", Content.Load<Texture2D>("RPG Graphics Icons by 7Soul's"));
            tilesets.Add("Inca_front_by_Kronbits_extended", Content.Load<Texture2D>("Inca_front_by_Kronbits-extended"));
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
            GraphicsDevice.Clear(project.BackgroundColor);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(tilesets["Inca_front_by_Kronbits_extended"], new Rectangle(0, 0, tilesets["Inca_front_by_Kronbits_extended"].Width, tilesets["Inca_front_by_Kronbits_extended"].Height), Color.White);
            _spriteBatch.DrawString(font, project.Definitions.Layers.Count.ToString(), new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
