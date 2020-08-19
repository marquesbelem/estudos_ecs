using Unity.Entities;
[UpdateAfter(typeof(MoveBulletSystem))]
public class TimeDestroySystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem _Buffer;

    protected override void OnCreate()
    {
        base.OnCreate();
        _Buffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var commands = _Buffer.CreateCommandBuffer().ToConcurrent();

        Entities
            .ForEach((Entity entity, int entityInQueryIndex, ref LifeTimeData lifeTimeData) =>
            {
                lifeTimeData.Value -= deltaTime;
                if (lifeTimeData.Value <= 0)
                    commands.DestroyEntity(entityInQueryIndex, entity);
            })
             .ScheduleParallel();

        _Buffer.AddJobHandleForProducer(this.Dependency);
    }
}
