using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public readonly partial struct BulletAspect : IAspect 
{
    public readonly RefRO<Bullet> Bullet;
    public readonly RefRW<LocalTransform> LocalTransform;
    public readonly RefRO<BulletMoveSpeedComponent> MoveSpeed;
    public readonly RefRO<BulletDirectionComponent> Direction;

    public void MoveAndChangeAngle(float deltaTime)
    {
        LocalTransform.ValueRW = LocalTransform.ValueRO.Translate(MoveSpeed.ValueRO.MoveSpeed * deltaTime * Direction.ValueRO.Direction);
    }
}
