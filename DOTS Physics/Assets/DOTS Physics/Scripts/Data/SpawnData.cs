using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct SpawnData : IComponentData
{
    public Entity PrefabDuck;
    public Entity PrefabCharacter;
    public int CountDuck;
    public int CountCharacter;
}
