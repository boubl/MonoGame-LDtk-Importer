using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Importer
{
    /// <summary>
    /// A class to import ldtk files
    /// </summary>
    [ContentImporter(".ldtk", DisplayName = "LDtk Project Importer", DefaultProcessor = "Processor")]
    public class Importer : ContentImporter<Dictionary<string, JsonElement>>
    {
        /// <summary>
        /// Import the .ldtk file and parse it as a Dictionary
        /// </summary>
        /// <param name="filename">Name of the .ldtk file</param>
        /// <param name="context"></param>
        /// <returns>A Dictionary containing the values of the .ldtk file</returns>
        public override Dictionary<string, JsonElement> Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing LDtk project: {0}", filename);
            Dictionary<string, JsonElement> dico = new Dictionary<string, JsonElement>();
            dico.Add(filename, JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(filename)));
            return dico;
        }
    }
}
