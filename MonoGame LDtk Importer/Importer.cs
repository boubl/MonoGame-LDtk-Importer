using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Text.Json;

namespace Importer
{
    /// <summary>
    /// A class to import ldtk files
    /// </summary>
    [ContentImporter(".ldtk", DisplayName = "LDtk Project Importer", DefaultProcessor = "LDtk Project Processor")]
    public class Importer : ContentImporter<JsonElement>
    {
        /// <summary>
        /// Import the .ldtk file and parse it as a <see cref="JsonElement"/>
        /// </summary>
        /// <param name="filename">Name of the .ldtk file</param>
        /// <param name="context"></param>
        /// <returns>A <see cref="JsonElement"/> containing the values of the .ldtk file</returns>
        public override JsonElement Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing LDtk project: {0}", filename);
            return JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(filename));
        }
    }
}
