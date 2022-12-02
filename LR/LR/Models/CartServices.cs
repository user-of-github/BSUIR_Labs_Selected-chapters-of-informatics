using Newtonsoft.Json;
using LR.Entities;


namespace LR.Models
{
  public class CartService : Cart
  {
    private string _sessionKey = "cartSessionKey2022";

    ISession Session { get; set; }
    public static Cart GetCart(IServiceProvider sp)
    {

      var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

      CartService cartService;
      if (session.GetString("cart") != null)
        cartService = JsonConvert.DeserializeObject<CartService>(session.GetString("cart"));
      else
        cartService = new CartService();

      // var cart = JsonConvert.DeserializeObject<CartService>(session?.GetString("cart")) ?? new CartService();

      cartService.Session = session;

      return cartService;
    }
    
    public override void AddToCart(Movie dish)
    {
      base.AddToCart(dish);
      Session?.SetString(this._sessionKey, JsonConvert.SerializeObject(this));
    }
    public override void RemoveFromCart(int id)
    {
      base.RemoveFromCart(id);
      Session?.SetString(this._sessionKey, JsonConvert.SerializeObject(this));
    }
    public override void ClearAll()
    {
      base.ClearAll();
      Session?.SetString(this._sessionKey, JsonConvert.SerializeObject(this));
    }
  }
}