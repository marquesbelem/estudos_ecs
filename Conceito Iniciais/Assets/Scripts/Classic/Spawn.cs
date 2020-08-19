using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Prefab;
    public int CountInstance; 
    void Start()
    {
        for(int i = 0; i < CountInstance; i ++)
        {
            Vector3 pos = new Vector3(Random.Range(-4f,4f), 0, Random.Range(-3f, 3f));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
    }

}
