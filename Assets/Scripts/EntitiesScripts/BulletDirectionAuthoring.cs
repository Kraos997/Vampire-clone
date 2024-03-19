using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BulletDirectionAuthoring : MonoBehaviour
{
    public float3 Direction;

    public void Setup(float3 shootDir)
    {
        this.Direction = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }
    private class Baker : Baker<BulletDirectionAuthoring>
    {
        public override void Bake(BulletDirectionAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BulletDirectionComponent { Direction = authoring.Direction});
        }
    }
}
public struct BulletDirectionComponent : IComponentData
{
    public float3 Direction;
}
