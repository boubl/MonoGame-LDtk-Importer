using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MonoGame_LDtk_Importer
{
    /// <summary>
    /// A LDtk project containing layers definitions and levels
    /// </summary>
    public class LDtkProject
    {
        /// <summary>
        /// Default X pivot (0 to 1) for new entities
        /// </summary>
        public float defaultPivotX { get; set; }
        /// <summary>
        /// Default Y pivot (0 to 1) for new entities
        /// </summary>
        public float defaultPivotY { get; set; }
        /// <summary>
        /// Default grid size for new layers
        /// </summary>
        public int defaultGridSize { get; set; }
        /// <summary>
        /// Project background color
        /// </summary>
        public string bgColor { get; set; }
        /// <summary>
        /// Default background color of levels
        /// </summary>
        public string defaultLevelBgColor { get; set; }
        /// <summary>
        /// If TRUE, the Json is partially minified (no indentation, nor line breaks, default is FALSE)
        /// </summary>
        public bool minifyJson { get; set; }
        /// <summary>
        /// An enum that describes how levels are organized in this project (ie. linearly or in a 2D space).
        /// Possible values are: Free, GridVania, LinearHorizontal and LinearVertical.
        /// </summary>
        public worldLayoutTypes worldLayout { get; set; }
        /// <summary>
        /// Width of the world grid in pixels.
        /// </summary>
        public int worldGridWidth { get; set; }
        /// <summary>
        /// Height of the world grid in pixels.
        /// </summary>
        public int worldGridHeight { get; set; }
        /// <summary>
        /// A structure containing all the definitions of this project
        /// </summary>
        public Definitions defs { get; set; }
        /// <summary>
        /// All levels. The order of this array is only relevant in LinearHorizontal and
        /// linearVertical world layouts (see worldLayout value).
        /// Otherwise, you should refer to the worldX, worldY coordinates of each Level.
        /// </summary>
        public List<Level> levels { get; set; }

        public LDtkProject()
        {
            defs = new Definitions();
            levels = new List<Level>();
        }
    }

    /// <summary>
    /// Types of the world layout
    /// </summary>
    public enum worldLayoutTypes
    {
        Free,
        GridVania,
        LinearHorizontal,
        LinearVertical
    }

    /// <summary>
    /// A structure containing all the definitions of a project
    /// </summary>
    public class Definitions
    {
        /// <summary>
        /// All the layers definitions
        /// </summary>
        public List<LayerDef> layers { get; set; }
        /// <summary>
        /// All the entities definitions
        /// </summary>
        public List<EntitieDef> entities { get; set; }
        /// <summary>
        /// All the enums defintions
        /// </summary>
        public List<MonoGame_LDtk_Importer.EnumDef> enums { get; set; }
        /// <summary>
        /// External enums are exactly the same as enums, except they have a externalRelPath to point to an external source file
        /// </summary>
        public List<MonoGame_LDtk_Importer.EnumDef> externalEnums { get; set; }
        /// <summary>
        /// All the tilesets
        /// </summary>
        public List<Tileset> tilesets { get; set; }

        public Definitions()
        {
            layers = new List<LayerDef>();
            entities = new List<EntitieDef>();
            enums = new List<EnumDef>();
            externalEnums = new List<EnumDef>();
            tilesets = new List<Tileset>();
        }
    }

    public struct EnumDef
    {
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// All possible enum values, with their optional Tile infos
        /// </summary>
        public List<EnumValue> values { get; set; }
        /// <summary>
        /// Tileset UID if provided
        /// </summary>
        public int iconTilesetUid { get; set; }
        /// <summary>
        /// Relative path to the external file providing this Enum. Only for External enums
        /// </summary>
        public string externalRelPath { get; set; }

    }

    /// <summary>
    /// An enum value, with the optional Tile infos
    /// </summary>
    public struct EnumValue
    {
        /// <summary>
        /// Enum value
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// The optional ID of the tile
        /// </summary>
        public int ?tileId { get; set; }
        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: [ x, y, width, height ]
        /// </summary>
        public List<int> __tileSrcRect { get; set; }
    }

    /// <summary>
    /// A layer of project
    /// </summary>
    public struct LayerDef
    {
        /// <summary>
        /// Type of the layer (IntGrid, Entities, Tiles or AutoLayer)
        /// </summary>
        public LayerType type { get; set; }
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// Width and height of the grid in pixels
        /// </summary>
        public int gridSize { get; set; }
        /// <summary>
        /// Opacity of the layer (0 to 1.0)
        /// </summary>
        public float displayOpacity { get; set; }
        /// <summary>
        /// X offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance optional offset)
        /// </summary>
        public int pxOffsetX { get; set; }
        /// <summary>
        /// Y offset of the layer, in pixels (IMPORTANT: this should be added to the LayerInstance optional offset)
        /// </summary>
        public int pxOffsetY { get; set; }
        /// <summary>
        /// Values for a IntGrid layer
        /// </summary>
        public List<IntGridValuesDef> intGridValues { get; set; }
        /// <summary>
        /// Reference to the Tileset UID being used by this auto-layer rules
        /// </summary>
        public int? autoTilesetDefUid { get; set; }
        /// <summary>
        /// Empty for now
        /// </summary>
        public int? autoSourceLayerDefUid { get; set; }
        /// <summary>
        /// Reference to the Tileset UID being used by this tile layer
        /// </summary>
        public int? tilesetDefUid { get; set; }
    }

    /// <summary>
    /// Type of a layer
    /// </summary>
    public enum LayerType
    {
        IntGrid,
        Entities,
        Tiles,
        AutoLayer
    }

    /// <summary>
    /// Values for a IntGrid layer
    /// </summary>
    public struct IntGridValuesDef
    {
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Hex color "#rrggbb"
        /// </summary>
        public string color { get; set; }
    }

    /// <summary>
    /// A group of rules
    /// </summary>
    public struct AutoRuleGroup
    {
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// Name of the group
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// If FALSE, all the rules effects of the group aren't applied, and no tiles are generated.
        /// </summary>
        public bool active { get; set; }
        /// <summary>
        /// If TRUE, the group is collapsed in LDtk (not very useful)
        /// </summary>
        public bool collapsed { get; set; }
        /// <summary>
        /// The rules of the group
        /// </summary>
        public List<Rule> rules { get; set; }
    }

    /// <summary>
    /// A rule
    /// </summary>
    public struct Rule
    {
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// If FALSE, the rule effect isn't applied, and no tiles are generated.
        /// </summary>
        public bool active { get; set; }
        /// <summary>
        /// Pattern width & height. Should only be 1,3,5 or 7.
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// Array of all the tile IDs. They are used randomly or as stamps, based on tileMode value.
        /// </summary>
        public int[] tilesIds { get; set; }
        /// <summary>
        /// Chances for this rule to be applied (0 to 1)
        /// </summary>
        public int chance { get; set; }
        /// <summary>
        /// When TRUE, the rule will prevent other rules to be applied in the same cell if it matches (TRUE by default).
        /// </summary>
        public bool breakOnMatch { get; set; }
        /// <summary>
        /// Rule pattern (size x size)
        /// </summary>
        public int[] pattern { get; set; }
        /// <summary>
        /// If TRUE, allow rule to be matched by flipping its pattern horizontally
        /// </summary>
        public bool flipX { get; set; }
        /// <summary>
        /// If TRUE, allow rule to be matched by flipping its pattern vertically
        /// </summary>
        public bool flipY { get; set; }
        /// <summary>
        /// X cell coord modulo
        /// </summary>
        public int xModulo { get; set; }
        /// <summary>
        /// Y cell coord modulo
        /// </summary>
        public int yModulo { get; set; }
        // Demander au dev pourquoi il y a un "None" dans le Json et pourtant c'est indiqué comme bool dans la doc
        /// <summary>
        /// If TRUE, enable checker mode
        /// </summary>
        public bool checker { get; set; }
        /// <summary>
        /// Defines how tileIds array is used
        /// </summary>
        public TileMode tileMode { get; set; }
        /// <summary>
        /// X pivot of a tile stamp (0-1)
        /// </summary>
        public int pivotX { get; set; }
        /// <summary>
        /// Y pivot of a tile stamp (0-1)
        /// </summary>
        public int pivotY { get; set; }
        /// <summary>
        /// If TRUE, enable Perlin filtering to only apply rule on specific random area
        /// </summary>
        public bool perlinActive { get; set; }
        // Demander au dev pourquoi ça ressemble plus à un int alors que la doc dit que c'est un float
        /// <summary>
        /// The seed of the perlin noise
        /// </summary>
        public int perlinSeed { get; set; }
        /// <summary>
        /// Scale of the perlin noise in %
        /// </summary>
        public float perlinScale { get; set; }
        // Demander au dev pourquoi ça ressemble plus à un int alors que la doc dit que c'est un float
        /// <summary>
        /// Number of octave of the perlin noise (1-4)
        /// </summary>
        public float perlinOctaves { get; set; }
    }

    // Demander au dev les différents mode de tiles
    /// <summary>
    /// A rule tile mode
    /// </summary>
    public enum TileMode
    {
        Single,
        Stamp
    }

    /// <summary>
    ///  An entity definition
    /// </summary>
    public struct EntitieDef
    {
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// Pixel width
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Pixel height
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// Base entity color
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// Tileset ID used for optional tile display
        /// </summary>
        public int tilesetId { get; set; }
        /// <summary>
        /// Tile ID used for optional tile display
        /// </summary>
        public int tileId { get; set; }
        /// <summary>
        /// Max instances per level
        /// </summary>
        public int maxPerLevel { get; set; }
        /// <summary>
        /// Pivot X coordinate (from 0 to 1.0)
        /// </summary>
        public float pivotX { get; set; }
        /// <summary>
        /// Pivot Y coordinate (from 0 to 1.0)
        /// </summary>
        public float pivotY { get; set; }
    }

    /// <summary>
    /// A entity field type
    /// </summary>
    public enum FieldType
    {
        Int,
        Float,
        Bool,
        String,
        Text,
        Enum,
        Color,
        Point,
        FilePath
    }

    /// <summary>
    /// A tileset
    /// </summary>
    public struct Tileset
    {
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Distance in pixels from image borders
        /// </summary>
        public int padding { get; set; }
        /// <summary>
        /// Image width in pixels
        /// </summary>
        public int pxHei { get; set; }
        /// <summary>
        /// Image width in pixels
        /// </summary>
        public int pxWid { get; set; }
        /// <summary>
        /// Path to the source file, relative to the current project JSON file
        /// </summary>
        public string relPath { get; set; }
        /// <summary>
        /// Space in pixels between all tiles
        /// </summary>
        public int spacing { get; set; }
        /// <summary>
        /// Size of the grid, of each tile
        /// </summary>
        public int tileGridSize { get; set; }
        /// <summary>
        /// Unique Intidentifier
        /// </summary>
        public int uid { get; set; }
    }

    /// <summary>
    /// A level
    /// </summary>
    public struct Level
    {
        /// <summary>
        /// Background color of the level. If null, the project defaultLevelBgColor should be used.
        /// </summary>
        public string bgColor { get; set; }
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// An array listing all other levels touching this one on the world map
        /// </summary>
        public List<LevelNeighbour> neighbours { get; set; }
        /// <summary>
        /// All the layers of the level
        /// </summary>
        public List<LayerInstance> layerInstances { get; set; }
        /// <summary>
        /// Height of the level in pixels
        /// </summary>
        public int pxHei { get; set; }
        /// <summary>
        /// Width of the level in pixels
        /// </summary>
        public int pxWid { get; set; }
        /// <summary>
        /// Unique Int identifier
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// World X coordinate in pixels
        /// </summary>
        public int worldX { get; set; }
        /// <summary>
        /// World Y coordinate in pixels
        /// </summary>
        public int worldY { get; set; }
    }

    /// <summary>
    /// A level neighbour
    /// </summary>
    public struct LevelNeighbour
    {
        /// <summary>
        /// An enum indicating the level location (North, South, West, East)
        /// </summary>
        public NeighbourDirection dir { get; set; }
        /// <summary>
        /// The Int Identifier of the neighbour level
        /// </summary>
        public int levelUid { get; set; }
    }

    public enum NeighbourDirection
    {
        North,
        South,
        West,
        East
    }

    /// <summary>
    /// A layer instance
    /// </summary>
    public struct LayerInstance
    {
        /// <summary>
        /// Grid-based height
        /// </summary>
        public int cHei { get; set; }
        /// <summary>
        /// Grid-based width
        /// </summary>
        public int cWid { get; set; }
        /// <summary>
        /// Grid size
        /// </summary>
        public int gridSize { get; set; }
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Layer opacity as Float [0-1]
        /// </summary>
        public float opacity { get; set; }
        /// <summary>
        /// Total layer X pixel offset, including both instance and definition offsets.
        /// </summary>
        public int pxTotalOffsetX { get; set; }
        /// <summary>
        /// Total layer Y pixel offset, including both instance and definition offsets.
        /// </summary>
        public int pxTotalOffsetY { get; set; }
        /// <summary>
        /// The definition UID of corresponding Tileset, if any.
        /// </summary>
        public int? tilesetDefUid { get; set; }
        /// <summary>
        /// The relative path to corresponding Tileset, if any.
        /// </summary>
        public string tilesetRelPath { get; set; }
        /// <summary>
        /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
        /// </summary>
        public LayerType type { get; set; }
        /// <summary>
        /// An array containing all tiles generated by Auto-layer rules.
        /// <br/>The array is already sorted in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).
        /// <br/><br/>
        /// <b>Note:</b> <i>if multiple tiles are stacked in the same cell as the result of different rules, all tiles behind opaque ones will be discarded.</i>
        /// </summary>
        public List<TileInstance> autoLayerTiles { get; set; }
        /// <summary>
        /// All the values of a Entity layer
        /// </summary>
        public List<EntityInstance> entityInstances { get; set; }
        /// <summary>
        /// All the tiles of a Tile layer
        /// </summary>
        public List<TileInstance> gridTiles { get; set; }
        /// <summary>
        /// All the values of a IntGrid layer
        /// </summary>
        public List<IntGridValue> intGrid { get; set; }
        /// <summary>
        /// A list of all values in the IntGrid layer, stored from left to right,
        /// and top to bottom: <b>-1</b> means "empty cell" and IntGrid values start at 0.
        /// <br/>This array size is <b>__cWid</b> x <b>__cHei</b> cells.
        /// </summary>
        public int[] intGridCsv { get; set; }
        /// <summary>
        /// Reference the Layer definition UID
        /// </summary>
        public int layerDefUid { get; set; }
        /// <summary>
        /// Reference to the UID of the level containing this layer instance
        /// </summary>
        public int levelId { get; set; }
        /// <summary>
        /// This layer can use another tileset by overriding the tileset UID here
        /// </summary>
        public int? overrideTilesetUid { get; set; }
        /// <summary>
        /// X offset in pixels to render this layer, usually 0
        /// <br/><i>(<b>IMPORTANT:</b> this should be added to the LayerDef optional offset, see __pxTotalOffsetX)</i>
        /// </summary>
        public int pxOffsetX { get; set; }
        /// <summary>
        /// Y offset in pixels to render this layer, usually 0
        /// <br/><i>(<b>IMPORTANT:</b> this should be added to the LayerDef optional offset, see __pxTotalOffsetY)</i>
        /// </summary>
        public int pxOffsetY { get; set; }
        /// <summary>
        /// Random seed used for Auto-Layers rendering
        /// </summary>
        public int seed { get; set; }
    }

    /// <summary>
    /// A value in a IntGrid layer
    /// </summary>
    public struct IntGridValue
    {
        /// <summary>
        /// Coordinate ID in the layer grid
        /// </summary>
        public Vector2 coordinates { get; set; }
        /// <summary>
        /// IntGrid Value
        /// </summary>
        public int value { get; set; }
    }

    /// <summary>
    /// A tile instance
    /// </summary>
    public struct TileInstance
    {
        /// <summary>
        /// True if the tile is flipped on x axis
        /// </summary>
        public bool xFlip { get; set; }
        /// <summary>
        /// True if the tile is flipped on y axis
        /// </summary>
        public bool yFlip { get; set; }
        /// <summary>
        /// Pixel coordinates of the tile in the layer. Don't forget optional layer offsets, if they exist!
        /// </summary>
        public Vector2 coordinates { get; set; }
        /// <summary>
        /// Pixel coordinates of the tile in the <b>tileset</b>
        /// </summary>
        public Vector2 src { get; set; }
        /// <summary>
        /// The <i>Tile ID</i> in the corresponding tileset.
        /// </summary>
        public int tileId { get; set; }
    }

    /// <summary>
    /// An entity instance
    /// </summary>
    public struct EntityInstance
    {
        /// <summary>
        /// Grid-based coordinates
        /// </summary>
        public Vector2 gridCoordinates { get; set; }
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Pivot coordinates (values are from 0 to 1) of the Entity
        /// </summary>
        public Vector2 pivotCoordinates { get; set; }
        /// <summary>
        /// Optional Tile used to display this entity (it could either be the default Entity tile, or some tile provided by a field value, like an Enum)
        /// </summary>
        public EntityTile tile { get; set; }
        /// <summary>
        /// Reference of the <b>Entity definition</b> UID
        /// </summary>
        public int defUid { get; set; }
        /// <summary>
        /// All the fields of the entity
        /// </summary>
        public List<EntityField> fieldInstances { get; set; }
        /// <summary>
        /// Pixel coordinates. Don't forget optional layer offsets, if they exist!
        /// </summary>
        public Vector2 coordinates { get; set; }
    }

    /// <summary>
    /// An entity tile
    /// </summary>
    public struct EntityTile
    {
        /// <summary>
        /// An array of 4 Int values that refers to the tile in the tileset image: <c>[x, y, width, height]</c>
        /// </summary>
        public int[] srcRect { get; set; }
        /// <summary>
        /// Tileset ID
        /// </summary>
        public int tilesetUid { get; set; }
    }

    /// <summary>
    /// Entity field instance
    /// </summary>
    public struct EntityField
    {
        /// <summary>
        /// Unique String identifier
        /// </summary>
        public string identifier { get; set; }
        /// <summary>
        /// Type of the field, such as Int, Float, Enum, Bool, etc.
        /// </summary>
        public FieldType type { get; set; }
        /// <summary>
        /// Raw text of the actual value of the field instance.
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// Reference of the <b>Field definition</b> UID
        /// </summary>
        public int defUid { get; set; }
        /// <summary>
        /// Name of the enum, if the field is an enum
        /// </summary>
        public string enumName { get; set; }
        /// <summary>
        /// True if the field is an array
        /// </summary>
        public bool isArray { get; set; }
    }
}
