using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShootSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach((ref Translation translation, ref Rotation rotation, ref ShipData shipData) =>
            {
                var directionToTarget = GameDataManager.instance.wps[shipData.CurrentWP] - translation.Value;
                var angleToTarget = math.acos(
                        math.dot(math.forward(rotation.Value), directionToTarget /
                        (math.length(math.forward(rotation.Value)) * math.length(directionToTarget))));

                if (angleToTarget < math.radians(5))
                {
                    var instance = EntityManager.Instantiate(shipData.Bullet);
                    EntityManager.SetComponentData(instance, new Translation { Value = translation.Value });
                    EntityManager.SetComponentData(instance, new Rotation { Value = rotation.Value });
                    EntityManager.SetComponentData(instance, new LifeTimeData { life = 2.5f });
                }
            })
            .WithStructuralChanges().Run();
    }
}
