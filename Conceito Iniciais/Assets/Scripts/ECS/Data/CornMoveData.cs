using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct CornMoveData : IComponentData
{
    public float TimeOff;
    public bool InMove;
    public float DeltaTime;
    public float3 PosAlvo;
    public float Speed; 
}
