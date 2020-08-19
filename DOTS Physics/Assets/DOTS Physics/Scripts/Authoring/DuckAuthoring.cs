using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DuckAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new DuckData { });
        entityManager.AddComponentData(entity, new DestroyData { Destroy = false });
    }
}
