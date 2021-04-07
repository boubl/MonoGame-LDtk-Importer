using Microsoft.Xna.Framework.Content.Pipeline;
using System.Text.Json;
using MonoGame_LDtk_Importer;

namespace Importer
{
    /// <summary>
    /// A class to process the imported .ldtk file
    /// </summary>
    [ContentProcessor(DisplayName = "LDtk Project Processor")]
    class Processor : ContentProcessor<JsonElement, LDtkProject>
    {
        /// <summary>
        /// Process the imported <see cref="JsonElement"/> to load it into a <see cref="LDtkProject"/>
        /// </summary>
        /// <param name="input"><see cref="JsonElement"/> containing the LDtk project</param>
        /// <param name="context"></param>
        /// <returns>A <see cref="LDtkProject"/> containing the values of the <paramref name="input"/></returns>
        public override LDtkProject Process(JsonElement input, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing LDtk Project File");
            System.Diagnostics.Debugger.Break();
            LDtkProject project = LDtkProject.LoadProject(input);
            
            return project;
        }
    }
}
