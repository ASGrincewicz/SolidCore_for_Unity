using UnityEngine;
using SolidCore.ECS;

using SolidCore.UnityIntegration.Runtime.Assets;

namespace SolidCore.UnityIntegration.Runtime.Bridge
{
    public class SolidCoreBootstrap : MonoBehaviour
    {
        public SolidSystemPipelineSO pipeline;

        void Awake()
        {
            // Create ECS world
            SolidCoreRuntime.World = new World();

            // Create scheduler
            SolidCoreRuntime.Scheduler = new Scheduler.Scheduler();

            // Load systems from pipeline
            if (pipeline != null && pipeline.systems != null)
            {
                foreach (var sysAsset in pipeline.systems)
                {
                    var system = sysAsset.InstantiateSystem();
                    if (system != null)
                        SolidCoreRuntime.Scheduler.AddSystem(system);
                }
            }
        }

        void Update()
        {
            SolidCoreRuntime.Scheduler.Update(
                SolidCoreRuntime.World,
                Time.deltaTime
            );
        }
    }
}