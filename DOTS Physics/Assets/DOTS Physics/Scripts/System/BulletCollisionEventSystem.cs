using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class BulletCollisionEventSystem : JobComponentSystem
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
        JobHandle jobHandle = new CollisionEventJob
        {
            BulletGroup = GetComponentDataFromEntity<BulletData>(),
            DestroyComponentGroup = GetComponentDataFromEntity<DestroyData>()
        }.Schedule(_StepPhysics.Simulation, ref _PhysicsWorld.PhysicsWorld, inputDeps);
        
        jobHandle.Complete();
        
        return jobHandle;
    }

    struct CollisionEventJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<BulletData> BulletGroup;
        public ComponentDataFromEntity<DestroyData> DestroyComponentGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.Entities.EntityA; 
            Entity entityB = collisionEvent.Entities.EntityB;

            bool isTargetA = DestroyComponentGroup.Exists(entityA);
            bool isTargetB = DestroyComponentGroup.Exists(entityB);

            bool isBulletA = BulletGroup.Exists(entityA);
            bool isBulletB = BulletGroup.Exists(entityB);

            if(isBulletA && isTargetB) //Bala atingindo o pato
            {
                /* var velocityComponent = PhysicsVelocityGroup[entityB];
                 velocityComponent.Linear = BulletGroup[entityA].CollisionEffect;
                 PhysicsVelocityGroup[entityB] = velocityComponent;*/

                var destroyComponent = DestroyComponentGroup[entityB];
                destroyComponent.Destroy = true;
                DestroyComponentGroup[entityB] = destroyComponent;
            }

            if (isBulletB && isTargetA)
            {
                /*var velocityComponent = PhysicsVelocityGroup[entityA];
                velocityComponent.Linear = BulletGroup[entityB].CollisionEffect;
                PhysicsVelocityGroup[entityA] = velocityComponent;*/

                var destroyComponent = DestroyComponentGroup[entityA];
                destroyComponent.Destroy = true;
                DestroyComponentGroup[entityA] = destroyComponent;
            }
        }
    }

}
