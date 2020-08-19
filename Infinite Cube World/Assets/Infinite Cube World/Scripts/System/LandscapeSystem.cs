using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class LandscapeSystem : SystemBase
{
    private EntityQuery BlockQuery;

    protected override void OnCreate()
    {
        BlockQuery = GetEntityQuery(typeof(BlockData));
    }
    protected override void OnUpdate()
    {
        var strength = GameManagerData.Strength;
        var scale = GameManagerData.Scale;

        var strengthTwo = GameManagerData.StrengthTwo;
        var scaleTwo = GameManagerData.ScaleTwo;

        var strengthTree = GameManagerData.StrengthTree;
        var scaleTree = GameManagerData.ScaleTree;

        Entities
            .ForEach((ref Translation translation, ref BlockData blockData) =>
            {
                var vertex = translation.Value;
                var perlin1 = Mathf.PerlinNoise(vertex.x * scale, vertex.z * scale) * strength;
                var perlin2 = Mathf.PerlinNoise(vertex.x * scaleTwo, vertex.z * scaleTwo) * strengthTwo;
                var perlin3 = Mathf.PerlinNoise(vertex.x * scaleTree, vertex.z * scaleTree) * strengthTree;
                var heigth = perlin1 + perlin2 + perlin3;

                translation.Value = new float3(vertex.x, heigth, vertex.z);
            })
            .Run();

        using (var blockEntities = BlockQuery.ToEntityArray(Allocator.TempJob))
        {
            foreach(var entity in blockEntities)
            {
                float heigth = EntityManager.GetComponentData<Translation>(entity).Value.y;

                Entity block;

                if (heigth <= GameManagerData.SandLevel)
                    block = GameManagerData.Sand;
                else if (heigth <= GameManagerData.DirtLevel)
                    block = GameManagerData.Dirt;
                else if (heigth <= GameManagerData.GrassLevel)
                    block = GameManagerData.Grass;
                else if (heigth <= GameManagerData.RockLevel)
                    block = GameManagerData.Rock;
                else
                    block = GameManagerData.Snow;

                RenderMesh colourRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(block);
                var entityRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(entity);
                entityRenderMesh.material = colourRenderMesh.material;
                EntityManager.SetSharedComponentData(entity, entityRenderMesh);
            }
        }
    }
}
