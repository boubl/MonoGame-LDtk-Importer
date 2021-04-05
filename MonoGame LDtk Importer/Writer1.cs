using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MonoGame_LDtk_Importer
{
    [ContentTypeWriter]
    class Writer1 : ContentTypeWriter<LDtkProject>
    {
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
                output.Write(!String.IsNullOrEmpty(@enum.ExternalRelPath)); //bool
                if (!String.IsNullOrEmpty(@enum.ExternalRelPath)) output.Write(@enum.ExternalRelPath); //string

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

                    output.Write(valueDef.Id); //string

                    output.Write(valueDef.TileId.HasValue); //bool
                    if (valueDef.TileId.HasValue) output.Write(valueDef.TileId.Value); //int
                }
            }

            //external enums
            output.Write(value.Definitions.ExternalEnums.Count); //int
            foreach (EnumDef @enum in value.Definitions.ExternalEnums)
            {
                output.Write(!String.IsNullOrEmpty(@enum.ExternalRelPath)); //bool
                if (!String.IsNullOrEmpty(@enum.ExternalRelPath)) output.Write(@enum.ExternalRelPath); //string

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
                    output.Write(!String.IsNullOrEmpty(intGridValue.Identifier)); //bool
                    if (!String.IsNullOrEmpty(intGridValue.Identifier)) output.Write(intGridValue.Identifier); //string
                    output.Write(intGridValue.Value); //int
                }

            }

            //tilesets
            output.Write(value.Definitions.Tilesets.Count);
            foreach(Tileset tileset in value.Definitions.Tilesets)
            {
                output.Write(tileset.Identifier); //string
                output.Write(tileset.Padding); //int
                output.Write(tileset.Height); //int
                output.Write(tileset.Width); //int
                output.Write(tileset.RelPath); //string
                output.Write(tileset.Spacing); //int
                output.Write(tileset.TileGridSize); //int
                output.Write(tileset.Uid); //int
            }

            #endregion

            #region levels

            output.Write(value.Levels.Count); //int, used for loop
            foreach (Level level in value.Levels)
            {
                output.Write(level.BackgroundColor); //color
                output.Write(!String.IsNullOrEmpty(level.BackgroundRelPath)); //bool
                if (!String.IsNullOrEmpty(level.BackgroundRelPath)) output.Write(level.BackgroundRelPath); //string
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
                foreach (FieldInstance field in level.FieldInstances)
                {
                    output.Write(field.Identifier); //string
                    output.Write((int)field.Type); //int to enum
                    output.Write(!String.IsNullOrEmpty(field.Value)); //bool
                    if (!String.IsNullOrEmpty(field.Value)) output.Write(field.Value); //string
                    output.Write(field.DefUid); //int
                    output.Write(!String.IsNullOrEmpty(field.EnumName)); //bool
                    if (!String.IsNullOrEmpty(field.EnumName)) output.Write(field.EnumName); //string
                    output.Write(field.IsArray); //bool
                }

                //layers
                output.Write(level.LayerInstances.Count); //int
                foreach (LayerInstance layer in level.LayerInstances)
                {
                    output.Write(layer.Height); //int
                    output.Write(layer.Width); //int
                    output.Write(layer.GridSize); //int
                    output.Write(layer.Identifier); //string
                    output.Write(layer.Opacity); //float

                    output.Write(layer.TotalOffset); //Vector2

                    output.Write(layer.TilesetDefUid.HasValue); //bool
                    if (layer.TilesetDefUid.HasValue) output.Write(layer.TilesetDefUid.Value); //int

                    output.Write(!String.IsNullOrEmpty(layer.TilesetRelPath)); //bool
                    if (!String.IsNullOrEmpty(layer.TilesetRelPath)) output.Write(layer.TilesetRelPath); //string

                    output.Write((int)layer.Type); //int to enum
                    output.Write(layer.LayerDefUid); //int
                    output.Write(layer.LevelId); //int

                    output.Write(layer.OverrideTilesetUid.HasValue); //bool
                    if (layer.OverrideTilesetUid.HasValue) output.Write(layer.OverrideTilesetUid.Value); //int

                    output.Write(layer.Offset); //Vector2

                    output.Write(layer.IsVisible); //bool

                    // auto layer tiles
                    output.Write(layer.AutoLayerTiles.Count); //int
                    foreach(TileInstance tile in layer.AutoLayerTiles)
                    {
                        output.Write(tile.IsFlippedOnX); //bool
                        output.Write(tile.IsFlippedOnY); //bool

                        output.Write(tile.Coordinates); //Vector2

                        output.Write(tile.Source); //Vector2

                        output.Write(tile.TileId); //int
                    }

                    //entities
                    output.Write(layer.EntityInstances.Count); //int
                    foreach (EntityInstance entity in layer.EntityInstances)
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
                        foreach (FieldInstance field in level.FieldInstances)
                        {
                            output.Write(field.Identifier); //string
                            output.Write((int)field.Type); //int to enum
                            output.Write(!String.IsNullOrEmpty(field.Value)); //bool
                            if (!String.IsNullOrEmpty(field.Value)) output.Write(field.Value); //string
                            output.Write(field.DefUid); //int
                            output.Write(!String.IsNullOrEmpty(field.EnumName)); //bool
                            if (!String.IsNullOrEmpty(field.EnumName)) output.Write(field.EnumName); //string
                            output.Write(field.IsArray); //bool
                        }
                    }

                    //grid tiles
                    output.Write(layer.GridTilesInstances.Count); //int
                    foreach (TileInstance tile in layer.GridTilesInstances)
                    {
                        output.Write(tile.IsFlippedOnX); //bool
                        output.Write(tile.IsFlippedOnY); //bool

                        output.Write(tile.Coordinates); //Vector2

                        output.Write(tile.Source); //Vector2

                        output.Write(tile.TileId); //int
                    }

                    //IntGridCsv
                    output.Write(layer.IntGridCsv.Length); //int
                    foreach (int i in layer.IntGridCsv)
                    {
                        output.Write(i); //int
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
            return typeof(Reader1).AssemblyQualifiedName;
        }
    }
}
