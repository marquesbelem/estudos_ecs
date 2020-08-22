using Unity.Entities;

public struct ZombieData : IComponentData
{
    public float Speed;
    public float RotationSpeed;
    public int CurrentWP;
}
