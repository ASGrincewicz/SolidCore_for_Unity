using UnityEngine;

namespace SolidCore.UnityIntegration.Runtime.Assets
{
    [CreateAssetMenu(
        fileName = "NewSystemPipeline",
        menuName = "SolidCore/System Pipeline",
        order = 3)]
    public class SolidSystemPipelineSO : ScriptableObject
    {
        public SolidSystemSO[] systems;
    }
}
