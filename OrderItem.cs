public class OrderItem
{
    public const decimal BundleDiscountRate = 0.05m;

    public MenuItem MenuItem { get; }
    public int Quantity { get; }

    public OrderItem(MenuItem menuItem, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        MenuItem = menuItem;
        Quantity = quantity;
    }

    public decimal CalculateTotal()
    {
        return MenuItem.Price * Quantity;
    }

    public decimal CalculateBundleDiscount()
    {
        if (!MenuItem.HasBundleDiscount)
        {
            return 0m;
        }

        // Integer division counts complete pairs; an extra set stays full price.
        int bundleCount = Quantity / 2;
        return bundleCount * (MenuItem.Price * 2) * BundleDiscountRate;
    }
}
