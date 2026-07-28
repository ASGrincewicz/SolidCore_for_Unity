using UnityEngine;
using SolidCore.Conversion;
using SolidCore.ECS;
using SolidCore.ECS.Components;
using SolidCore.UnityIntegration.Runtime.Assets;
using SolidCore.UnityIntegration.Runtime.Bridge;
using SolidCore.UnityIntegration.Runtime.Extensions;

namespace SolidCore.UnityIntegration.Runtime.Bridge
{
    public class SolidCoreEntityAuthoring : MonoBehaviour
    {
        public SolidEntitySO entityAsset;

        private Entity _entity;

        void Start()
        {
            if (entityAsset == null || entityAsset.sourceFile == null)
            {
                Debug.LogError("SolidCoreEntity has no .solidentity asset assigned.");
                return;
            }

            // Parse the .solidentity file
            var def = SolidEntityReader.Read(entityAsset.sourceFile.bytes);

            // Create ECS entity
            var world = SolidCoreRuntime.World;
            _entity = world.EntityManager.CreateEntity();

            // ⭐ REGISTER UNITY GAMEOBJECT ↔ ECS ENTITY
            SolidCoreRuntime.EntityToGameObject[_entity] = this.gameObject;

            // Add components
            for (int i = 0; i < def.Components.Count; i++)
            {
                object comp = def.Components[i];
                AddComponentToWorld(world, _entity, comp);
            }

            // Sync Unity Transform → ECS Transform2D/Transform3D
            SyncTransform(world);
        }


        private void AddComponentToWorld(World world, Entity entity, object component)
        {
            var type = component.GetType();

            // World.Set<T>(entity, component)
            var method = typeof(World).GetMethod("Set").MakeGenericMethod(type);
            method.Invoke(world, new object[] { entity, component });
        }

        private void SyncTransform(World world)
        {
            // 3D transform?
            if (world.TryGet(_entity, out Transform3D t3))
            {
                t3.Position = transform.position.SC();
                t3.Rotation = transform.rotation.SC();
                world.Set(_entity, t3);
                return;
            }


            // 2D transform?
            if (world.TryGet(_entity, out Transform2D t2))
            {
                var pos = transform.position;
                t2.Position = new SolidCore.Math.Vector2(pos.x, pos.y);
                t2.Rotation = transform.eulerAngles.z;
                world.Set(_entity, t2);
                return;
            }

        }
    }
}
