using Unity.Entities;
public class DestroyDuckSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem _Buffer;

    protected override void OnCreate()
    {
        base.OnCreate();
        _Buffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var commands = _Buffer.CreateCommandBuffer().ToConcurrent();

        Entities
            .ForEach((Entity entity, int entityInQueryIndex, in DestroyData duckData) =>
            {
                if (duckData.Destroy)
                    commands.DestroyEntity(entityInQueryIndex, entity);
            })
             .ScheduleParallel();

        _Buffer.AddJobHandleForProducer(this.Dependency);
    }
}
