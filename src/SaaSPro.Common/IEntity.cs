namespace SaaSPro.Common
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
