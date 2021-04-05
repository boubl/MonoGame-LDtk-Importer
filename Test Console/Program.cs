using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Xna.Framework;
using MonoGame_LDtk_Importer;

namespace Test_Console
{
    public class Program
    {
        static TextWriter logger = new StringWriter();
        static bool LevelsAreExternal;
        static string fileVersion;
        static string spacing;
        static void Main(string[] args)
        {

            JsonElement jsonFile = JsonSerializer.Deserialize<JsonDocument>(File.ReadAllText(@"C:\Users\Titou\source\repos\MonoGame LDtk Importer\App\Content\Test_file_for_API_showing_all_features.ldtk")).RootElement;
            
            if (jsonFile.GetProperty("jsonVersion").GetString() == "")
            {

            }

            LDtkProject project = LoadProject(jsonFile);

            Console.WriteLine("End of line.\n\nPress enter to quit");
            string path = "logs\\" + "log-" + DateTime.Now.ToString().Replace(":", "-").Replace(" ", "--").Replace("/", "-") + ".txt";
            Directory.CreateDirectory("logs");
            File.WriteAllText(path, logger.ToString());
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

        public static void Write(string text)
        {
            logger.Write(text);
            Console.Write(text);
        }
        public static void WriteLine(string text)
        {
            logger.WriteLine(text);
            Console.WriteLine(text);
        }

        /// <summary>
        /// Load the main project
        /// </summary>
        /// <param name="project">A json element containing the project</param>
        /// <returns></returns>
        public static LDtkProject LoadProject(JsonElement project)
        {
            LDtkProject output = new LDtkProject();

            foreach(JsonProperty property in project.EnumerateObject().ToArray())
            {
                if (property.Value.ValueKind != JsonValueKind.Null)
                {
                    if (property.Name == "bgColor")
                    {
                        output.BackgroundColor = property.Value.GetString();
                    }
                    else if (property.Name == "defs")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine("THIS IS A FUCKING TEST");
                        output.Definitions = LoadDefinitions(property);
                    }
                    else if (property.Name == "externalLevels")
                    {
                        LevelsAreExternal = property.Value.GetBoolean();
                    }
                    else if (property.Name == "jsonVersion")
                    {
                        fileVersion = property.Value.GetString();
                    }
                    else if (property.Name == "levels")
                    {
                        output.Levels = LoadLevels(property);
                    }
                    else if (property.Name == "worldGridHeight")
                    {
                        output.WorldGridHeight = property.Value.GetInt32();
                    }
                    else if (property.Name == "worldGridWidth")
                    {
                        output.WorldGridWidth = property.Value.GetInt32();
                    }
                    else if (property.Name == "worldLayout")
                    {
                        output.WorldLayout = (worldLayoutTypes)Enum.Parse(typeof(worldLayoutTypes), property.Value.GetString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write(spacing + "Warning : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " not used.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write(spacing + "Info : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(property.Name + " was null.");
                }
            }
            return output;
        }

        #region INSTANCES LOAD METHODS

        /// <summary>
        ///  Load the levels of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the entities instances</param>
        /// <returns></returns>
        public static List<Level> LoadLevels(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<Level> output = new List<Level>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                Level level = new Level();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "_bgcolor")
                        {
                            level.BackgroundColor = property.Value.GetString();
                        }
                        if (property.Name == "_bgPos")
                        {
                            level.BackgroundPosition = LoadBackgroundPos(property);
                        }
                        else if (property.Name == "__neighbours")
                        {
                            level.Neighbours = LoadLevelNeighbours(property);
                        }
                        else if (property.Name == "bgRelPath")
                        {
                            level.BackgroundRelPath = property.Value.GetString();
                        }
                        // load here externalRelPath
                        else if (property.Name == "bgRelPath")
                        {
                            level.BackgroundRelPath = property.Value.GetString();
                        }
                        else if (property.Name == "fieldInstances")
                        {
                            level.FieldInstances = LoadFields(property);
                        }
                        else if (property.Name == "identifier")
                        {
                            level.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "layerInstances")
                        {
                            level.LayerInstances = LoadLayers(property);
                        }
                        else if (property.Name == "pxHei")
                        {
                            level.Height = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxWid")
                        {
                            level.Width = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            level.Uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "worldX")
                        {
                            level.WorldCoordinates = new Vector2(property.Value.GetInt32(), jsonElement.GetProperty("worldY").GetInt32());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(level);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        /// <summary>
        ///  Load the levels of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the entities instances</param>
        /// <returns></returns>
        public static BackgroundPosition LoadBackgroundPos(JsonProperty jsonProperty)
        {
            spacing += " | ";
            BackgroundPosition bgPos = new BackgroundPosition();
            foreach (JsonProperty property in jsonProperty.Value.EnumerateObject().ToArray())
            {
                if (property.Value.ValueKind != JsonValueKind.Null)
                {
                    if (property.Name == "cropRect")
                    {
                        bgPos.CropRectangle = new Rectangle(
                            property.Value.EnumerateArray().ToArray()[0].GetInt32(),
                            property.Value.EnumerateArray().ToArray()[1].GetInt32(),
                            property.Value.EnumerateArray().ToArray()[2].GetInt32(),
                            property.Value.EnumerateArray().ToArray()[3].GetInt32());
                    }
                    else if (property.Name == "scale")
                    {
                        bgPos.Scale = new Vector2(
                            property.Value.EnumerateArray().ToArray()[0].GetInt32(),
                            property.Value.EnumerateArray().ToArray()[1].GetInt32());
                    }
                    else if (property.Name == "topLeftPx")
                    {
                        bgPos.Coordinates = new Vector2(
                            property.Value.EnumerateArray().ToArray()[0].GetInt32(),
                            property.Value.EnumerateArray().ToArray()[1].GetInt32());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write(spacing + "Warning : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " not used.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write(spacing + "Info : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(property.Name + " was null.");
                }
            }
            spacing = spacing.Remove(0, 3);
            return bgPos;
        }
        /// <summary>
        /// Load the levels neighbours of a level
        /// </summary>
        /// <param name="jsonProperty">A json property containing the neighbours</param>
        /// <returns></returns>
        public static List<LevelNeighbour> LoadLevelNeighbours(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<LevelNeighbour> output = new List<LevelNeighbour>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                LevelNeighbour neighbour = new LevelNeighbour();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "dir")
                        {
                            if (property.Value.GetString() == "n")
                            {
                                neighbour.Direction = NeighbourDirection.North;
                            }
                            else if (property.Value.GetString() == "s")
                            {
                                neighbour.Direction = NeighbourDirection.South;
                            }
                            else if (property.Value.GetString() == "e")
                            {
                                neighbour.Direction = NeighbourDirection.East;
                            }
                            else if (property.Value.GetString() == "w")
                            {
                                neighbour.Direction = NeighbourDirection.West;
                            }
                        }
                        else if (property.Name == "levelUid")
                        {
                            neighbour.LevelUid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(neighbour);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        public static List<LayerInstance> LoadLayers(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<LayerInstance> output = new List<LayerInstance>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                LayerInstance layer = new LayerInstance();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__cHei")
                        {
                            layer.Height = property.Value.GetInt32();
                        }
                        else if (property.Name == "__cWid")
                        {
                            layer.Width = property.Value.GetInt32();
                        }
                        else if (property.Name == "__gridSize")
                        {
                            layer.GridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "__identifier")
                        {
                            layer.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "__opacity")
                        {
                            layer.Opacity = (float)property.Value.GetDouble();
                        }
                        else if (property.Name == "__pxTotalOffsetX")
                        {
                            layer.TotalOffset = new Vector2(property.Value.GetInt32(), jsonElement.GetProperty("__pxTotalOffsetY").GetInt32());
                        }
                        else if (property.Name == "__tilesetDefUid")
                        {
                            layer.TilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "__tilesetRelPath")
                        {
                            layer.TilesetRelPath = property.Value.GetString();
                        }
                        else if (property.Name == "__type")
                        {
                            layer.Type = (LayerType)Enum.Parse(typeof(LayerType), property.Value.GetString());
                        }
                        else if (property.Name == "autoLayerTiles")
                        {
                            layer.AutoLayerTiles = LoadTiles(property);
                        }
                        else if (property.Name == "entityInstances")
                        {
                            layer.EntityInstances = LoadEntities(property);
                        }
                        else if (property.Name == "gridTiles")
                        {
                            layer.GridTiles = LoadTiles(property);
                        }
                        else if (property.Name == "intGridCsv")
                        {
                            int[] intgrid = new int[property.Value.GetArrayLength()];
                            foreach(JsonElement element in property.Value.EnumerateArray().ToArray())
                            {
                                intgrid.Append(element.GetInt32());
                            }
                            layer.IntGridCsv = intgrid;
                        }
                        else if (property.Name == "levelId")
                        {
                            layer.LevelId = property.Value.GetInt32();
                        }
                        else if (property.Name == "layerDefUid")
                        {
                            layer.LayerDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "overrideTilesetUid")
                        {
                            layer.OverrideTilesetUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxOffsetX")
                        {
                            layer.Offset = new Vector2(property.Value.GetInt32(), jsonElement.GetProperty("pxOffsetY").GetInt32());
                        }
                        else if (property.Name == "visible")
                        {
                            layer.IsVisible = property.Value.GetBoolean();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(layer);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        public static List<TileInstance> LoadTiles(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<TileInstance> output = new List<TileInstance>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                TileInstance tile = new TileInstance();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "f")
                        {
                            int f = property.Value.GetInt32();
                            if (f == 0)
                            {
                                tile.IsFlippedOnX = false;
                                tile.IsFlippedOnY = false;
                            }
                            else if (f == 1)
                            {
                                tile.IsFlippedOnX = true;
                                tile.IsFlippedOnY = false;
                            }
                            else if (f == 2)
                            {
                                tile.IsFlippedOnX = false;
                                tile.IsFlippedOnY = true;
                            }
                            else if (f == 3)
                            {
                                tile.IsFlippedOnX = true;
                                tile.IsFlippedOnY = true;
                            }
                        }
                        else if (property.Name == "px")
                        {
                            tile.Coordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "src")
                        {
                            tile.Source = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "t")
                        {
                            tile.TileId = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(tile);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        public static List<EntityInstance> LoadEntities(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<EntityInstance> output = new List<EntityInstance>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EntityInstance entity = new EntityInstance();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__grid")
                        {
                            entity.GridCoordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "__identifier")
                        {
                            entity.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "__pivot")
                        {
                            entity.PivotCoordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetSingle(), property.Value.EnumerateArray().ToArray()[1].GetSingle());
                        }
                        else if (property.Name == "__tile")
                        {
                            EntityTile entityTile = new EntityTile();
                            entityTile.SourceRectangle = new Rectangle(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32(), property.Value.EnumerateArray().ToArray()[2].GetInt32(), property.Value.EnumerateArray().ToArray()[3].GetInt32());
                            entityTile.TilesetUid = property.Value.GetInt32();
                            entity.Tile = entityTile;
                        }
                        else if (property.Name == "defUid")
                        {
                            entity.DefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "fieldInstances")
                        {
                            entity.FieldInstances = LoadFields(property);
                        }
                        else if (property.Name == "height")
                        {
                            entity.Height = property.Value.GetInt32();
                        }
                        else if (property.Name == "px")
                        {
                            entity.Coordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "width")
                        {
                            entity.Width = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(entity);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        public static List<FieldInstance> LoadFields(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<FieldInstance> output = new List<FieldInstance>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                FieldInstance field = new FieldInstance();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__identifier")
                        {
                            field.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "__type")
                        {
                            if (property.Value.GetString().StartsWith("Enum"))
                            {
                                field.type = FieldType.Enum;
                                field.isArray = false;
                                field.enumName = property.Value.GetString().Substring(5, property.Value.GetString().Length - 6);
                            }
                            else if (property.Value.GetString().StartsWith("LocalEnum"))
                            {
                                field.type = FieldType.Enum;
                                field.isArray = false;
                                field.enumName = property.Value.GetString().Substring(("LocalEnum").Length + 1);
                            }
                            else if (property.Value.GetString().StartsWith("Array"))
                            {
                                field.isArray = true;
                                string arrayType = property.Value.GetString().Substring(6, property.Value.GetString().Length - 7);
                                if (arrayType.StartsWith("Enum"))
                                {
                                    field.type = FieldType.Enum;
                                    field.enumName = arrayType.Substring(5, property.Value.GetString().Length - 1);
                                }
                                else if (arrayType.StartsWith("LocalEnum"))
                                {
                                    field.type = FieldType.Enum;
                                    field.enumName = arrayType.Substring(("LocalEnum").Length + 1);
                                }
                                else
                                {
                                    field.type = (FieldType)Enum.Parse(typeof(FieldType), arrayType);
                                }
                            }
                            else
                            {
                                field.type = (FieldType)Enum.Parse(typeof(FieldType), property.Value.GetString());
                                field.isArray = false;
                            }
                        }
                        else if (property.Name == "__value")
                        {
                            field.value = property.Value.GetRawText();
                        }
                        else if (property.Name == "defUid")
                        {
                            field.defUid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(field);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        #endregion

        #region DEFINTIONS LOAD METHODS

        /// <summary>
        /// Load the definitions of a project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the definitions</param>
        /// <returns></returns>
        public static Definitions LoadDefinitions(JsonProperty jsonProperty)
        {
            spacing += " | ";
            Definitions output = new Definitions();
            foreach (JsonProperty property in jsonProperty.Value.EnumerateObject().ToArray())
            {
                if (property.Value.ValueKind != JsonValueKind.Null)
                {
                    //do somethin
                    if (property.Name == "entities")
                    {
                        output.Entities = LoadEntitiesDef(property);
                    }
                    else if (property.Name == "enums")
                    {
                        output.Enums = LoadEnumsDef(property);
                    }
                    else if (property.Name == "externalEnums")
                    {
                        output.ExternalEnums = LoadEnumsDef(property);
                    }
                    else if (property.Name == "layers")
                    {
                        output.Layers = LoadLayersDef(property);
                    }
                    else if (property.Name == "tilesets")
                    {
                        output.Tilesets = LoadTilesets(property);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(spacing + "Warning : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(property.Name + " not used.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write(spacing + "Info : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(property.Name + " was null.");
                }
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the tilesets of a project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the Tilesets defintions</param>
        /// <returns></returns>
        public static List<Tileset> LoadTilesets(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<Tileset> output = new List<Tileset>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                Tileset tileset = new Tileset();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "identifier")
                        {
                            tileset.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "padding")
                        {
                            tileset.Padding = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxHei")
                        {
                            tileset.Height = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxWid")
                        {
                            tileset.Width = property.Value.GetInt32();
                        }
                        else if (property.Name == "relPath")
                        {
                            tileset.RelPath = property.Value.GetString();
                        }
                        else if (property.Name == "spacing")
                        {
                            tileset.Spacing = property.Value.GetInt32();
                        }
                        else if (property.Name == "tileGridSize")
                        {
                            tileset.TileGridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            tileset.Uid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(tileset);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the layers definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the layers defintions</param>
        /// <returns></returns>
        public static List<LayerDef> LoadLayersDef(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<LayerDef> output = new List<LayerDef>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                LayerDef layerDef = new LayerDef();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__type")
                        {
                            layerDef.Type = (LayerType)Enum.Parse(typeof(LayerType), property.Value.GetString());
                        }
                        else if (property.Name == "autoSourceLayerDefUid")
                        {
                            layerDef.AutoSourceLayerDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "autoTilesetDefUid")
                        {
                            layerDef.AutoTilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "displayOpacity")
                        {
                            layerDef.DisplayOpacity = (float)property.Value.GetDouble();
                        }
                        else if (property.Name == "gridSize")
                        {
                            layerDef.GridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            layerDef.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "intGridValues")
                        {
                            layerDef.IntGridValues = LoadIntGridValuesDef(property);
                        }
                        else if (property.Name == "pxOffsetX")
                        {
                            layerDef.Offset = new Vector2(property.Value.GetInt32(), jsonElement.GetProperty("pxOffsetY").GetInt32());
                        }
                        else if (property.Name == "tilesetDefUid")
                        {
                            layerDef.TilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            layerDef.Uid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(layerDef);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the Intgrid Values definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the Intgrid Values defintions</param>
        /// <returns></returns>
        public static List<IntGridValuesDef> LoadIntGridValuesDef(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<IntGridValuesDef> output = new List<IntGridValuesDef>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                IntGridValuesDef intGridValue = new IntGridValuesDef();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "color")
                        {
                            intGridValue.Color = property.Value.GetString();
                        }
                        else if (property.Name == "identifier")
                        {
                            intGridValue.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "value")
                        {
                            intGridValue.Value = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(intGridValue);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the enums definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the enums defintions</param>
        /// <returns></returns>
        public static List<EnumDef> LoadEnumsDef(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<EnumDef> output = new List<EnumDef>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EnumDef enumDef = new EnumDef();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "externalRelPath")
                        {
                            enumDef.ExternalRelPath = property.Value.GetString();
                        }
                        else if (property.Name == "iconTilesetUid")
                        {
                            enumDef.IconTilesetUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            enumDef.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "uid")
                        {
                            enumDef.Uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "values")
                        {
                            enumDef.Values = LoadEnumsValuesDef(property);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(enumDef);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the enums values definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the enums values defintions</param>
        /// <returns></returns>
        public static List<EnumValueDef> LoadEnumsValuesDef(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<EnumValueDef> output = new List<EnumValueDef>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EnumValueDef enumValue = new EnumValueDef();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__tileSrcRect")
                        {
                            Rectangle returned = new Rectangle(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32(), property.Value.EnumerateArray().ToArray()[2].GetInt32(), property.Value.EnumerateArray().ToArray()[3].GetInt32());
                        }
                        else if (property.Name == "id")
                        {
                            enumValue.Id = property.Value.GetString();
                        }
                        else if (property.Name == "tileId")
                        {
                            enumValue.TileId = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(enumValue);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }
        /// <summary>
        /// Load the entities definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the entities defintions</param>
        /// <returns></returns>
        public static List<EntitieDef> LoadEntitiesDef(JsonProperty jsonProperty)
        {
            spacing += " | ";
            List<EntitieDef> output = new List<EntitieDef>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EntitieDef entitieDef = new EntitieDef();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "color")
                        {
                            entitieDef.Color = property.Value.GetString();
                        }
                        else if (property.Name == "height")
                        {
                            entitieDef.Height = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            entitieDef.Identifier = property.Value.GetString();
                        }
                        else if (property.Name == "pivotX")
                        {
                            entitieDef.PivotX = property.Value.GetSingle();
                        }
                        else if (property.Name == "pivotY")
                        {
                            entitieDef.PivotY = property.Value.GetSingle();
                        }
                        else if (property.Name == "tileId")
                        {
                            entitieDef.TileId = property.Value.GetInt32();
                        }
                        else if (property.Name == "tilesetId")
                        {
                            entitieDef.TilesetId = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            entitieDef.Uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "width")
                        {
                            entitieDef.Width = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(spacing + "Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write(spacing + "Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(entitieDef);
            }
            spacing = spacing.Remove(0, 3);
            return output;
        }

        #endregion
    }
}
