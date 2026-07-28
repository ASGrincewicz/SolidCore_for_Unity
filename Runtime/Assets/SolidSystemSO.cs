using SolidCore.ECS;
using UnityEngine;
namespace SolidCore.UnityIntegration.Runtime.Assets
{
    [CreateAssetMenu(
        fileName = "NewSystem",
        menuName = "SolidCore/System",
        order = 4)]
    public class SolidSystemSO : ScriptableObject
    {
        public string systemTypeName;

        public ISystem InstantiateSystem()
        {
            var type = System.Type.GetType(systemTypeName);
            if (type == null)
            {
                Debug.LogError($"SolidSystemSO: Could not find system type {systemTypeName}");
                return null;
            }

            return (ISystem)System.Activator.CreateInstance(type);
        }
    }
}