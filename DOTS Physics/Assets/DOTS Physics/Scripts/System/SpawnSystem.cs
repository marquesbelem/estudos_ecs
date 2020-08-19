using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnSystem : SystemBase
{
    private int _CountDuck = 0;
    private int _CountCharacter = 0;
    Random _random;
    protected override void OnCreate()
    {
        base.OnCreate();
        _random = new Random(0xABCD);
    }

    protected override void OnUpdate()
    {
        var random = new Random(_random.NextUInt());

        Entities
          .ForEach((ref SpawnData spawn) =>
          {
              if (_CountDuck < spawn.CountDuck)
              {
                  var instance = EntityManager.Instantiate(spawn.PrefabDuck);
                  var position = new float3(random.NextFloat(-200, 200), 2, random.NextFloat(-200, 200));
                  EntityManager.SetComponentData(instance, new Translation { Value = position });
                  _CountDuck++;
              }

              if (_CountCharacter < spawn.CountCharacter)
              {
                  var instance = EntityManager.Instantiate(spawn.PrefabCharacter);
                  var position = new float3(0, 2, 0);
                  EntityManager.SetComponentData(instance, new Translation { Value = position });
                  EntityTraker.Instance.SetReceivedEntity(instance);
                  _CountCharacter++;
              }

          })
          .WithStructuralChanges()
          .Run();
    }
}
