using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct ChickenMoveData : IComponentData
{
    public float3 Direction;
    public float Speed;
}
