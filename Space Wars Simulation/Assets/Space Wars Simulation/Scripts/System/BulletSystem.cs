using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class BulletSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        Entities
            .ForEach((ref Translation transition, ref BulletData bulletData) =>
            {
                transition.Value += bulletData.Speed * deltaTime;
            
            })
            .Run();
    }
}
