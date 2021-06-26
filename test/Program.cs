using System;
using System.IO;
using System.Text.Json;
using MonoGame_LDtk_Importer;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            JsonElement jsonFile = JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(@"/Users/AlexisNicolas/Documents/MonoGame-LDtk-Importer-main/Example/Content/Test_file_for_API_showing_all_features.ldtk"));
            LDtkProject project = LDtkProject.LoadProject(jsonFile, "Test_file_for_API_showing_all_features.ldtk");
        }
    }
}
