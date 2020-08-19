using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParallel : MonoBehaviour
{
    public GameObject Prefab;
    public int CountInstance;
    List<GameObject> m_Prafabs = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < CountInstance; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-3f, 3f));
            m_Prafabs.Add(Instantiate(Prefab, pos, Quaternion.identity));
        }
    }

    void Update()
    {
        for (int i = 0; i < m_Prafabs.Count; i++)
        {
            m_Prafabs[i].transform.Translate(0f, 0f, 0.1f);
            if (m_Prafabs[i].transform.position.z > 10)
                m_Prafabs[i].transform.position = new Vector3(Random.Range(-4f, 4f), 0, -3f);
        }
    }

}
