using SolidCore.Collections;
using SolidCore.ECS;

namespace SolidCore.UnityIntegration.Runtime.Bridge
{
    public static class SolidCoreRuntime
    {
        public static World               World;
        public static Scheduler.Scheduler Scheduler;

        // Unity-only mapping: ECS Entity → Unity GameObject
        public static readonly IndexMap<Entity, UnityEngine.GameObject> EntityToGameObject = new();
    }
}