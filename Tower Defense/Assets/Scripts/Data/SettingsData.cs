using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct SettingsData : IComponentData
{
    public Entity ZombiePrefab;
    public Entity TankBasePrefab;
    public int CountZombie;
    public int CountBaseTank;
}
