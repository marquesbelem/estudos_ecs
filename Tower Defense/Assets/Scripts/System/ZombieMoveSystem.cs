using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class ZombieMoveSystem : SystemBase
{
    private Random _Random;
    protected override void OnCreate()
    {
        base.OnCreate();
        _Random = new Random(0xABCD);
    }
    protected override void OnUpdate()
    {
        var random = new Random(_Random.NextUInt());
        var deltaTime = Time.DeltaTime;
        var nextWp = random.NextInt(0, GameDataManager.Instance.wps.Length);

        NativeArray<float3> waypointPositions = new NativeArray<float3>(GameDataManager.Instance.wps, Allocator.TempJob);

        Entities
            .ForEach((ref PhysicsVelocity physcisVelocity, ref PhysicsMass physicsMass, ref ZombieData zombieData,
                       ref Rotation rotation, in Translation translation) =>
            {
                var distance = math.distance(translation.Value, waypointPositions[zombieData.CurrentWP]);

                if (distance < 5)
                    zombieData.CurrentWP = nextWp;

                float3 heading;
                heading = waypointPositions[zombieData.CurrentWP] - translation.Value;

                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * zombieData.Speed);
                physcisVelocity.Linear = deltaTime * zombieData.Speed * math.forward(rotation.Value);

                physicsMass.InverseInertia[0] = 0;
                physicsMass.InverseInertia[1] = 0;
                physicsMass.InverseInertia[2] = 0;
            })
            .Run();

        waypointPositions.Dispose();
    }
}
