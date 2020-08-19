using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public class Corn : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new CornMoveData
        {
            TimeOff = 2.5f,
            InMove = true,
            PosAlvo = new float3(UnityEngine.Random.Range(-170, 170), -38, UnityEngine.Random.Range(-300, 70)),
            Speed = 1.2f
        });
    }
}
