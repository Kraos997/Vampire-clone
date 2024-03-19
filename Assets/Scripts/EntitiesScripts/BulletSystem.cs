using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public partial struct BulletSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (BulletAspect bulletAspect in SystemAPI.Query<BulletAspect>())
        {
            bulletAspect.MoveAndChangeAngle(SystemAPI.Time.DeltaTime);
        }
    }
}
