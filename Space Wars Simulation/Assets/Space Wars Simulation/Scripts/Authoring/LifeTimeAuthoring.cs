using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LifeTimeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Life;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new LifeTimeData
        {
            life = Life
        });
    }
}
