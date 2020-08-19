using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public int CountDucks;
    public int CountPlayer;
    public GameObject DuckPrefab;
    public GameObject Player;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        var duck = conversionSystem.GetPrimaryEntity(DuckPrefab);
        var player = conversionSystem.GetPrimaryEntity(Player);

        entityManager.AddComponentData(entity, new SpawnData
        {
            PrefabDuck = duck,
            PrefabCharacter = player,
            CountCharacter = CountPlayer,
            CountDuck = CountDucks
        });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(DuckPrefab);
        referencedPrefabs.Add(Player);
    }
}
