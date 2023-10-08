namespace example.domain.Interfaces
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeleteEntity<TKey> : IDeleteEntity, IBaseEntity<TKey>
    {
    }

    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        int CreatedUser { get; set; }
        DateTime UpdatedDate { get; set; }
        int UpdatedUser { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IBaseEntity<TKey>
    {
    }
}
