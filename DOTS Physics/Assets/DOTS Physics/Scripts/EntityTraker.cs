using UnityEngine;
using Unity.Entities;
using Unity.Transforms; 

public class EntityTraker : MonoBehaviour
{
    public static EntityTraker Instance { get; private set; }

    private Entity _EntityToTrack = Entity.Null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void SetReceivedEntity(Entity entity)
    {
        _EntityToTrack = entity;
    }

    private void LateUpdate()
    {
        if (_EntityToTrack != Entity.Null)
        {
            try
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                transform.position = entityManager.GetComponentData<Translation>(_EntityToTrack).Value;
                transform.rotation = entityManager.GetComponentData<Rotation>(_EntityToTrack).Value;
            }
            catch
            {
                _EntityToTrack = Entity.Null;
            }
        }
    }
}
