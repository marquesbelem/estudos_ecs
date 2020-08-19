using Unity.Entities;

public struct Settings: IComponentData
{
    public Entity Prefab;
    public int Count;
}
