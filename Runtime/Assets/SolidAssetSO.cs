using System;
using UnityEngine;
using SolidCore.Serialization;

namespace SolidCore.UnityIntegration.Runtime.Assets
{
    [CreateAssetMenu(
        fileName = "NewSolidAsset",
        menuName = "SolidCore/Component Asset",
        order = 1)]
    public class SolidAssetSO : ScriptableObject
    {
        // The raw .solidasset file Unity imported
        public TextAsset sourceFile;
        
        public byte[] rawBytes;


        // Parsed asset definition (components + fields)
        [NonSerialized]
        public AssetDefinition definition;

        // Optional: cached preview for inspector display
        [TextArea(3, 10)]
        public string previewJson;
    }
}