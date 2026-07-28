using System;
using UnityEditor.AssetImporters;
using UnityEngine;
using SolidCore.Serialization;
using System.IO;
using SolidCore.UnityIntegration.Runtime.Assets;

namespace SolidCore.UnityIntegration.Editor.Importers
{
    [ScriptedImporter(1, "solidasset")]
    public class SolidAssetImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log("SolidAssetImporter: Running importer for " + ctx.assetPath);

            byte[] bytes = File.ReadAllBytes(ctx.assetPath);

            AssetDefinition definition = null;

            try
            {
                definition = SolidAssetReader.Read(bytes);
                Debug.Log("Importer: Reader succeeded. Components = " + definition.Components.Count);
            }
            catch (Exception ex)
            {
                Debug.LogError("Importer: Reader FAILED: " + ex);
            }


            var asset = ScriptableObject.CreateInstance<SolidAssetSO>();
            asset.rawBytes   = bytes;
            asset.definition = definition;


            ctx.AddObjectToAsset("MainAsset", asset);
            ctx.SetMainObject(asset);

        }
    }
}