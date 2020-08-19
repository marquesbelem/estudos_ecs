using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[DisableAutoCreation]
public class Spwan : SystemBase
{
    Random _random;
    protected override void OnCreate()
    {
        base.OnCreate();
        _random = new Random(0xABCD);
    }
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var random = new Random(_random.NextUInt());

        Entities
            .ForEach((ref CornMoveData cornData, ref Translation translation) =>
            {
                if (cornData.InMove)
                {
                    cornData.DeltaTime += deltaTime;
                    var direction = cornData.PosAlvo - translation.Value;
                    direction = math.normalize(direction);
                    // direction.Normalize();
                    translation.Value += direction * cornData.Speed * cornData.DeltaTime;

                    if (math.distance(cornData.PosAlvo, translation.Value) < 2)
                    {
                        cornData.DeltaTime = 0;
                        cornData.InMove = false;
                    }
                }
                else
                {
                    cornData.DeltaTime += deltaTime;
                    if (cornData.DeltaTime > cornData.TimeOff)
                    {
                        cornData.PosAlvo = new float3(random.NextFloat(-170, 170), -38, random.NextFloat(-300, 70));
                        cornData.DeltaTime = 0;
                        cornData.InMove = true;
                    }
                }
            })
            .Run();
    }
}
