using Microsoft.Xna.Framework.Content.Pipeline;
using System.Text.Json;
using MonoGame_LDtk_Importer;
using System.Collections.Generic;

namespace Importer
{
    /// <summary>
    /// A class to process the imported .ldtk file
    /// </summary>
    [ContentProcessor(DisplayName = "LDtk Project Processor")]
    class Processor : ContentProcessor<Dictionary<string, JsonElement>, LDtkProject>
    {
        /// <summary>
        /// Process the imported <see cref="JsonElement"/> to load it into a <see cref="LDtkProject"/>
        /// </summary>
        /// <param name="input"><see cref="JsonElement"/> containing the LDtk project</param>
        /// <param name="context"></param>
        /// <returns>A <see cref="LDtkProject"/> containing the values of the <paramref name="input"/></returns>
        public override LDtkProject Process(Dictionary<string, JsonElement> input, ContentProcessorContext context)
        {
            string filename = "";
            foreach (string key in input.Keys)
            {
                filename = key;
            }
            JsonElement jsonProject = new JsonElement();
            foreach (JsonElement jsonElement in input.Values)
            {
                jsonProject = jsonElement;
            }
            context.Logger.LogMessage("Processing LDtk Project File");
            LDtkProject project = LDtkProject.LoadProject(jsonProject, filename);
            
            return project;
        }
    }
}
