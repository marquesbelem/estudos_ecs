using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using System.Collections.Generic;

public class SettingAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject Prefab;
    public int Count = 10;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new Settings
        {
            Prefab = conversionSystem.GetPrimaryEntity(Prefab),
            Count = Count
        });

       /* var bufferCamis = new NativeArray<InstantiateCamis>(Count, Allocator.Temp); 
        entityManager.AddBuffer<InstantiateCamis>(entity).AddRange(bufferCamis);

        bufferCamis.Dispose();*/
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(Prefab);
    }
}

public struct InstantiateCamis : IBufferElementData
{
     
}