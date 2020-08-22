using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnSettingsSystem : SystemBase
{
    private int _CountBaseTank = 0;
    private int _CountZombie = 0;
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
          .ForEach((ref SettingsData spawn) =>
          {
              if (_CountBaseTank < spawn.CountBaseTank)
              {
                  var instance = EntityManager.Instantiate(spawn.TankBasePrefab);
                  var position = new float3(0, 2, 0);
                  EntityManager.SetComponentData(instance, new Translation { Value = position });
                  _CountBaseTank++;
              }

              if (_CountZombie < spawn.CountZombie)
              {
                  var instance = EntityManager.Instantiate(spawn.ZombiePrefab);
                  var position = new float3(random.NextFloat(-200, 200), 2, random.NextFloat(-200, 200));
                  EntityManager.SetComponentData(instance, new Translation { Value = position });
                  EntityManager.SetComponentData(instance, new ZombieData
                  {
                      Speed = random.NextFloat(100, 300),
                      RotationSpeed = random.NextFloat(100, 300),
                      CurrentWP = random.NextInt(0, GameDataManager.Instance.wps.Length)
                  });
                  _CountZombie++;
              }

          })
          .WithStructuralChanges()
          .Run();
    }
}
