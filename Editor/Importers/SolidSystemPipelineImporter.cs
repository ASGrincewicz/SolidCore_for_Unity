using System.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;
using SolidCore.UnityIntegration.Runtime.Assets;

namespace SolidCore.UnityIntegration.Editor.Importers
{
    [ScriptedImporter(1, "solidsystempipeline")]
    public class SolidSystemPipelineImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // Read raw file contents
            string rawText = System.IO.File.ReadAllText(ctx.assetPath);

            // Create the ScriptableObject wrapper
            var pipeline = ScriptableObject.CreateInstance<SolidSystemPipelineSO>();

            // Parse system type names from the file
            var systemTypeNames = ExtractSystemTypes(rawText);

            // Convert type names into SolidSystemSO assets
            pipeline.systems = CreateSystemAssets(systemTypeNames, ctx);

            // Register the pipeline asset
            ctx.AddObjectToAsset("SolidSystemPipeline", pipeline);
            ctx.SetMainObject(pipeline);
        }

        private string[] ExtractSystemTypes(string raw)
        {
            // Placeholder: treat each line as a system type name
            // Example file:
            // SolidCore.Gameplay.MovementSystem
            // SolidCore.Gameplay.GravitySystem
            // SolidCore.Rendering.SpriteSystem
            return raw
                .Split('\n')
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();
        }

        private SolidSystemSO[] CreateSystemAssets(string[] typeNames, AssetImportContext ctx)
        {
            var list = new SolidSystemSO[typeNames.Length];

            for (int i = 0; i < typeNames.Length; i++)
            {
                var sys = ScriptableObject.CreateInstance<SolidSystemSO>();
                sys.systemTypeName = typeNames[i];

                ctx.AddObjectToAsset($"System_{i}", sys);
                list[i] = sys;
            }

            return list;
        }
    }
}