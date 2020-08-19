using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

//Declaro as variveis, propriedades que serão necessarias para o uso
public struct MoveAutoData : IComponentData
{
    public float Speed;
}
