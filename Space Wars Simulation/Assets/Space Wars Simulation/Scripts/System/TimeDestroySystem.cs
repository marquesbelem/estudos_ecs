using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//Esses sistema é usado para destruir um entity 
[UpdateAfter(typeof(BulletSystem))] //garante que esse sistema só irá ser executado assim que o sistema bullet ter terminado
public class TimeDestroySystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem _buffer;
    protected override void OnCreate()
    {
        base.OnCreate();
        _buffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var commands = _buffer.CreateCommandBuffer().ToConcurrent();

        Entities
            .ForEach((Entity entity, int entityInQueryIndex, ref LifeTimeData lifeTime) =>
            {

                lifeTime.life -= deltaTime;
                if (lifeTime.life <= 0)
                    commands.DestroyEntity(entityInQueryIndex, entity);
            })
             .ScheduleParallel();

        _buffer.AddJobHandleForProducer(this.Dependency);

    }
}
