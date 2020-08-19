using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public class ECSManagerTwo : MonoBehaviour
{
    EntityManager m_EntityManager;
    public GameObject PrefabChicken;
    public GameObject PrafabCorn;
    public GameObject[] PrefabAnimals;

    void Start()
    {
        m_EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        /*var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        var entityChicken = GameObjectConversionUtility.ConvertGameObjectHierarchy(PrefabChicken, settings);
        var pos = new float3(0, -55f, -400f);

        var entityChickenInstance = m_EntityManager.Instantiate(entityChicken);
        m_EntityManager.SetComponentData(entityChickenInstance, new Translation { Value = pos });
        m_EntityManager.SetComponentData(entityChickenInstance, new Rotation { Value = quaternion.identity });

        var entityCorn = GameObjectConversionUtility.ConvertGameObjectHierarchy(PrafabCorn, settings);
        var entityCornInstance = m_EntityManager.Instantiate(entityCorn);
        m_EntityManager.SetComponentData(entityCornInstance, new Translation { Value = pos });*/
    }
}
