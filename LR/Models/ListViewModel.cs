using System.Linq.Expressions;

namespace LR.Models
{
  public class ListViewModel<T> : List<T>
  {
    public ListViewModel() { }
    private ListViewModel(List<T> list) : base(list) { }
    public static ListViewModel<T> GetModel(IQueryable<T> items, int currentPage, int itemsPerPage, Expression<Func<T, bool>> filter) => new(items.Where(filter).Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList<T>());
  }
}
