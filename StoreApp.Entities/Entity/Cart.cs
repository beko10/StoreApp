namespace StoreApp.Entities.Entity;

public class Cart
{
    public List<CartItem> CartItems { get; set; } = new();

    public virtual void AddItem(Product product, int quantiy)
    {
        var item = CartItems.Where(x => x.Id == product.Id).FirstOrDefault();
        if (item is null)
        {
            var cardItem = new CartItem
            {
                Product = product,
                Quantity = quantiy
            };
            CartItems.Add(cardItem);
        }
        else
        {
            item.Quantity += quantiy;
        }
    }

    public virtual void RemoveItem(Product product, int quantity)
    {
        if (product == null || quantity <= 0)
        {
            throw new ArgumentException("Ürün veya miktar geçerli değil.");
        }

        var item = CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
        if (item is not null)
        {
            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                CartItems.Remove(item);
            }
        }
    }

    public virtual void RemoveCartItemLine(Product product)
    {
        var item = CartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
        if(item is not null)
        {
            CartItems.RemoveAll(cl => cl.Product.Id == product.Id);
        }
    }

    public void ClearCart() => CartItems.Clear();

    public decimal GetTotalPrice() => CartItems.Sum(item => item.Product.Price * item.Quantity);


}