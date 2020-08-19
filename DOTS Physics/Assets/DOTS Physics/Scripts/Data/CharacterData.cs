using Unity.Entities;

public struct CharacterData : IComponentData
{
    public float Speed;
    public Entity BulletPrefab;
}
