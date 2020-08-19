using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

[DisableAutoCreation]
public class MoveAutoSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MoveAutoData moveAutoData, ref Translation translation) =>
        {
            //Logica para fazer algo 
            translation.Value += new float3(0, 0, (0.1f * moveAutoData.Speed));
        }).Schedule();
    }
}