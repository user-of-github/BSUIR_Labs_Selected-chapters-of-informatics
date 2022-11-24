using LR.Entities;


namespace LR.Models
{
  public class CartItem
  {
    public int Id { get; set; }
    public Movie Movie { get; set; }
    public uint Quantity { get; set; }
  }
}