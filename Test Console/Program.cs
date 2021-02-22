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
        static void Main(string[] args)
        {

            JsonElement jsonFile = JsonSerializer.Deserialize<JsonDocument>(File.ReadAllText(@"C:\Users\Titou\source\repos\MonoGame LDtk Importer\App\Content\Test_file_for_API_showing_all_features.ldtk")).RootElement;
            
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
                        output.bgColor = property.Value.GetString();
                    }
                    else if (property.Name == "defaultGridSize")
                    {
                        output.defaultGridSize = property.Value.GetInt32();
                    }
                    else if (property.Name == "defaultLevelBgColor")
                    {
                        output.defaultLevelBgColor = property.Value.GetString();
                    }
                    else if (property.Name == "defaultPivotX")
                    {
                        output.defaultPivotX = property.Value.GetSingle();
                    }
                    else if (property.Name == "defaultPivotY")
                    {
                        output.defaultPivotY = property.Value.GetSingle();
                    }
                    else if (property.Name == "defs")
                    {
                        output.defs = LoadDefinitions(property);
                    }
                    else if (property.Name == "levels")
                    {
                        output.levels = LoadLevels(property);
                    }
                    else if (property.Name == "worldGridHeight")
                    {
                        output.worldGridHeight = property.Value.GetInt32();
                    }
                    else if (property.Name == "worldGridWidth")
                    {
                        output.worldGridWidth = property.Value.GetInt32();
                    }
                    else if (property.Name == "worldLayout")
                    {
                        output.worldLayout = (worldLayoutTypes)Enum.Parse(typeof(worldLayoutTypes), property.Value.GetString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write("Warning : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " not used.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write("Info : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(property.Name + " was null.");
                }
            }
            return output;
        }

        // DEFINTIONS LOAD METHODS

        /// <summary>
        /// Load the definitions of a project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the definitions</param>
        /// <returns></returns>
        public static Definitions LoadDefinitions(JsonProperty jsonProperty)
        {
            Definitions output = new Definitions();
            foreach (JsonProperty property in jsonProperty.Value.EnumerateObject().ToArray())
            {
                if (property.Value.ValueKind != JsonValueKind.Null)
                {
                    //do somethin
                    if (property.Name == "entities")
                    {
                        output.entities = LoadEntitiesDef(property);
                    }
                    else if (property.Name == "enums")
                    {
                        output.enums = LoadEnumsDef(property);
                    }
                    else if (property.Name == "externalEnums")
                    {
                        output.externalEnums = LoadEnumsDef(property);
                    }
                    else if (property.Name == "layers")
                    {
                        output.layers = LoadLayersDef(property);
                    }
                    else if (property.Name == "tilesets")
                    {
                        output.tilesets = LoadTilesets(property);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Warning : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(property.Name + " not used.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Write("Info : ");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(property.Name + " was null.");
                }
            }
            return output;
        }
        /// <summary>
        /// Load the tilesets of a project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the Tilesets defintions</param>
        /// <returns></returns>
        public static List<Tileset> LoadTilesets(JsonProperty jsonProperty)
        {
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
                            tileset.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "padding")
                        {
                            tileset.padding = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxHei")
                        {
                            tileset.pxHei = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxWid")
                        {
                            tileset.pxWid = property.Value.GetInt32();
                        }
                        else if (property.Name == "relPath")
                        {
                            tileset.relPath = property.Value.GetString();
                        }
                        else if (property.Name == "spacing")
                        {
                            tileset.spacing = property.Value.GetInt32();
                        }
                        else if (property.Name == "tileGridSize")
                        {
                            tileset.tileGridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            tileset.uid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(tileset);
            }
            return output;
        }
        /// <summary>
        /// Load the layers definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the layers defintions</param>
        /// <returns></returns>
        public static List<LayerDef> LoadLayersDef(JsonProperty jsonProperty)
        {
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
                            layerDef.type = (LayerType)Enum.Parse(typeof(LayerType), property.Value.GetString());
                        }
                        else if (property.Name == "autoSourceLayerDefUid")
                        {
                            layerDef.autoSourceLayerDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "autoTilesetDefUid")
                        {
                            layerDef.autoTilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "displayOpacity")
                        {
                            layerDef.displayOpacity = (float)property.Value.GetDouble();
                        }
                        else if (property.Name == "gridSize")
                        {
                            layerDef.gridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            layerDef.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "intGridValues")
                        {
                            layerDef.intGridValues = LoadIntGridValuesDef(property);
                        }
                        else if (property.Name == "pxOffsetX")
                        {
                            layerDef.pxOffsetX = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxOffsetY")
                        {
                            layerDef.pxOffsetY = property.Value.GetInt32();
                        }
                        else if (property.Name == "tilesetDefUid")
                        {
                            layerDef.tilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            layerDef.uid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(layerDef);
            }
            return output;
        }
        /// <summary>
        /// Load the Intgrid Values definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the Intgrid Values defintions</param>
        /// <returns></returns>
        public static List<IntGridValuesDef> LoadIntGridValuesDef(JsonProperty jsonProperty)
        {
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
                            intGridValue.color = property.Value.GetString();
                        }
                        else if (property.Name == "identifier")
                        {
                            intGridValue.identifier = property.Value.GetString();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(intGridValue);
            }
            return output;
        }
        /// <summary>
        /// Load the enums definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the enums defintions</param>
        /// <returns></returns>
        public static List<EnumDef> LoadEnumsDef(JsonProperty jsonProperty)
        {
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
                            enumDef.externalRelPath = property.Value.GetString();
                        }
                        else if (property.Name == "iconTilesetUid")
                        {
                            enumDef.iconTilesetUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            enumDef.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "uid")
                        {
                            enumDef.uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "values")
                        {
                            enumDef.values = LoadEnumsValuesDef(property);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(enumDef);
            }
            return output;
        }
        /// <summary>
        /// Load the enums values definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the enums values defintions</param>
        /// <returns></returns>
        public static List<EnumValue> LoadEnumsValuesDef(JsonProperty jsonProperty)
        {
            List<EnumValue> output = new List<EnumValue>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EnumValue enumValue = new EnumValue();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "__tileSrcRect")
                        {
                            List<int> returned = new List<int>();
                            foreach(JsonElement i in property.Value.EnumerateArray().ToArray())
                            {
                                returned.Add(i.GetInt32());
                            }
                            enumValue.__tileSrcRect = returned;
                        }
                        else if (property.Name == "id")
                        {
                            enumValue.id = property.Value.GetString();
                        }
                        else if (property.Name == "tileId")
                        {
                            enumValue.tileId = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(enumValue);
            }
            return output;
        }
        /// <summary>
        /// Load the entities definitions of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the entities defintions</param>
        /// <returns></returns>
        public static List<EntitieDef> LoadEntitiesDef(JsonProperty jsonProperty)
        {
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
                            entitieDef.color = property.Value.GetString();
                        }
                        else if (property.Name == "height")
                        {
                            entitieDef.height = property.Value.GetInt32();
                        }
                        else if (property.Name == "identifier")
                        {
                            entitieDef.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "maxPerLevel")
                        {
                            entitieDef.maxPerLevel = property.Value.GetInt32();
                        }
                        else if (property.Name == "pivotX")
                        {
                            entitieDef.pivotX = property.Value.GetSingle();
                        }
                        else if (property.Name == "pivotY")
                        {
                            entitieDef.pivotY = property.Value.GetSingle();
                        }
                        else if (property.Name == "tileId")
                        {
                            entitieDef.tileId = property.Value.GetInt32();
                        }
                        else if (property.Name == "tilesetId")
                        {
                            entitieDef.tilesetId = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            entitieDef.uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "width")
                        {
                            entitieDef.width = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(entitieDef);
            }
            return output;
        }

        // INSTANCES LOAD METHODS

        /// <summary>
        ///  Load the levels of project
        /// </summary>
        /// <param name="jsonProperty">A json property containing the entities instances</param>
        /// <returns></returns>
        public static List<Level> LoadLevels(JsonProperty jsonProperty)
        {
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
                            level.bgColor = property.Value.GetString();
                        }
                        else if (property.Name == "__neighbours")
                        {
                            level.neighbours = LoadLevelNeighbours(property);
                        }
                        else if (property.Name == "identifier")
                        {
                            level.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "layerInstances")
                        {
                            level.layerInstances = LoadLayers(property);
                        }
                        else if (property.Name == "pxHei")
                        {
                            level.pxHei = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxWid")
                        {
                            level.pxWid = property.Value.GetInt32();
                        }
                        else if (property.Name == "uid")
                        {
                            level.uid = property.Value.GetInt32();
                        }
                        else if (property.Name == "worldX")
                        {
                            level.worldX = property.Value.GetInt32();
                        }
                        else if (property.Name == "worldY")
                        {
                            level.worldY = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(level);
            }
            return output;
        }
        /// <summary>
        /// Load the levels neighbours of a level
        /// </summary>
        /// <param name="jsonProperty">A json property containing the neighbours</param>
        /// <returns></returns>
        public static List<LevelNeighbour> LoadLevelNeighbours(JsonProperty jsonProperty)
        {
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
                                neighbour.dir = NeighbourDirection.North;
                            }
                            else if (property.Value.GetString() == "s")
                            {
                                neighbour.dir = NeighbourDirection.South;
                            }
                            else if (property.Value.GetString() == "e")
                            {
                                neighbour.dir = NeighbourDirection.East;
                            }
                            else if (property.Value.GetString() == "w")
                            {
                                neighbour.dir = NeighbourDirection.West;
                            }
                        }
                        else if (property.Name == "levelUid")
                        {
                            neighbour.levelUid = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(neighbour);
            }
            return output;
        }

        public static List<LayerInstance> LoadLayers(JsonProperty jsonProperty)
        {
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
                            layer.cHei = property.Value.GetInt32();
                        }
                        else if (property.Name == "__cWid")
                        {
                            layer.cWid = property.Value.GetInt32();
                        }
                        else if (property.Name == "__gridSize")
                        {
                            layer.gridSize = property.Value.GetInt32();
                        }
                        else if (property.Name == "__identifier")
                        {
                            layer.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "__opacity")
                        {
                            layer.opacity = (float)property.Value.GetDouble();
                        }
                        else if (property.Name == "__pxTotalOffsetX")
                        {
                            layer.pxTotalOffsetX = property.Value.GetInt32();
                        }
                        else if (property.Name == "__pxTotalOffsetY")
                        {
                            layer.pxTotalOffsetY = property.Value.GetInt32();
                        }
                        else if (property.Name == "__tilesetDefUid")
                        {
                            layer.tilesetDefUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "__tilesetRelPath")
                        {
                            layer.tilesetRelPath = property.Value.GetString();
                        }
                        else if (property.Name == "__type")
                        {
                            layer.type = (LayerType)Enum.Parse(typeof(LayerType), property.Value.GetString());
                        }
                        else if (property.Name == "autoLayerTiles")
                        {
                            layer.autoLayerTiles = LoadTiles(property);
                        }
                        else if (property.Name == "entityInstances")
                        {
                            layer.entityInstances = LoadEntities(property);
                        }
                        else if (property.Name == "gridTiles")
                        {
                            layer.gridTiles = LoadTiles(property);
                        }
                        else if (property.Name == "intGrid")
                        {
                            layer.intGrid = LoadIntGrid(property, jsonElement.GetProperty("__cWid").GetInt32());
                        }
                        else if (property.Name == "intGridCsv")
                        {
                            int[] intgrid = new int[property.Value.GetArrayLength()];
                            foreach(JsonElement element in property.Value.EnumerateArray().ToArray())
                            {
                                intgrid.Append(element.GetInt32());
                            }
                            layer.intGridCsv = intgrid;
                        }
                        else if (property.Name == "levelId")
                        {
                            layer.levelId = property.Value.GetInt32();
                        }
                        else if (property.Name == "overrideTilesetUid")
                        {
                            layer.overrideTilesetUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxOffsetX")
                        {
                            layer.pxOffsetX = property.Value.GetInt32();
                        }
                        else if (property.Name == "pxOffsetY")
                        {
                            layer.pxOffsetY = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(layer);
            }
            return output;
        }

        public static List<TileInstance> LoadTiles(JsonProperty jsonProperty)
        {
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
                                tile.xFlip = false;
                                tile.yFlip = false;
                            }
                            else if (f == 1)
                            {
                                tile.xFlip = true;
                                tile.yFlip = false;
                            }
                            else if (f == 2)
                            {
                                tile.xFlip = false;
                                tile.yFlip = true;
                            }
                            else if (f == 3)
                            {
                                tile.xFlip = true;
                                tile.yFlip = true;
                            }
                        }
                        else if (property.Name == "px")
                        {
                            tile.coordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "src")
                        {
                            tile.src = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "t")
                        {
                            tile.tileId = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(tile);
            }
            return output;
        }

        public static List<EntityInstance> LoadEntities(JsonProperty jsonProperty)
        {
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
                            entity.gridCoordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "__identifier")
                        {
                            entity.identifier = property.Value.GetString();
                        }
                        else if (property.Name == "__pivot")
                        {
                            entity.pivotCoordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else if (property.Name == "__tile")
                        {
                            EntityTile entityTile = new EntityTile();
                            entityTile.srcRect = new int[4] { property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32(), property.Value.EnumerateArray().ToArray()[2].GetInt32(), property.Value.EnumerateArray().ToArray()[3].GetInt32() };
                            entityTile.tilesetUid = property.Value.GetInt32();
                            entity.tile = entityTile;
                        }
                        else if (property.Name == "defUid")
                        {
                            entity.defUid = property.Value.GetInt32();
                        }
                        else if (property.Name == "fieldInstances")
                        {
                            entity.fieldInstances = LoadFields(property);
                        }
                        else if (property.Name == "px")
                        {
                            entity.coordinates = new Vector2(property.Value.EnumerateArray().ToArray()[0].GetInt32(), property.Value.EnumerateArray().ToArray()[1].GetInt32());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(entity);
            }
            return output;
        }

        public static List<EntityField> LoadFields(JsonProperty jsonProperty)
        {
            List<EntityField> output = new List<EntityField>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                EntityField field = new EntityField();
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
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(field);
            }
            return output;
        }

        public static List<IntGridValue> LoadIntGrid(JsonProperty jsonProperty, int gridBasedWidth)
        {
            List<IntGridValue> output = new List<IntGridValue>();
            foreach (JsonElement jsonElement in jsonProperty.Value.EnumerateArray().ToArray())
            {
                IntGridValue field = new IntGridValue();
                foreach (JsonProperty property in jsonElement.EnumerateObject().ToArray())
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                    {
                        if (property.Name == "coordId")
                        {
                            float Y = (float)Math.Floor((float)(property.Value.GetInt32() / gridBasedWidth));
                            field.coordinates = new Vector2((float)(property.Value.GetInt32() - Y * gridBasedWidth), Y);
                        }
                        else if (property.Name == "v")
                        {
                            field.value = property.Value.GetInt32();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("Warning : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            WriteLine(property.Name + " not used.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Write("Info : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine(property.Name + " was null.");
                    }
                }
                output.Add(field);
            }
            return output;
        }
    }
}
