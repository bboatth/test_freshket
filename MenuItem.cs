public class MenuItem
{
    public string Name { get; }
    public decimal Price { get; }
    public bool HasBundleDiscount { get; }

    public MenuItem(string name, decimal price, bool hasBundleDiscount = false)
    {
        Name = name;
        Price = price;
        HasBundleDiscount = hasBundleDiscount;
    }
}
