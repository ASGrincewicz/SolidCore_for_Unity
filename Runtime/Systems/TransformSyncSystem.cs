using SolidCore.ECS;
using SolidCore.ECS.Components;
using SolidCore.UnityIntegration.Runtime.Bridge;
using SolidCore.UnityIntegration.Runtime.Extensions;

namespace SolidCore.UnityIntegration.Runtime.Systems
{
    public struct TransformSyncSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var world = state.World;

            // --- 3D Transform Sync ---
            foreach (var (entity, t3) in world.Query<Transform3D>())
            {
                if (SolidCoreRuntime.EntityToGameObject.TryGetValue(entity, out var go))
                {
                    go.transform.position = t3.Position.ToUnity();
                    go.transform.rotation = t3.Rotation.ToUnity();
                }
            }

            // --- 2D Transform Sync ---
            foreach (var (entity, t2) in world.Query<Transform2D>())
            {
                if (SolidCoreRuntime.EntityToGameObject.TryGetValue(entity, out var go))
                {
                    go.transform.position = t2.Position.ToUnity();
                    go.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, t2.Rotation);
                }
            }
        }
    }
}