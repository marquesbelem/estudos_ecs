using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    private EntityManager _EntityManager;

    public GameObject SandPrefab;
    public GameObject DirtPrefab;
    public GameObject GrassPrefab;
    public GameObject RockPrefab;
    public GameObject SnowPrefab;

    private const int _WorldHalfSize = 75;

    [Range(0.1f, 10f)]
    public float Strength = 1f;

    [Range(0.01f, 1f)]
    public float Scale = 0.1f;

    [Range(0.1f, 10f)]
    public float StrengthTwo = 1f;

    [Range(0.01f, 1f)]
    public float ScaleTwo = 0.1f;

    [Range(0.1f, 10f)]
    public float StrengthTree = 1f;

    [Range(0.01f, 1f)]
    public float ScaleTree = 0.1f;
    
    [Range(0f, 100f)]
    public float SandLevel = 2f;

    [Range(0f, 100f)]
    public float DirtLevel = 4f;

    [Range(0f, 100f)]
    public float GrassLevel = 6f;

    [Range(0f, 100f)]
    public float RockLevel = 8f;

    [Range(0f, 100f)]
    public float SnowLevel = 10f;
    void Start()
    {
        _EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        GameManagerData.Sand = GameObjectConversionUtility.ConvertGameObjectHierarchy(SandPrefab, settings); 
        GameManagerData.Dirt = GameObjectConversionUtility.ConvertGameObjectHierarchy(DirtPrefab, settings); 
        GameManagerData.Grass = GameObjectConversionUtility.ConvertGameObjectHierarchy(GrassPrefab, settings); 
        GameManagerData.Rock = GameObjectConversionUtility.ConvertGameObjectHierarchy(RockPrefab, settings); 
        GameManagerData.Snow = GameObjectConversionUtility.ConvertGameObjectHierarchy(SnowPrefab, settings); 

        for(int z = -_WorldHalfSize; z <= _WorldHalfSize; z++)
        {
            for(int x = -_WorldHalfSize; x <= _WorldHalfSize; x ++)
            {
                var position = new float3(x, 0, z);
                Entity instance = _EntityManager.Instantiate(GameManagerData.Sand);
                _EntityManager.SetComponentData(instance, new Translation { Value = position});
            }
        }
    }

    void Update()
    {
        GameManagerData.Scale = Scale;
        GameManagerData.Strength = Strength;

        GameManagerData.ScaleTwo = ScaleTwo;
        GameManagerData.StrengthTwo = StrengthTwo;

        GameManagerData.ScaleTree = ScaleTree;
        GameManagerData.StrengthTree = StrengthTree;

        GameManagerData.SandLevel = SandLevel;
        GameManagerData.DirtLevel = DirtLevel;
        GameManagerData.GrassLevel = GrassLevel;
        GameManagerData.RockLevel = RockLevel;
        GameManagerData.SnowLevel = SnowLevel;
    }
}
