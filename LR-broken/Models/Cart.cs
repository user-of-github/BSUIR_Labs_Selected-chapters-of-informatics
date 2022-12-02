using LR.Entities;


namespace LR.Models
{
  public class Cart
  {
    public Dictionary<int, CartItem> Items { get; set; }

    public Cart() => this.Items = new Dictionary<int, CartItem>();
    
    public uint Count
    {
      get => (uint)Items.Sum(item => item.Value.Quantity);
    }
    public decimal Duration
    {
      get => Items.Sum(item => item.Value.Quantity * item.Value.Movie.Duration);
    }
    public virtual void AddToCart(Movie book)
    {
      foreach (var items in Items)
      {
        if (items.Value.Movie.Id == book.Id)
        {
          items.Value.Quantity += 1;
          return;
        }
      }

      Items.Add(book.Id, new CartItem { Movie = book, Quantity = 1 });
    }

    public virtual void RemoveFromCart(int id) => Items.Remove(id);

    public virtual void ClearAll() => Items.Clear();
  }
}