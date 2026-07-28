using SolidCore.UnityIntegration.Runtime.Assets;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace SolidCore.UnityIntegration.Editor.Importers
{
    [ScriptedImporter(1, "solidsystem")]
    public class SolidSystemImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // Read raw file contents
            string rawText = System.IO.File.ReadAllText(ctx.assetPath);

            // Create the ScriptableObject wrapper
            var systemAsset = ScriptableObject.CreateInstance<SolidSystemSO>();

            // For now, assume the file contains the system's fully-qualified type name
            // Example: SolidCore.Gameplay.MovementSystem
            systemAsset.systemTypeName = ExtractSystemType(rawText);

            // Register the asset with Unity
            ctx.AddObjectToAsset("SolidSystem", systemAsset);
            ctx.SetMainObject(systemAsset);
        }

        private string ExtractSystemType(string raw)
        {
            // Placeholder until you define a binary format or metadata block.
            // For now, treat the entire file as the type name.
            return raw.Trim();
        }
    }
}
