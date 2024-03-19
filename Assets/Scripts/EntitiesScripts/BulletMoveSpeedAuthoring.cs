using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletMoveSpeedAuthoring : MonoBehaviour
{
    public float MoveSpeed;

    private class Baker : Baker<BulletMoveSpeedAuthoring>
    {
        public override void Bake(BulletMoveSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BulletMoveSpeedComponent { MoveSpeed = authoring.MoveSpeed});
        }
    }
}

public struct BulletMoveSpeedComponent : IComponentData
{
    public float MoveSpeed;
}
