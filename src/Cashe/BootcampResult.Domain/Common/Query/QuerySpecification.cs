using BootcampResult.Domain.Common.Cashing;
using BootcampResult.Domain.Common.Entities;
using BootcampResult.Domain.Comparers;
using System.Linq.Expressions;

namespace BootcampResult.Domain.Common.Query;

public class QuerySpecification<TEntity>(uint pageSize, uint pageToken) : CasheModel where TEntity : IEntity
{
    public IList<Expression<Func<TEntity, bool>>> FilteringOptions { get; } = new List<Expression<Func<TEntity, bool>>>();

    public IList<(Expression<Func<TEntity, object>> KeySelector, bool IsAcsending)> OrderingOptions { get; } =
        new List<(Expression<Func<TEntity, object>> KeySelector, bool IsAcsending)>();

    public FilterPagination PaginationOptions = new(pageSize, pageToken); 

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (var filter in FilteringOptions.Order(new PredicateExpressionComparer<TEntity>()))
            hashCode.Add(filter.ToString());

        foreach (var filer in OrderingOptions)
            hashCode.Add(filer.ToString());

        hashCode.Add(PaginationOptions);

        return hashCode.GetHashCode();
    }

    public override string CasheKey { get; }
}