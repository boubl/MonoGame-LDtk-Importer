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

        private LDtkProject project;
        private LDtkProject project2;
        private LDtkProject project3;
        private SpriteFont font;

        Level level;

        Texture2D croppedBg;
        List<Layer> autoLayers;
        List<Field> fields;

        AutoLayer auto;
        List<Point> pointList;
        Tile tile;
        List<int> tileIdsList;
        Dictionary<int, Texture2D> txDico;
        Texture2D autoTsTx;

        IntGridLayer ig;
        List<Point> pointList2;
        int igValue;

        EntitieLayer el;
        List<Entity> entityList;

        TileLayer tl;
        List<Point> tilePointList;
        Tile tileLayerTile;
        List<int> usedTileIds;
        Dictionary<int, Texture2D> newDico;
        Texture2D tileTsTx;

        Entity entity;
        List<Field> entityFields;

        EntitieDef entitieDef;
        EnumDef enumdef;
        LayerDef layerdef;
        TilesetDef tileset;

        EnumValueDef enumValueDef;

        IntGridValueDef intGridValueDef;


        List<string> enumValueIds;
        TileMetadata tileMetadata;
        TilesetTag tilesetTag;
        Texture2D tilesetTx;
        List<Texture2D> tiles;
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
            project = new LDtkProject(@"Content\Test_file_for_API_showing_all_features.ldtk");
            project2 = new LDtkProject(@"Content\FieldsTypes.ldtk");
            project3 = new LDtkProject(@"Content\SeparateLevelFiles.ldtk");
            font = Content.Load<SpriteFont>("font");

            //project
            level = project.GetLevelByIdentifier("Level_0");

            //level
            croppedBg = project.GetLevelByIdentifier("Level_3").GetCroppedBackground(GraphicsDevice);
            autoLayers = level.GetLayersByType(LayerType.AutoLayer);
            fields = level.GetFieldsByType(FieldType.String);

            #region layers
            //autolayer
            auto = autoLayers[0] as AutoLayer;
            pointList = auto.GetPointsByTileId(180);
            tile = auto.GetTileAt(new Point(304, 112));
            tileIdsList = auto.GetUsedTileIds();
            txDico = auto.GetUsedTilesTextures(GraphicsDevice);
            autoTsTx = auto.GetTilesetTexture(GraphicsDevice);

            //intgridlayer
            ig = level.GetLayersByType(LayerType.IntGrid)[0] as IntGridLayer;
            pointList2 = ig.GetPointsByValue(0);
            igValue = ig.GetValueAt(new Point(70 * 8, 14 * 8));

            //entitylayer
            el = level.GetLayersByType(LayerType.Entities)[0] as EntitieLayer;
            entityList = el.GetEntitiesByIdentifier("EntityFieldsTest");

            //tileLayer
            tl = level.GetLayersByType(LayerType.Tiles)[0] as TileLayer;
            tilePointList = tl.GetPointsByTileId(241); //fix this
            tileLayerTile = tl.GetTileAt(new Point(432, 112));
            usedTileIds = tl.GetUsedTileIds();
            newDico = tl.GetUsedTilesTextures(GraphicsDevice);
            tileTsTx = tl.GetTilesetTexture(GraphicsDevice);
            #endregion

            //entity
            entity = entityList[0];
            entityFields = entity.GetFieldsByType(FieldType.String);

            //definitions
            entitieDef = project.Definitions.GetEntitieDefById("EntityFieldsTest");
            enumdef = project.Definitions.GetEnumDefById("SomeEnum");
            //project.Definitions.GetExtEnumDefById("There is no ext enum in this file anyway so...");
            layerdef = project.Definitions.GetLayerDefById("IntGrid_classic");
            tileset = project.Definitions.GetTilesetDefById("Inca_front_by_Kronbits_extended");

            //enumdef
            enumValueDef = enumdef.GetValueByType("A");

            //layerdef
            intGridValueDef = layerdef.GetIntGridValueById("first");

            //tilesetdef
            enumValueIds = tileset.GetEnumValueIdsByTileId(0);
            tileMetadata = tileset.GetTileMetadataByTileId(0);
            tilesetTag = tileset.GetTilesetTagByEnumValueId("A");
            tilesetTx = tileset.GetTilesetTexture(GraphicsDevice);
            tiles = tileset.GetTilesTextures(GraphicsDevice);


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
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            //project
            Level level = project.GetLevelByIdentifier("Level_0");
            
            //level
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                _spriteBatch.Draw(croppedBg, new Vector2(0, 0), Color.White);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _spriteBatch.Draw(autoTsTx, new Vector2(0, 0), Color.White);

                int i = 0;
                foreach (Texture2D tx in txDico.Values)
                {
                    _spriteBatch.Draw(tx, new Vector2(16 * i, 244), Color.White);
                    i++;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                _spriteBatch.Draw(tileTsTx, new Vector2(0, 0), Color.White);

                int i = 0;
                foreach (Texture2D tx in newDico.Values)
                {
                    _spriteBatch.Draw(tx, new Rectangle(16 * i, 244, 16, 16), Color.White);
                    i++;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _spriteBatch.Draw(tilesetTx, new Vector2(0, 0), Color.White);

                int i = 0;
                foreach(Texture2D texture in tiles)
                {
                    _spriteBatch.Draw(texture, new Vector2(i * 16, 244), Color.White);
                    i++;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
