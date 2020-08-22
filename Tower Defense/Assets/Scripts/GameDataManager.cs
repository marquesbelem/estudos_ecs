using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public EntityManager EntityManager;
    public Transform[] waypoints;
    public float3[] wps;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        wps = new float3[waypoints.Length];
        for(int i = 0; i < waypoints.Length; i ++)
        {
            wps[i] = waypoints[i].position;
        }
    }
}
