using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var waypointPositions = new NativeArray<float3>(GameDataManager.instance.wps, Allocator.Temp);

        Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation translation, ref Rotation rotation, ref ShipData shipData) =>
            {
                var distance = math.distance(translation.Value, waypointPositions[shipData.CurrentWP]);

                if (distance < 80)
                    shipData.Approach = false;
                else if (distance > 400)
                    shipData.Approach = true;

                float3 heading;
                if (shipData.Approach)
                    heading = waypointPositions[shipData.CurrentWP] - translation.Value;
                else
                    heading = waypointPositions[shipData.CurrentWP] + translation.Value;

                var targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * shipData.RotationSpeed);
                translation.Value += deltaTime * shipData.Speed * math.forward(rotation.Value);

                if (math.distance(translation.Value, waypointPositions[shipData.CurrentWP]) < 3)
                {
                    shipData.CurrentWP++;
                    if (shipData.CurrentWP >= waypointPositions.Length)
                        shipData.CurrentWP = 0;
                }
            })
            .Run();

        waypointPositions.Dispose();
    }
}
