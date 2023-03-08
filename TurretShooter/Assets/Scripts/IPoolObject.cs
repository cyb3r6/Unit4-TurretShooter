
public interface IPoolObject
{
    PoolObjectId PoolId { get; }
    void Activate();
    void Deactivate();
}
