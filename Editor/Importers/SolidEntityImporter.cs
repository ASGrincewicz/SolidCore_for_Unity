using SolidCore.UnityIntegration.Runtime.Assets;
using UnityEditor.AssetImporters;
using UnityEngine;
namespace SolidCore.UnityIntegration.Editor.Importers
{
    [ScriptedImporter(1, "solidentity")]
    public class SolidEntityImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // Read raw file contents
            string rawText = System.IO.File.ReadAllText(ctx.assetPath);

            // Create the ScriptableObject wrapper
            var entity = ScriptableObject.CreateInstance<SolidEntitySO>();
            entity.sourceFile = new TextAsset(rawText);

            // Optional: extract component type names (placeholder)
            entity.componentTypes = ExtractComponentTypes(rawText);

            // Optional: preview JSON (placeholder)
            entity.previewJson = GeneratePreview(rawText);

            // Register the asset with Unity
            ctx.AddObjectToAsset("SolidEntity", entity);
            ctx.SetMainObject(entity);
        }

        private string[] ExtractComponentTypes(string raw)
        {
            // Placeholder until SolidEntityReader is wired in
            return new[] { "UnknownComponent" };
        }

        private string GeneratePreview(string raw)
        {
            // Placeholder preview — replace with real parsing later
            return raw.Length > 300 ? raw.Substring(0, 300) + "..." : raw;
        }
    }
}