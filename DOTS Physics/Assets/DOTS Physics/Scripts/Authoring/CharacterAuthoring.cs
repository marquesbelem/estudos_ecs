using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CharacterAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public float Speed;
    public GameObject BulletPrefab;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        var bullet = conversionSystem.GetPrimaryEntity(BulletPrefab); 

        entityManager.AddComponentData(entity, new CharacterData
        {
            Speed = Speed, 
            BulletPrefab = bullet
        });

        entityManager.AddComponentData(entity, new SpawnData { });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(BulletPrefab);
    }
}
