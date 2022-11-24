using System.Linq.Expressions;


namespace LR.Models
{
  public class ListViewModel<ValueType> : List<ValueType>
  {
    public static int TotalPages { get; set; }
    public static int CurrentPage { get; set; }
    public static int AmountPerPage { get; set; } = 3;
    public static int GroupId { get; set; }

    public ListViewModel() { }
    private ListViewModel(List<ValueType> list) : base(list) { }
    public static ListViewModel<ValueType> GetModel(IQueryable<ValueType> items, int currentPage, Expression<Func<ValueType, bool>> filter)
    {
      ListViewModel<ValueType>.CurrentPage = currentPage;

      return new(
        items.Where(filter)
        .Skip((currentPage - 1) * ListViewModel<ValueType>.AmountPerPage)
        .Take(ListViewModel<ValueType>.AmountPerPage)
        .ToList()
        );
    }
  }
}
