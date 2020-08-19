using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using UnityEditor.Build.Pipeline;
using System;
using Unity.Mathematics;

[DisableAutoCreation]
public class ChickenMoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref ChickenMoveData chickenMoveData, ref InputData inputData, ref Translation translation, ref Rotation rotation) =>
        {
            bool isRightPreessed = Input.GetKey(inputData.RightKey);
            bool isLeftPreessed = Input.GetKey(inputData.LeftKey);
            bool isUpPreessed = Input.GetKey(inputData.UpKey);
            bool isDownPreessed = Input.GetKey(inputData.DownKey);


            chickenMoveData.Direction.x = Convert.ToInt32(isRightPreessed);
            chickenMoveData.Direction.x -= Convert.ToInt32(isLeftPreessed);
            chickenMoveData.Direction.z = Convert.ToInt32(isUpPreessed);
            chickenMoveData.Direction.z -= Convert.ToInt32(isDownPreessed);

            translation.Value += chickenMoveData.Direction * chickenMoveData.Speed;

            if (isUpPreessed)
                rotation.Value = UptadeRotationChicken(0, 180, 0);
            else if (isDownPreessed)
                rotation.Value = UptadeRotationChicken(0, 0, 0);
            else if (isLeftPreessed)
                rotation.Value = UptadeRotationChicken(0, 90, 0);
            else if (isRightPreessed)
                rotation.Value = UptadeRotationChicken(0, -90, 0);


        }).WithoutBurst().Run();
    }

    private quaternion UptadeRotationChicken(float x, float y, float z)
    {
        var rotQuaternion = Quaternion.Euler(new Vector3(x, y, z));
        return new quaternion(rotQuaternion.x, rotQuaternion.y, rotQuaternion.z, rotQuaternion.w);
    }
}
