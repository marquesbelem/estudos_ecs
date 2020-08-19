using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

public struct InputData : IComponentData
{
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;
    /*public string AxisHorizontal;
    public string AxisVertical;*/
}
