using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class CharacterControllerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var inputY = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        Entities
            .ForEach((ref PhysicsVelocity physics, ref PhysicsMass physicsMass,ref CharacterData player, ref Rotation rotation) =>
            {
                if (inputZ == 0)
                    physics.Linear = float3.zero;
                else
                    physics.Linear += inputZ * deltaTime * player.Speed * math.forward(rotation.Value);

                physicsMass.InverseInertia[0] = 0; //impede que tombe no eixo x quando usa o physics em dynamic
                physicsMass.InverseInertia[2] = 0; //impede que tombe no eixo Z quando usa o physics em dynamic

                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), deltaTime * inputY));
            })
            .Run();
    }
}
