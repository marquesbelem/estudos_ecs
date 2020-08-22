using Unity.Assertions;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class RaycastHitJob : MonoBehaviour
{
    public float Distance = 10f;
    public float3 Direction = new float3(0, 0, 1);

    private RaycastInput _RaycastInput;

    private NativeList<Unity.Physics.RaycastHit> _RayCastHits;
    private NativeList<DistanceHit> _DistanceHit;

    private BuildPhysicsWorld _PhysicsWorld;
    private StepPhysicsWorld _StepWorld;
    public bool CollectAllHits;
    public struct RaycastJob : IJob
    {
        public RaycastInput RaycastInput;
        public NativeList<Unity.Physics.RaycastHit> RayCastHits;

        public bool CollectAllHits;
        [ReadOnly] public PhysicsWorld World;
        public void Execute()
        {
            if (CollectAllHits)
            {
                World.CastRay(RaycastInput, ref RayCastHits);
            }
            else if (World.CastRay(RaycastInput, out Unity.Physics.RaycastHit hit))
            {
                RayCastHits.Add(hit);
            }
        }
    }

    private void Start()
    {
        _RayCastHits = new NativeList<Unity.Physics.RaycastHit>(Allocator.Persistent);
        _DistanceHit = new NativeList<DistanceHit>(Allocator.Persistent);
        _PhysicsWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BuildPhysicsWorld>();
        _StepWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<StepPhysicsWorld>();
    }
    private void OnDestroy()
    {
        if (_RayCastHits.IsCreated)
            _RayCastHits.Dispose();
        if (_DistanceHit.IsCreated)
            _DistanceHit.Dispose();
    }
    private void LateUpdate()
    {
        _StepWorld.FinalJobHandle.Complete();

        float3 origin = transform.position;
        float3 rdirection = (transform.rotation * Direction) * Distance;

        _RayCastHits.Clear();
        _DistanceHit.Clear();

        _RaycastInput = new RaycastInput
        {
            Start = origin,
            End = origin + rdirection,
            Filter = CollisionFilter.Default
        };

        JobHandle raycastJobHandle = new RaycastJob
        {
            RaycastInput = _RaycastInput,
            RayCastHits = _RayCastHits,
            CollectAllHits = CollectAllHits,
            World = _PhysicsWorld.PhysicsWorld

        }.Schedule();

        raycastJobHandle.Complete();

        foreach (Unity.Physics.RaycastHit hit in _RayCastHits.ToArray())
        {
            var entity = _PhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
            World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(entity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_RaycastInput.Start, _RaycastInput.End - _RaycastInput.Start);

        if (_RayCastHits.IsCreated)
        {
            foreach (Unity.Physics.RaycastHit hit in _RayCastHits.ToArray())
            {
                Assert.IsTrue(hit.RigidBodyIndex >= 0 && hit.RigidBodyIndex < _PhysicsWorld.PhysicsWorld.NumBodies);
                Assert.IsTrue(math.abs(math.lengthsq(hit.SurfaceNormal) - 1f) < 0.01f);

                Gizmos.color = Color.magenta;
                Gizmos.DrawRay(_RaycastInput.Start, hit.Position - _RaycastInput.Start);
                Gizmos.DrawSphere(hit.Position, 0.02f);
            }
        }
    }
}
