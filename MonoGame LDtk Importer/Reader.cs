using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame_LDtk_Importer;


namespace Importer
{
    /// <summary>
    /// A class to read a .xnb file into a <see cref="LDtkProject"/>
    /// </summary>
    public class Reader : ContentTypeReader<LDtkProject>
    {
        /// <summary>
        /// Read an .xnb file and load it into a <see cref="LDtkProject"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="existingInstance"></param>
        /// <returns>A <see cref="LDtkProject"/> containing the values of a .xnb file</returns>
        protected override LDtkProject Read(ContentReader input, LDtkProject existingInstance)
        {
            LDtkProject project = new LDtkProject();

            project.BackgroundColor = input.ReadColor();
            project.WorldGridHeight = input.ReadInt32();
            project.WorldGridWidth = input.ReadInt32();
            project.WorldLayout = (WorldLayoutTypes)input.ReadInt32();

            #region definitions

            project.Definitions = new Definitions();
            //entities
            project.Definitions.Entities = new List<EntitieDef>();
            int entitieDefsCount = input.ReadInt32();
            for (int i = 0; i < entitieDefsCount; i++)
            {
                EntitieDef entitie = new EntitieDef();
                entitie.Color = input.ReadColor();
                entitie.Height = input.ReadInt32();
                entitie.Width = input.ReadInt32();
                entitie.Identifier = input.ReadString();
                entitie.PivotCoordinates = input.ReadVector2();
                if (input.ReadBoolean()) entitie.TileId = input.ReadInt32();
                if (input.ReadBoolean()) entitie.TilesetId = input.ReadInt32();
                entitie.Uid = input.ReadInt32();

                project.Definitions.Entities.Add(entitie);
            }

            //enums
            project.Definitions.Enums = new List<EnumDef>();
            int enumDefsCount = input.ReadInt32();
            for (int i = 0; i < enumDefsCount; i++)
            {
                EnumDef enumDef = new EnumDef();
                enumDef.ExternalRelPath = input.ReadString();
                if (input.ReadBoolean()) enumDef.IconTilesetUid = input.ReadInt32();
                enumDef.Identifier = input.ReadString();
                enumDef.Uid = input.ReadInt32();

                enumDef.Values = new List<EnumValueDef>();
                int enumDefValueNb = input.ReadInt32();
                for (int j = 0; j < enumDefValueNb; j++)
                {
                    EnumValueDef value = new EnumValueDef();
                    value.TileSourceRectangle = new Rectangle(
                        input.ReadInt32(),
                        input.ReadInt32(),
                        input.ReadInt32(),
                        input.ReadInt32()
                        );

                    value.Color = input.ReadColor();
                    value.Id = input.ReadString();
                    if (input.ReadBoolean()) value.TileId = input.ReadInt32();
                    enumDef.Values.Add(value);
                }
                project.Definitions.Enums.Add(enumDef);
            }

            //externals enums
            project.Definitions.ExternalEnums = new List<EnumDef>();
            int externalEnumDefsCount = input.ReadInt32();
            for (int i = 0; i < externalEnumDefsCount; i++)
            {
                EnumDef enumDef = new EnumDef();
                enumDef.ExternalRelPath = input.ReadString();
                if (input.ReadBoolean()) enumDef.IconTilesetUid = input.ReadInt32();
                enumDef.Identifier = input.ReadString();
                enumDef.Uid = input.ReadInt32();

                enumDef.Values = new List<EnumValueDef>();
                int enumDefValueNb = input.ReadInt32();
                for (int j = 0; j < enumDefValueNb; j++)
                {
                    EnumValueDef value = new EnumValueDef();
                    value.TileSourceRectangle = new Rectangle(
                        input.ReadInt32(),
                        input.ReadInt32(),
                        input.ReadInt32(),
                        input.ReadInt32()
                        );

                    value.Color = input.ReadColor();
                    value.Id = input.ReadString();
                    if (input.ReadBoolean()) value.TileId = input.ReadInt32();
                    enumDef.Values.Add(value);
                }
                project.Definitions.Enums.Add(enumDef);
            }

            //layers
            project.Definitions.Layers = new List<LayerDef>();
            int layerDefsCount = input.ReadInt32();
            for (int i = 0; i < layerDefsCount; i++)
            {
                LayerDef layer = new LayerDef();

                layer.Type = (LayerType)input.ReadInt32();
                if (input.ReadBoolean()) layer.AutoSourceLayerDefUid = input.ReadInt32();
                if (input.ReadBoolean()) layer.AutoTilesetDefUid = input.ReadInt32();
                layer.DisplayOpacity = input.ReadSingle();
                layer.GridSize = input.ReadInt32();
                layer.Identifier = input.ReadString();
                layer.Offset = input.ReadVector2();
                if (input.ReadBoolean()) layer.TilesetDefUid = input.ReadInt32();
                layer.Uid = input.ReadInt32();

                layer.IntGridValues = new List<IntGridValueDef>();
                int intGridValuesCount = input.ReadInt32();
                for (int j = 0; j < intGridValuesCount; j++)
                {
                    IntGridValueDef intGridValue = new IntGridValueDef();
                    intGridValue.Color = input.ReadColor();
                    intGridValue.Identifier = input.ReadString();
                    intGridValue.Value = input.ReadInt32();
                    layer.IntGridValues.Add(intGridValue);
                }
                project.Definitions.Layers.Add(layer);
            }
            //tilesets
            project.Definitions.Tilesets = new List<TilesetDef>();
            int tilesetCount = input.ReadInt32();
            for (int i = 0; i < tilesetCount; i++)
            {
                TilesetDef tileset = new TilesetDef();

                tileset.GridHeight = input.ReadInt32();
                tileset.GridWidth = input.ReadInt32();

                int metadataCount = input.ReadInt32();
                tileset.CustomData = new List<TileMetadata>();
                for (int j = 0; j < metadataCount; j++)
                {
                    TileMetadata metadata = new TileMetadata();
                    metadata.Data = input.ReadString();
                    metadata.TileId = input.ReadInt32();
                    tileset.CustomData.Add(metadata);
                }

                int tagCount = input.ReadInt32();
                tileset.EnumTags = new List<TilesetTag>();
                for (int j = 0; j < tagCount; j++)
                {
                    TilesetTag tag = new TilesetTag();

                    int tileIdsCount = input.ReadInt32();
                    tag.TileIds = new List<int>();
                    for (int k = 0; k < tileIdsCount; k++)
                    {
                        tag.TileIds.Add(input.ReadInt32());
                    }

                    tag.EnumValueId = input.ReadString();
                }

                tileset.Identifier = input.ReadString();
                tileset.Padding = input.ReadInt32();
                tileset.Height = input.ReadInt32();
                tileset.Width = input.ReadInt32();
                tileset.RelPath = input.ReadString();
                tileset.Spacing = input.ReadInt32();
                tileset.TileGridSize = input.ReadInt32();
                tileset.Uid = input.ReadInt32();

                project.Definitions.Tilesets.Add(tileset);
            }

            #endregion

            #region levels
            project.Levels = new List<Level>();
            int levelCount = input.ReadInt32();
            for (int i = 0; i < levelCount; i++)
            {
                Level level = new Level();
                level.BackgroundColor = input.ReadColor();
                level.BackgroundRelPath = input.ReadString();
                level.Identifier = input.ReadString();
                level.Height = input.ReadInt32();
                level.Width = input.ReadInt32();
                level.Uid = input.ReadInt32();
                level.WorldCoordinates = input.ReadVector2();

                //background position fields
                if (input.ReadBoolean())
                {
                    level.BackgroundPosition = new BackgroundPosition(
                        new Rectangle(
                            input.ReadInt32(),
                            input.ReadInt32(),
                            input.ReadInt32(),
                            input.ReadInt32()
                            ),
                        input.ReadVector2(),
                        input.ReadVector2()
                        );
                }

                //neighbours
                level.Neighbours = new List<LevelNeighbour>();
                int neighboursCount = input.ReadInt32();
                for (int j = 0; j < neighboursCount; j++)
                {
                    LevelNeighbour neighbour = new LevelNeighbour();
                    neighbour.Direction = (NeighbourDirection)input.ReadInt32();
                    neighbour.LevelUid = input.ReadInt32();
                    level.Neighbours.Add(neighbour);
                }

                //field instances
                level.FieldInstances = new List<Field>();
                int fieldInstancesCount = input.ReadInt32();
                for (int j = 0; j < fieldInstancesCount; j++)
                {
                    Field field = new Field();

                    string identifier = input.ReadString();
                    FieldType type = (FieldType)input.ReadInt32();

                    if (type == FieldType.Int)
                    {
                        field = new IntField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }
                    else if (type == FieldType.Float)
                    {
                        field = new FloatField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }
                    else if (type == FieldType.Bool)
                    {
                        field = new BoolField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }
                    else if (type == FieldType.Point)
                    {
                        field = new PointField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }
                    else if (type == FieldType.Color)
                    {
                        field = new ColorField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }
                    else
                    {
                        field = new StringField();
                        field.Type = type;
                        field.Identifier = identifier;
                    }

                    field.DefUid = input.ReadInt32();
                    if (input.ReadBoolean()) field.EnumName = input.ReadString();
                    field.IsArray = input.ReadBoolean();
                    level.FieldInstances.Add(field);

                    if (field.Type == FieldType.Int)
                    {
                        IntField intField = field as IntField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            intField.Value = new List<int>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                intField.Value.Add(input.ReadInt32());
                            }
                        }
                    }
                    else if (field.Type == FieldType.Float)
                    {
                        FloatField floatField = field as FloatField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            floatField.Value = new List<float>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                floatField.Value.Add(input.ReadSingle());
                            }
                        }
                    }
                    else if (field.Type == FieldType.Bool)
                    {
                        BoolField boolField = field as BoolField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            boolField.Value = new List<bool>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                boolField.Value.Add(input.ReadBoolean());
                            }
                        }
                    }
                    else if (field.Type == FieldType.Color)
                    {
                        ColorField colorField = field as ColorField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            colorField.Value = new List<Color>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                colorField.Value.Add(input.ReadColor());
                            }
                        }
                    }
                    else if (field.Type == FieldType.Point)
                    {
                        PointField pointField = field as PointField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            pointField.Value = new List<Point>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                pointField.Value.Add(input.ReadVector2().ToPoint());
                            }
                        }
                    }
                    else
                    {
                        StringField stringField = field as StringField;
                        if (input.ReadBoolean())
                        {
                            int valueCount = input.ReadInt32();
                            stringField.Value = new List<string>();
                            for (int k = 0; k < valueCount; k++)
                            {
                                stringField.Value.Add(input.ReadString());
                            }
                        }
                    }
                }

                //layers
                level.LayerInstances = new List<Layer>();
                int layersCount = input.ReadInt32();
                for (int j = 0; j < layersCount; j++)
                {
                    Layer layer = new Layer();
                    LayerType type = (LayerType)input.ReadInt32();

                    if (type == LayerType.AutoLayer)
                    {
                        layer = new AutoLayer();
                    }
                    else if (type == LayerType.Entities)
                    {
                        layer = new EntitieLayer();
                    }
                    else if (type == LayerType.IntGrid)
                    {
                        layer = new IntGridLayer();
                    }
                    else
                    {
                        layer = new TileLayer();
                    }

                    layer.Type = type;

                    layer.Height = input.ReadInt32();
                    layer.Width = input.ReadInt32();
                    layer.GridSize = input.ReadInt32();
                    layer.Identifier = input.ReadString();
                    layer.Opacity = input.ReadSingle();
                    layer.TotalOffset = input.ReadVector2();

                    layer.LayerDefUid = input.ReadInt32();
                    layer.LevelId = input.ReadInt32();

                    layer.Offset = input.ReadVector2();
                    layer.IsVisible = input.ReadBoolean();

                    if (layer.Type == LayerType.AutoLayer)
                    {
                        AutoLayer autoLayer = layer as AutoLayer;

                        if (input.ReadBoolean())
                        {
                            autoLayer.TilesetDefUid = input.ReadInt32();
                        }
                        if (input.ReadInt32() > 0)
                        {
                            autoLayer.TilesetRelPath = input.ReadString();
                        }
                        int tileCount = input.ReadInt32();
                        for (int k = 0; k < tileCount; k++)
                        {
                            Tile tile = new Tile();
                            tile.IsFlippedOnX = input.ReadBoolean();
                            tile.IsFlippedOnY = input.ReadBoolean();
                            tile.Coordinates = input.ReadVector2();
                            tile.Source = input.ReadVector2();
                            tile.TileId = input.ReadInt32();

                            autoLayer.AutoLayerTiles.Add(tile);
                        }

                        layer = autoLayer;
                    }
                    else if (layer.Type == LayerType.Entities)
                    {
                        EntitieLayer entitieLayer = layer as EntitieLayer;
                        entitieLayer.EntityInstances = new List<Entity>();
                        int entitieCount = input.ReadInt32();
                        for (int k = 0; k < entitieCount; k++)
                        {
                            Entity entity = new Entity();
                            entity.GridCoordinates = input.ReadVector2();
                            entity.Identifier = input.ReadString();
                            entity.PivotCoordinates = input.ReadVector2();
                            entity.DefUid = input.ReadInt32();
                            entity.Height = input.ReadInt32();
                            entity.Width = input.ReadInt32();
                            entity.Coordinates = input.ReadVector2();

                            //entity tile
                            if (input.ReadBoolean())
                            {
                                EntityTile tile = new EntityTile();
                                tile.SourceRectangle = new Rectangle(
                                    input.ReadInt32(),
                                    input.ReadInt32(),
                                    input.ReadInt32(),
                                    input.ReadInt32()
                                    );
                                tile.TilesetUid = input.ReadInt32();
                                entity.Tile = tile;
                            }

                            //field instances
                            entity.FieldInstances = new List<Field>();
                            int fieldInstancesCount2 = input.ReadInt32();
                            for (int l = 0; l < fieldInstancesCount2; l++)
                            {
                                Field field = new Field();
                                field.Identifier = input.ReadString();
                                field.Type = (FieldType)input.ReadInt32();
                                field.DefUid = input.ReadInt32();
                                field.EnumName = input.ReadString();
                                field.IsArray = input.ReadBoolean();

                                if (field.Type == FieldType.Int)
                                {
                                    IntField intField = field as IntField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        intField.Value = new List<int>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            intField.Value.Add(input.ReadInt32());
                                        }
                                    }
                                }
                                else if (field.Type == FieldType.Float)
                                {
                                    FloatField floatField = field as FloatField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        floatField.Value = new List<float>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            floatField.Value.Add(input.ReadSingle());
                                        }
                                    }
                                }
                                else if (field.Type == FieldType.Bool)
                                {
                                    BoolField boolField = field as BoolField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        boolField.Value = new List<bool>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            boolField.Value.Add(input.ReadBoolean());
                                        }
                                    }
                                }
                                else if (field.Type == FieldType.Color)
                                {
                                    ColorField colorField = field as ColorField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        colorField.Value = new List<Color>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            colorField.Value.Add(input.ReadColor());
                                        }
                                    }
                                }
                                else if (field.Type == FieldType.Point)
                                {
                                    PointField pointField = field as PointField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        pointField.Value = new List<Point>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            pointField.Value.Add(input.ReadVector2().ToPoint());
                                        }
                                    }
                                }
                                else
                                {
                                    StringField stringField = field as StringField;
                                    if (input.ReadBoolean())
                                    {
                                        int valueCount = input.ReadInt32();
                                        stringField.Value = new List<string>();
                                        for (int m = 0; m < valueCount; m++)
                                        {
                                            stringField.Value.Add(input.ReadString());
                                        }
                                    }
                                }

                                entity.FieldInstances.Add(field);
                            }

                            entitieLayer.EntityInstances.Add(entity);
                        }

                        layer = entitieLayer;
                    }
                    else if (layer.Type == LayerType.IntGrid)
                    {
                        IntGridLayer intGridLayer = layer as IntGridLayer;
                        int valueCount = input.ReadInt32();
                        intGridLayer.IntGridCsv = new List<int>();
                        for (int k = 0; k < valueCount; k++)
                        {
                            intGridLayer.IntGridCsv.Add(input.ReadInt32());
                        }
                        layer = intGridLayer;
                    }
                    else
                    {
                        TileLayer tileLayer = layer as TileLayer;
                        int tileCount = new int();
                        tileLayer.GridTilesInstances = new List<Tile>();
                        for (int k = 0; k < tileCount; k++)
                        {
                            Tile tile = new Tile();
                            tile.IsFlippedOnX = input.ReadBoolean();
                            tile.IsFlippedOnY = input.ReadBoolean();
                            tile.Coordinates = input.ReadVector2();
                            tile.Source = input.ReadVector2();
                            tile.TileId = input.ReadInt32();
                            tileLayer.GridTilesInstances.Add(tile);
                        }

                        if (input.ReadBoolean())
                        {
                            tileLayer.TilesetDefUid = input.ReadInt32();
                        }

                        tileLayer.TilesetRelPath = input.ReadString();

                        if (input.ReadBoolean())
                        {
                            tileLayer.OverrideTilesetUid = input.ReadInt32();
                        }

                        layer = tileLayer;
                    }

                    level.LayerInstances.Add(layer);
                }

                project.Levels.Add(level);
            }

            #endregion

            return project;
        }
    }
}
