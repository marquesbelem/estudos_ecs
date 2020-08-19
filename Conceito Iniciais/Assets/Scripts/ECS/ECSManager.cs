using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms; 
public class ECSManager : MonoBehaviour
{
    EntityManager m_Manager;
    public GameObject SheepPrefab;
    const int NumSheep = 5000; 

    void Start()
    {
        m_Manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(SheepPrefab, settings);

        for(int i = 0; i < NumSheep; i ++)
        {
            var instance = m_Manager.Instantiate(prefab); //clone na cena
            var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(0f, 10f), UnityEngine.Random.Range(-10f, 10f))); //posição inicial do meu clone
            
            //colocando os componentes necessario, nesse caso para movimentação
            m_Manager.SetComponentData(instance, new Translation { Value = position }); 
            m_Manager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });

            Debug.Log("Instanciando prefabs");
        }

        Debug.Log("Start");
    }
}
