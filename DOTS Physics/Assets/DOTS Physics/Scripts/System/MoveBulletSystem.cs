using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class MoveBulletSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;

        Entities
            .ForEach((ref BulletData bulletData, ref PhysicsVelocity physics, in Translation translation, in Rotation rotation) =>
            {
                physics.Angular = float3.zero;
                physics.Linear += bulletData.Speed * deltaTime * math.forward(rotation.Value); ;
            })
            .Run();
    }
}
