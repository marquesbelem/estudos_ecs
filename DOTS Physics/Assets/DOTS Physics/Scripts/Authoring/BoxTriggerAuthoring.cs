using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoxTriggerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float3 TriggerEffect;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new BoxTriggerData
        {
            TriggerEffect = TriggerEffect
        });
    }
}
