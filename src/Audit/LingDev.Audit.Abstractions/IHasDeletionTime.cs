namespace LingDev.Audit;

/// <summary>
/// Indicates that the IsDeleted and DeletionTime property is included.
/// </summary>
public interface IHasDeletionTime : ISoftDelete
{
}
