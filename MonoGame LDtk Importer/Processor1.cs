using Microsoft.Xna.Framework.Content.Pipeline;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TInput = System.Text.Json.JsonElement;
using TOutput = MonoGame_LDtk_Importer.LDtkProject;

namespace MonoGame_LDtk_Importer
{
    [ContentProcessor(DisplayName = "LDtk Project Processor")]
    class Processor1 : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing Ogmo Map File");
            System.Diagnostics.Debugger.Break();
            TOutput project = TOutput.LoadProject(input);
            
            return project;
        }
    }
}
