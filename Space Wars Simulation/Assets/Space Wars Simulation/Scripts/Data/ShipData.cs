using Unity.Entities;

[GenerateAuthoringComponent]
public struct ShipData : IComponentData
{
    public float Speed;
    public float RotationSpeed;
    public int CurrentWP;
    public Entity Bullet;
    public bool Approach;
}
