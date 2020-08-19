using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class BoxTriggerEventSystem : JobComponentSystem
{
    private BuildPhysicsWorld _PhysicsWorld;
    private StepPhysicsWorld _StepPhysics;
    protected override void OnCreate()
    {
        _PhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _StepPhysics = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new BoxTriggertJob
        {
            BoxTriggerDataGroup = GetComponentDataFromEntity<BoxTriggerData>(),
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>()
        }.Schedule(_StepPhysics.Simulation, ref _PhysicsWorld.PhysicsWorld, inputDeps);

       jobHandle.Complete();

        return jobHandle;
    }

    struct BoxTriggertJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<BoxTriggerData> BoxTriggerDataGroup;
        public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.Entities.EntityA;
            Entity entityB = triggerEvent.Entities.EntityB;

            bool isBodyATrigger = BoxTriggerDataGroup.Exists(entityA);
            bool isBodyBTrigger = BoxTriggerDataGroup.Exists(entityB);

            if (isBodyATrigger && isBodyBTrigger) return;

            bool isBodyADynamic = PhysicsVelocityGroup.Exists(entityA);
            bool isBodyBDynamic = PhysicsVelocityGroup.Exists(entityB);

            if((isBodyATrigger && !isBodyBDynamic) ||
                (isBodyBTrigger && !isBodyADynamic)) return;

            var triggerEntity = isBodyATrigger ? entityA : entityB;
            var dynamicEntity = isBodyATrigger ? entityB : entityA;

            var component = PhysicsVelocityGroup[dynamicEntity];
            component.Linear += BoxTriggerDataGroup[triggerEntity].TriggerEffect;
            PhysicsVelocityGroup[dynamicEntity] = component;
        }
    }
}
