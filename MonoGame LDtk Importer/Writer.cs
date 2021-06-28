using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using MonoGame_LDtk_Importer;
using Microsoft.Xna.Framework;

namespace Importer
{
    /// <summary>
    /// A class to write a <see cref="LDtkProject"/> into a .xnb file
    /// </summary>
    [ContentTypeWriter]
    class Writer : ContentTypeWriter<LDtkProject>
    {
        /// <summary>
        /// Read a <see cref="LDtkProject"/> and write it into a .xnb file
        /// </summary>
        /// <param name="output"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override void Write(ContentWriter output, LDtkProject value)
        {
            output.Write(value.BackgroundColor); //string
            output.Write(value.WorldGridHeight); //int
            output.Write(value.WorldGridWidth); //int
            output.Write((int)value.WorldLayout); //int to cast into enum

            #region definitions

            //entities
            output.Write(value.Definitions.Entities.Count); //int
            foreach (EntitieDef entitie in value.Definitions.Entities)
            {
                output.Write(entitie.Color); //string
                output.Write(entitie.Height); //int
                output.Write(entitie.Width); //int
                output.Write(entitie.Identifier); //string
                output.Write(entitie.PivotCoordinates); //Vector2

                output.Write(entitie.TileId.HasValue); //bool
                if (entitie.TileId.HasValue) output.Write(entitie.TileId.Value);

                output.Write(entitie.TilesetId.HasValue); //bool
                if (entitie.TilesetId.HasValue) output.Write(entitie.TilesetId.Value);

                output.Write(entitie.Uid); //int
            }

            //enums
            output.Write(value.Definitions.Enums.Count); //int
            foreach (EnumDef @enum in value.Definitions.Enums)
            {
                if (!String.IsNullOrEmpty(@enum.ExternalRelPath)) output.Write(@enum.ExternalRelPath); //string
                else output.Write("");

                output.Write(@enum.IconTilesetUid.HasValue); //bool
                if (@enum.IconTilesetUid.HasValue) output.Write(@enum.IconTilesetUid.Value); //int

                output.Write(@enum.Identifier); //string
                output.Write(@enum.Uid); //int

                output.Write(@enum.Values.Count); //int
                foreach(EnumValueDef valueDef in @enum.Values)
                {
                    output.Write(valueDef.TileSourceRectangle.X); //int
                    output.Write(valueDef.TileSourceRectangle.Y); //int
                    output.Write(valueDef.TileSourceRectangle.Width); //int
                    output.Write(valueDef.TileSourceRectangle.Height); //int

                    output.Write(valueDef.Color); //color
                    output.Write(valueDef.Id); //string

                    output.Write(valueDef.TileId.HasValue); //bool
                    if (valueDef.TileId.HasValue) output.Write(valueDef.TileId.Value); //int
                }
            }

            //external enums
            output.Write(value.Definitions.ExternalEnums.Count); //int
            foreach (EnumDef @enum in value.Definitions.ExternalEnums)
            {
                if (!String.IsNullOrEmpty(@enum.ExternalRelPath)) output.Write(@enum.ExternalRelPath); //string
                else output.Write("");

                output.Write(@enum.IconTilesetUid.HasValue); //bool
                if (@enum.IconTilesetUid.HasValue) output.Write(@enum.IconTilesetUid.Value); //int

                output.Write(@enum.Identifier); //string
                output.Write(@enum.Uid); //int

                output.Write(@enum.Values.Count); //int
                foreach (EnumValueDef valueDef in @enum.Values)
                {
                    output.Write(valueDef.TileSourceRectangle.X); //int
                    output.Write(valueDef.TileSourceRectangle.Y); //int
                    output.Write(valueDef.TileSourceRectangle.Width); //int
                    output.Write(valueDef.TileSourceRectangle.Height); //int

                    output.Write(valueDef.Color); //color
                    output.Write(valueDef.Id); //string

                    output.Write(valueDef.TileId.HasValue); //bool
                    if (valueDef.TileId.HasValue) output.Write(valueDef.TileId.Value); //int
                }
            }

            //layers
            output.Write(value.Definitions.Layers.Count); //int
            foreach(LayerDef layer in value.Definitions.Layers)
            {
                output.Write((int)layer.Type); //int to enum

                output.Write(layer.AutoSourceLayerDefUid.HasValue); //bool
                if (layer.AutoSourceLayerDefUid.HasValue) output.Write(layer.AutoSourceLayerDefUid.Value); //int

                output.Write(layer.AutoTilesetDefUid.HasValue); //bool
                if (layer.AutoTilesetDefUid.HasValue) output.Write(layer.AutoTilesetDefUid.Value); //int

                output.Write(layer.DisplayOpacity); //float
                output.Write(layer.GridSize); //int
                output.Write(layer.Identifier); //string
                output.Write(layer.Offset); //vector2

                output.Write(layer.TilesetDefUid.HasValue); //bool
                if (layer.TilesetDefUid.HasValue) output.Write(layer.TilesetDefUid.Value); //int

                output.Write(layer.Uid); //int

                //intgrid values defs
                output.Write(layer.IntGridValues.Count); //int
                foreach(IntGridValueDef intGridValue in layer.IntGridValues)
                {
                    output.Write(intGridValue.Color); //color
                    if (!String.IsNullOrEmpty(intGridValue.Identifier)) output.Write(intGridValue.Identifier); //string
                    else output.Write("");
                    output.Write(intGridValue.Value); //int
                }

            }
            //tilesets
            output.Write(value.Definitions.Tilesets.Count);
            foreach(TilesetDef tileset in value.Definitions.Tilesets)
            {
                output.Write(tileset.GridHeight); //int
                output.Write(tileset.GridWidth); //int

                output.Write(tileset.CustomData.Count); //int
                foreach(TileMetadata metadata in tileset.CustomData)
                {
                    output.Write(metadata.Data); //string
                    output.Write(metadata.TileId); //int
                }

                output.Write(tileset.EnumTags.Count); //int
                foreach (TilesetTag tag in tileset.EnumTags)
                {
                    output.Write(tag.TileIds.Count); //int
                    foreach (int i in tag.TileIds)
                    {
                        output.Write(i); //int
                    }
                    output.Write(tag.EnumValueId); //string
                }

                output.Write(tileset.Identifier); //string
                output.Write(tileset.Padding); //int
                output.Write(tileset.Height); //int
                output.Write(tileset.Width); //int
                output.Write(tileset.RelPath); //string
                output.Write(tileset.Spacing); //int

                output.Write(tileset.TagSourceEnumUid.HasValue);
                if(tileset.TagSourceEnumUid.HasValue)
                {
                    output.Write(tileset.TagSourceEnumUid.Value); //int
                }

                output.Write(tileset.TileGridSize); //int
                output.Write(tileset.Uid); //int
            }

            #endregion

            #region levels

            output.Write(value.Levels.Count); //int, used for loop
            foreach (Level level in value.Levels)
            {
                output.Write(level.BackgroundColor); //color
                if (!String.IsNullOrEmpty(level.BackgroundRelPath)) output.Write(level.BackgroundRelPath); //string
                else output.Write("");
                output.Write(level.Identifier); //string
                output.Write(level.Height); //int
                output.Write(level.Width); //int
                output.Write(level.Uid); //int
                output.Write(level.WorldCoordinates); //Vector2

                //background position fields
                output.Write(level.BackgroundPosition.HasValue); //indiacte if the BgPos is null or not
                if (level.BackgroundPosition.HasValue)
                {
                    output.Write(level.BackgroundPosition.Value.CropRectangle.X); //int
                    output.Write(level.BackgroundPosition.Value.CropRectangle.Y); //int
                    output.Write(level.BackgroundPosition.Value.CropRectangle.Height); //int
                    output.Write(level.BackgroundPosition.Value.CropRectangle.Width); //int

                    output.Write(level.BackgroundPosition.Value.Scale); //Vector2

                    output.Write(level.BackgroundPosition.Value.Coordinates); //Vector2
                }

                //neighbours
                output.Write(level.Neighbours.Count); //int
                foreach (LevelNeighbour neighbour in level.Neighbours)
                {
                    output.Write((int)neighbour.Direction); //int to enum
                    output.Write(neighbour.LevelUid); //int
                }

                //field instances
                output.Write(level.FieldInstances.Count); //int
                foreach (Field field in level.FieldInstances)
                {
                    output.Write(field.Identifier); //string
                    output.Write((int)field.Type); //int to enum
                    output.Write(field.DefUid); //int
                    if (!String.IsNullOrEmpty(field.EnumName)) output.Write(field.EnumName); //string
                    else output.Write("");
                    output.Write(field.IsArray); //bool

                    if (field.Type == FieldType.Int)
                    {
                        IntField intField = field as IntField;
                        output.Write(intField.Value.Count);
                        foreach(int i in intField.Value)
                        {
                            output.Write(i);
                        }
                    }
                    else if (field.Type == FieldType.Float)
                    {
                        FloatField floatField = field as FloatField;
                        output.Write(floatField.Value.Count);
                        foreach (float f in floatField.Value)
                        {
                            output.Write(f);
                        }
                    }
                    else if (field.Type == FieldType.Bool)
                    {
                        BoolField boolField = field as BoolField;
                        output.Write(boolField.Value.Count);
                        foreach (bool b in boolField.Value)
                        {
                            output.Write(b);
                        }
                    }
                    else if (field.Type == FieldType.Color)
                    {
                        ColorField colorField = field as ColorField;
                        output.Write(colorField.Value.Count);
                        foreach (Color c in colorField.Value)
                        {
                            output.Write(c);
                        }
                    }
                    else if (field.Type == FieldType.Point)
                    {
                        PointField pointField = field as PointField;
                        output.Write(pointField.Value.Count);
                        foreach (Point p in pointField.Value)
                        {
                            output.Write(p.ToVector2());
                        }
                    }
                    else
                    {
                        StringField stringField = field as StringField;
                        output.Write(stringField.Value.Count);
                        foreach (string s in stringField.Value)
                        {
                            output.Write(s);
                        }
                    }
                }

                //layers
                output.Write(level.LayerInstances.Count); //int
                foreach (Layer layer in level.LayerInstances)
                {
                    output.Write(layer.Height); //int
                    output.Write(layer.Width); //int
                    output.Write(layer.GridSize); //int
                    output.Write(layer.Identifier); //string
                    output.Write(layer.Opacity); //float

                    output.Write(layer.TotalOffset); //Vector2

                    output.Write((int)layer.Type); //int to enum
                    output.Write(layer.LayerDefUid); //int
                    output.Write(layer.LevelId); //int

                    output.Write(layer.Offset); //Vector2

                    output.Write(layer.IsVisible); //bool

                    

                    if (layer.Type == LayerType.AutoLayer)
                    {
                        AutoLayer autoLayer = layer as AutoLayer;
                        output.Write(autoLayer.TilesetDefUid.HasValue);
                        if(autoLayer.TilesetDefUid.HasValue)
                        {
                            output.Write(autoLayer.TilesetDefUid.Value);
                        }
                        output.Write(autoLayer.TilesetRelPath.Length > 0);
                        if (autoLayer.TilesetRelPath.Length > 0)
                        {
                            output.Write(autoLayer.TilesetRelPath);
                        }
                        output.Write(autoLayer.AutoLayerTiles.Count); //int
                        foreach (Tile tile in autoLayer.AutoLayerTiles)
                        {
                            output.Write(tile.IsFlippedOnX); //bool
                            output.Write(tile.IsFlippedOnY); //bool

                            output.Write(tile.Coordinates); //Vector2

                            output.Write(tile.Source); //Vector2

                            output.Write(tile.TileId); //int
                        }
                    }
                    else if (layer.Type == LayerType.Entities)
                    {
                        EntitieLayer entitieLayer = layer as EntitieLayer;
                        output.Write(entitieLayer.EntityInstances.Count);
                        foreach(Entity entity in entitieLayer.EntityInstances)
                        {
                            output.Write(entity.GridCoordinates); //Vector2
                            output.Write(entity.Identifier); //string
                            output.Write(entity.PivotCoordinates); //Vector2
                            output.Write(entity.DefUid); //int
                            output.Write(entity.Height); //int
                            output.Write(entity.Width); //int
                            output.Write(entity.Coordinates); //Vector2

                            //entity tile
                            output.Write(entity.Tile.HasValue); //bool
                            if (entity.Tile.HasValue)
                            {
                                output.Write(entity.Tile.Value.SourceRectangle.X); //int
                                output.Write(entity.Tile.Value.SourceRectangle.Y); //int
                                output.Write(entity.Tile.Value.SourceRectangle.Height); //int
                                output.Write(entity.Tile.Value.SourceRectangle.Width); //int

                                output.Write(entity.Tile.Value.TilesetUid); //int
                            }

                            //field instances
                            output.Write(level.FieldInstances.Count); //int
                            foreach (Field field in level.FieldInstances)
                            {
                                output.Write(field.Identifier); //string
                                output.Write((int)field.Type); //int to enum
                                output.Write(field.DefUid); //int
                                if (!String.IsNullOrEmpty(field.EnumName)) output.Write(field.EnumName); //string
                                else output.Write("");
                                output.Write(field.IsArray); //bool

                                if (field.Type == FieldType.Int)
                                {
                                    IntField intField = field as IntField;
                                    output.Write(intField.Value.Count);
                                    foreach (int i in intField.Value)
                                    {
                                        output.Write(i);
                                    }
                                }
                                else if (field.Type == FieldType.Float)
                                {
                                    FloatField floatField = field as FloatField;
                                    output.Write(floatField.Value.Count);
                                    foreach (float f in floatField.Value)
                                    {
                                        output.Write(f);
                                    }
                                }
                                else if (field.Type == FieldType.Bool)
                                {
                                    BoolField boolField = field as BoolField;
                                    output.Write(boolField.Value.Count);
                                    foreach (bool b in boolField.Value)
                                    {
                                        output.Write(b);
                                    }
                                }
                                else if (field.Type == FieldType.Color)
                                {
                                    ColorField colorField = field as ColorField;
                                    output.Write(colorField.Value.Count);
                                    foreach (Color c in colorField.Value)
                                    {
                                        output.Write(c);
                                    }
                                }
                                else if (field.Type == FieldType.Point)
                                {
                                    PointField pointField = field as PointField;
                                    output.Write(pointField.Value.Count);
                                    foreach (Point p in pointField.Value)
                                    {
                                        output.Write(p.ToVector2());
                                    }
                                }
                                else
                                {
                                    StringField stringField = field as StringField;
                                    output.Write(stringField.Value.Count);
                                    foreach (string s in stringField.Value)
                                    {
                                        output.Write(s);
                                    }
                                }
                            }
                        }
                    }
                    else if (layer.Type == LayerType.IntGrid)
                    {

                    }
                    else if (layer.Type == LayerType.Tiles)
                    {

                    }
                }
            }

            #endregion

        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(LDtkProject).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(Reader).AssemblyQualifiedName;
        }
    }
}
