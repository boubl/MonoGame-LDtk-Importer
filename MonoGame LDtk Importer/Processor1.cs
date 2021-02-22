using Microsoft.Xna.Framework.Content.Pipeline;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TInput = System.Text.Json.JsonDocument;
using TOutput = MonoGame_LDtk_Importer.LDtkProject;

namespace MonoGame_LDtk_Importer
{
    [ContentProcessor(DisplayName = "LDtk Project Processor")]
    class Processor1 : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing Ogmo Map File");
            TOutput project = new TOutput();

            foreach (JsonProperty jsonProperty in input.RootElement.EnumerateObject().ToArray())
            {
                foreach(PropertyInfo property in typeof(TOutput).GetProperties())
                {
                    if (property.Name == jsonProperty.Name)
                    {
                        property.SetValue(project, jsonProperty.Value);
                    }
                }
            }

            return project;
        }
    }
}
