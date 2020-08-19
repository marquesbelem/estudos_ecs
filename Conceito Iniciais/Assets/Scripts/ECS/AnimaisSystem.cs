using Unity.Entities;

public class AnimaisSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<Settings>();
    }
    protected override void OnUpdate()
    {
        /* Entities
             .ForEach((Entity entity, ref DynamicBuffer<InstantiateCamis> buffer, in Settings settings) =>
             {

                 var length = buffer.Length;

                 for (int i = 0; i < length; i++)
                 {
                     EntityManager.Instantiate(settings.Prefab);
                 }
                 EntityManager.RemoveComponent<InstantiateCamis>(entity);

             }).WithStructuralChanges().Run();*/

        var settings = GetSingleton<Settings>();
        
        if (settings.Count < 1)
            return;

        for (int i = 0; i < settings.Count; i++)
        {
            EntityManager.Instantiate(settings.Prefab);
        }

        settings.Count = 0;
        SetSingleton(settings);
    }
}