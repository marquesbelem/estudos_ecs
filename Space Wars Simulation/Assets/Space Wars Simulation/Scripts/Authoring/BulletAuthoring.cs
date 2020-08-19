using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new BulletData
        {
            Speed = Speed
        });
    }
}
