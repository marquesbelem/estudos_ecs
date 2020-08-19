using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class BulletAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    public float LifeTime;
    public float3 CollisionEffect;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new BulletData
        {
            Speed = Speed,
            CollisionEffect = CollisionEffect
        });

        entityManager.AddComponentData(entity, new LifeTimeData
        {
            Value = LifeTime
        });
    }
}
