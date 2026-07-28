using UnityEngine;
namespace SolidCore.UnityIntegration.Runtime.Assets
{
    [CreateAssetMenu(
        fileName = "NewSolidEntity",
        menuName = "SolidCore/Entity Asset",
        order = 2)]
    public class SolidEntitySO : ScriptableObject
    {
        // The raw .solidentity file Unity imported
        public TextAsset sourceFile;

        // Optional: cached list of component type names for inspector display
        public string[] componentTypes;

        // Optional: preview of the entity contents (editor-only)
        [TextArea(4, 12)]
        public string previewJson;
    }
}