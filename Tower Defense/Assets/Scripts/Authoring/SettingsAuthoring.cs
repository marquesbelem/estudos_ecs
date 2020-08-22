using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SettingsAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject ZombiePrefab;
    public GameObject TankBasePrefab;

    public int CountZombie;
    public int CountBaseTank;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        var zombie = conversionSystem.GetPrimaryEntity(ZombiePrefab);
        var tank = conversionSystem.GetPrimaryEntity(TankBasePrefab);

        entityManager.AddComponentData(entity, new SettingsData
        {
            ZombiePrefab = zombie,
            TankBasePrefab = tank,
            CountZombie = CountZombie,
            CountBaseTank = CountBaseTank
        });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(ZombiePrefab);
        referencedPrefabs.Add(TankBasePrefab);
    }
}
