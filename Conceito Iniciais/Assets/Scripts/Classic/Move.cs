using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0f, 0f, 0.1f);
        if (transform.position.z > 10)
            transform.position = new Vector3(Random.Range(-4f, 4f), 0,-3f);
    }
}
