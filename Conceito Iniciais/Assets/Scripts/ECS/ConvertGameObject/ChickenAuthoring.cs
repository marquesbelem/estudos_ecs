using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class ChickenAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;
    public float Speed;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new ChickenMoveData
        {
            Speed = Speed
        });

        entityManager.AddComponentData(entity, new InputData
        {
            UpKey = UpKey,
            DownKey = DownKey,
            RightKey = RightKey,
            LeftKey = LeftKey/*,
            AxisHorizontal = this.AxisHorizontal,
            AxisVertical = this.AxisVertical*/
        });
    }
}
