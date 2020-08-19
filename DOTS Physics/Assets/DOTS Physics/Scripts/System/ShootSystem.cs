using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
public class ShootSystem : SystemBase
{
    private Unity.Mathematics.Random _Random;

    protected override void OnCreate()
    {
        base.OnCreate();
        _Random = new Unity.Mathematics.Random(0xABCD);
    }
    protected override void OnUpdate()
    {
        var shoot = Input.GetAxis("Fire1");
        var random = new Unity.Mathematics.Random(_Random.NextUInt());

        Entities
            .ForEach((ref PhysicsVelocity physicsVelocity,
                        in CharacterData characterData, in Translation translation, in Rotation rotation) =>
            {
                if (shoot > 0)
                {
                    var instance = EntityManager.Instantiate(characterData.BulletPrefab);
                    var offset = new float3(0.8f, 0.8f, 0.8f);

                    EntityManager.SetComponentData(instance, new Translation
                    {
                        Value = translation.Value + math.mul(rotation.Value, offset)
                    });

                    EntityManager.SetComponentData(instance, new Rotation { Value = rotation.Value });

                    var collisionEffect = new float3(0, random.NextFloat(100, 500), 0);
                    var speed = random.NextFloat(10, 70);

                    EntityManager.SetComponentData(instance,
                        new BulletData
                        {
                            CollisionEffect = collisionEffect,
                            Speed = speed
                        });
                }
            })
            .WithStructuralChanges()
            .Run();
    }
}
