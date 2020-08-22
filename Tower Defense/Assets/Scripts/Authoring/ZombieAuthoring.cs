using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ZombieAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public static float Speed;
    public float RotationSpeed;
    public int CurrentWP;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new ZombieData
        {
            Speed = 0,
            RotationSpeed = RotationSpeed,
            CurrentWP = CurrentWP
        });
    }
}
